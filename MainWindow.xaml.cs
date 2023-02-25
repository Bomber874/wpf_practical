using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using wpf_practical.classes;

namespace wpf_practical
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        dbModel model = new dbModel();
        StatusBar statusBar;
        public string logFileName = "";
        bool AllowEdit = false;
        void Notify(string message, StatusBar.TYPE type)
        {
            statusBar.Show(message, type);
            SystemSounds.Beep.Play();
            MessageBox.Show(message);
        }
        string[] ReadText(string filePath)
        {
            string[] txt = { };
            try
            {
                txt = File.ReadAllLines(filePath);
                return txt;
            }
            catch (FileNotFoundException)
            {
                Notify("Не найден файл " + filePath, StatusBar.TYPE.ERROR);
                return null;
            }
            catch (Exception ex)
            {
                Notify("Непредвиденная ошибка:" + ex.Message, StatusBar.TYPE.ERROR);
                return null;
            }
        }
        void saveRequest()
        {
            SaveDialogue saveDialogue = new SaveDialogue();
            saveDialogue.ShowDialog(); // saveForm.Show(); Не подойдет, т.к. тогда выполнение кода продолжится
            if (saveDialogue.SaveFile)
            {
                if (CSV.Save(saveDialogue.FileName, dataGridView1))
                    statusBar.Show($"Успешная выгрузка в файл:{saveDialogue.FileName}", StatusBar.TYPE.OK);
                else
                    statusBar.Show($"Ошибка при выгрузке файла{saveDialogue.FileName}", StatusBar.TYPE.ERROR);
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            logFileName = $"logs/{DateTime.Now.ToShortDateString().Replace('.', '-')}-{DateTime.Now.ToShortTimeString().Replace(':', '-')}.log";
            statusBar = new StatusBar(statusBarText, logFileName);
            statusBar.Show("Программа запущена", StatusBar.TYPE.OK);
            //model.Database.Delete();
            //Client nc = new Client(0, "Иван", "Иванов", DateTime.Today, "88005553535");

            //model.Clients.Add(nc);
            //model.Clients.Add(new Client(0, "Иван2", "Иванов2", DateTime.Today, "880055535352"));
            //model.Clients.Add(new Client(0, "Иван3", "Иванов3", DateTime.Today, "880055535353"));

            //model.Orders.Add(new Order(0, "18.02.2023", nc, "Замена масла", "Обслуживание авто", "2д0ч0м", 0, 1000, true));
            //model.SaveChanges();
            var services = model.Orders;
            //MessageBox.Show("Успешная запись в базу");

            dataGridView1.ItemsSource = services.ToArray();
            nameInput.ItemsSource = model.Clients.ToArray();

            //dataGridView1.ItemsSource= services;
        }



        private void exportToFileButton_Click(object sender, RoutedEventArgs e)
        {
            saveRequest();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Parser.Name(nameInput.Text))
            {
                Notify("Некорректное ФИО/Название", StatusBar.TYPE.ERROR);
                return;
            }
            if (!Parser.Name(serviceInput.Text))
            {
                Notify("Некорректное название услуги", StatusBar.TYPE.ERROR);
                return;
            }
            if (serviceTypeInput.Text == "")
            {
                Notify("Выберите вид услуги из списка", StatusBar.TYPE.ERROR);
                return;
            }
            if (!Parser.Time(timeInput.Text))
            {
                Notify("Некорректый объём услуги", StatusBar.TYPE.ERROR);
                return;
            }
            if (!Parser.Cost(costInput.Text))
            {
                Notify("Некорректая стоимость услуги", StatusBar.TYPE.ERROR);
                return;
            }

            //services.Add(new ServiceArray(0, "18.02.2023", "Иванов Иван Иванович", "Замена масла", "Обслуживание авто", "2д0ч0м", 0, 1000, true));
            model.Orders.Add(new Order((int)Convert.ToInt16(orderNumber.Value), dateInput.SelectedDate.HasValue? (DateTime)dateInput.SelectedDate : DateTime.Today, nameInput.SelectedItem as Client, serviceInput.Text, serviceTypeInput.Text, timeInput.Text, Convert.ToInt16(discountInput.Text), Convert.ToInt16(costInput.Text), (done.IsChecked.HasValue? (bool)done.IsChecked : false )));
            model.SaveChanges();
            dataGridView1.ItemsSource = model.Orders.ToArray();
            statusBar.Show("Запись сохранена", StatusBar.TYPE.OK);
        }

        private void Window_Closed(object sender, EventArgs e) // Как бы выцепить причину закрытия (userexit/crash)
        {
            statusBar.Show("Завершение работы, причина: Да черт его знает, скорее всего выход из приложения", StatusBar.TYPE.SHUTDOWN);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены?", "Выйти из программы",
                          MessageBoxButton.YesNo,
                          MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private void orderNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            statusBar.Show("№Заказа:" + orderNumber.Value, StatusBar.TYPE.INFO);
        }

        private void dateInput_LostFocus(object sender, RoutedEventArgs e)
        {
            statusBar.Show("Дата:" + dateInput.Text, StatusBar.TYPE.INFO);
        }

        private void nameInput_LostFocus(object sender, RoutedEventArgs e)
        {
            statusBar.Show("Название/ФИО:" + nameInput.Text, StatusBar.TYPE.INFO);
        }

        private void serviceInput_LostFocus(object sender, RoutedEventArgs e)
        {
            statusBar.Show("Услуга:" + serviceInput.Text, StatusBar.TYPE.INFO);
        }

        private void serviceTypeInput_LostFocus(object sender, RoutedEventArgs e)
        {
            statusBar.Show("Вид услуги:" + serviceTypeInput.Text, StatusBar.TYPE.INFO);
        }

        private void discountInput_LostFocus(object sender, RoutedEventArgs e)
        {
            statusBar.Show("Скидка:" + discountInput.Text, StatusBar.TYPE.INFO);
        }

        private void costInput_LostFocus(object sender, RoutedEventArgs e)
        {
            statusBar.Show("Стоимость услуги:" + costInput.Text, StatusBar.TYPE.INFO);
        }

        private void done_Checked(object sender, RoutedEventArgs e)
        {
            statusBar.Show(Convert.ToBoolean(done.IsChecked)? "Услуга выполнена" : "Услуга не выполнена", StatusBar.TYPE.INFO);
        }

        private void timeInput_LostFocus(object sender, RoutedEventArgs e)
        {
            // Чтобы сработало, пришлось в маске отказаться от пробелов, в качестве разделителей
            // Также, оказалось, что timeInput.Text возвращает timeInput.Text.Trim(), поэтому в конец маски добавил \.
            timeInput.Text = timeInput.Text.Replace(' ', '0');
            statusBar.Show("Объём услуги:" + timeInput.Text, StatusBar.TYPE.INFO);
        }

        private void saveMenuItem_Selected(object sender, RoutedEventArgs e)
        {
            saveRequest();
        }

        private void openLog_Click(object sender, RoutedEventArgs e)
        {
            var log = ReadText(logFileName); if (log == null)
            {
                return;
            }
            ViewLogWindow viewForm = new ViewLogWindow(log);
            viewForm.ShowDialog();
        }

        private void addClientMenuItem_Click(object sender, RoutedEventArgs e)
        {
            windows.NewClient newClient = new windows.NewClient();
            newClient.ShowDialog();
        }

        private void showClients_Click(object sender, RoutedEventArgs e)
        {
            windows.ClientsList clientsList= new windows.ClientsList();
            clientsList.ShowDialog();
        }
    }
}
