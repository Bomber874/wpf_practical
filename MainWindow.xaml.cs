﻿using System;
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
using wpf_practical.windows;

namespace wpf_practical
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        dbModel db = dbModel.Instance;
        Service SelectedService;
        StatusBar statusBar;
        public string logFileName = "";
        void Notify(string message, StatusBar.TYPE type)
        {
            statusBar.Log(message, type);
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
            saveDialogue.ShowDialog();
            if (saveDialogue.SaveFile)
            {
                if (CSV.Save(saveDialogue.FileName, dataGridView1))
                    statusBar.Log($"Успешная выгрузка в файл:{saveDialogue.FileName}", StatusBar.TYPE.OK);
                else
                    statusBar.Log($"Ошибка при выгрузке файла{saveDialogue.FileName}", StatusBar.TYPE.ERROR);
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            logFileName = $"logs/{DateTime.Now.ToShortDateString().Replace('.', '-')}-{DateTime.Now.ToShortTimeString().Replace(':', '-')}.log";
            StatusBar.GenerateObject(statusBarText, logFileName);
            statusBar = StatusBar.Instance;
            statusBar.Log("Программа запущена", StatusBar.TYPE.OK);
            dataGridView1.ItemsSource = db.Orders.ToArray();
            nameInput.ItemsSource = db.Clients.ToArray();

        }



        private void exportToFileButton_Click(object sender, RoutedEventArgs e)
        {
            saveRequest();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameInput.SelectedItem==null)
            {
                Notify("Выберите клиента из списка", StatusBar.TYPE.ERROR);
                return;
            }
            if (SelectedService == null)
            {
                Notify("Выберите услугу из списка", StatusBar.TYPE.ERROR);
                return;
            }
            if (!Parser.Time(timeInput.Text))
            {
                Notify("Некорректый объём услуги", StatusBar.TYPE.ERROR);
                return;
            }
            if (dateInput.SelectedDate == null)
            {
                Notify("Выберите дату", StatusBar.TYPE.ERROR);
                return;
            }
            if (discountInput.Text == "")
                discountInput.Text = "0";

            //services.Add(new ServiceArray(0, "18.02.2023", "Иванов Иван Иванович", "Замена масла", "Обслуживание авто", "2д0ч0м", 0, 1000, true));

            //db.Orders.Add(new Order((int)Convert.ToInt16(orderNumber.Value), dateInput.SelectedDate.HasValue? (DateTime)dateInput.SelectedDate : DateTime.Today, nameInput.SelectedItem as Client, serviceInput.Text, serviceTypeInput.Text, timeInput.Text, Convert.ToInt16(discountInput.Text), Convert.ToInt16(costInput.Text), (done.IsChecked.HasValue? (bool)done.IsChecked : false )));

            //db.SaveChanges();
            //dataGridView1.ItemsSource = db.Orders.ToArray();
            db.Orders.Add(new Order((DateTime)(dateInput.SelectedDate), (Client)nameInput.SelectedItem, SelectedService, timeInput.Text, Convert.ToInt32(discountInput.Text), done.IsChecked == null ? false : (bool)done.IsChecked));
            db.SaveChanges();
            dataGridView1.ItemsSource = db.Orders.ToArray();
            statusBar.Log("Запись сохранена", StatusBar.TYPE.OK);
        }

        private void Window_Closed(object sender, EventArgs e) // Как бы выцепить причину закрытия (userexit/crash)
        {
            statusBar.Log("Завершение работы, причина: Да черт его знает, скорее всего выход из приложения", StatusBar.TYPE.SHUTDOWN);
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
            statusBar.Log("№Заказа:" + orderNumber.Value, StatusBar.TYPE.INFO);
        }

        private void dateInput_LostFocus(object sender, RoutedEventArgs e)
        {
            statusBar.Log("Дата:" + dateInput.Text, StatusBar.TYPE.INFO);
        }

        private void nameInput_LostFocus(object sender, RoutedEventArgs e)
        {
            statusBar.Log("Название/ФИО:" + nameInput.Text, StatusBar.TYPE.INFO);
        }

        private void serviceInput_LostFocus(object sender, RoutedEventArgs e)
        {
            //FIX
            //statusBar.Log("Услуга:" + serviceInput.Text, StatusBar.TYPE.INFO);
        }

        private void discountInput_LostFocus(object sender, RoutedEventArgs e)
        {
            statusBar.Log("Скидка:" + discountInput.Text, StatusBar.TYPE.INFO);
        }

        private void costInput_LostFocus(object sender, RoutedEventArgs e)
        {
            //FIX
            //statusBar.Log("Стоимость услуги:" + costInput.Text, StatusBar.TYPE.INFO);
        }

        private void done_Checked(object sender, RoutedEventArgs e)
        {
            statusBar.Log(Convert.ToBoolean(done.IsChecked)? "Услуга выполнена" : "Услуга не выполнена", StatusBar.TYPE.INFO);
        }

        private void timeInput_LostFocus(object sender, RoutedEventArgs e)
        {
            // Чтобы сработало, пришлось в маске отказаться от пробелов, в качестве разделителей
            // Также, оказалось, что timeInput.Text возвращает timeInput.Text.Trim(), поэтому в конец маски добавил \.
            timeInput.Text = timeInput.Text.Replace(' ', '0');
            statusBar.Log("Объём услуги:" + timeInput.Text, StatusBar.TYPE.INFO);
        }

        private void saveMenuItem_Selected(object sender, RoutedEventArgs e)
        {
            saveRequest();
        }

        private void openLog_Click(object sender, RoutedEventArgs e)
        {
            var log = ReadText(logFileName);
            if (log == null)
            {
                return;
            }
            ViewLogWindow viewForm = new ViewLogWindow(log);
            viewForm.ShowDialog();
        }
        private void openSearch_Click(object sender, RoutedEventArgs e)
        {

            SearchWindow viewForm = new SearchWindow();
            viewForm.ShowDialog();
        }


        private void addClientMenuItem_Click(object sender, RoutedEventArgs e)
        {
            windows.NewClient newClient = new windows.NewClient();
            if (newClient.ShowDialog() == true)
            {
                db.Clients.Add(newClient.client);
                db.SaveChanges();
                StatusBar.Instance.Log($"Новый клиент {newClient.client}", StatusBar.TYPE.OK);
            }
            
        }

        private void showClients_Click(object sender, RoutedEventArgs e)
        {
            windows.ClientsList clientsList= new windows.ClientsList();
            clientsList.ShowDialog();
        }

        private void dataGridView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(e.AddedItems[0].ToString());
        }
        private void CM_EditSelectedOrder(object sender, RoutedEventArgs e)
        {
            Order selectedOrder = dataGridView1.SelectedItem as Order;
            if (selectedOrder == null)
            {
                MessageBox.Show("Сначала выберите заказ для редактирования");
                return;
            }
            StatusBar.Instance.Log("Начато редактирование заказа:" + selectedOrder.ToSingleString(), StatusBar.TYPE.INFO);
            var menu = new EditOrderWindow(selectedOrder);
            if (menu.ShowDialog() == true)
            {
                if (db.Entry(selectedOrder).State == System.Data.Entity.EntityState.Modified | selectedOrder.Service != menu.Service)
                {
                    selectedOrder.Service = menu.Service == null ? selectedOrder.Service : menu.Service;
                    db.SaveChanges();
                    StatusBar.Instance.Log("Новые данные заказа:"+selectedOrder.ToSingleString(), StatusBar.TYPE.OK);
                    return;
                }
                StatusBar.Instance.Log("Нечего редактировать", StatusBar.TYPE.INFO);
            }
            else
            {
                db.Entry(selectedOrder).Reload();
                dataGridView1.ItemsSource = db.Orders.ToArray();
                StatusBar.Instance.Log("Отмена редактирования", StatusBar.TYPE.OK);
            }
            MessageBox.Show(dataGridView1.SelectedItem.ToString());
        }

        private void CM_DeleteSelectedOrder(object sender, RoutedEventArgs e)
        {

        }

        private void OpenSelectServiceWindow_Click(object sender, RoutedEventArgs e)
        {
            var window = new windows.SelectServiceWindow();
            if (window.ShowDialog() == true)
            {
                SelectedService = window.Service;
                OpenSelectServiceWindow.Content = $"{SelectedService.Name}\nЦена:{SelectedService.Cost}";
                statusBar.Log("Услуга:" + SelectedService.ToString(), StatusBar.TYPE.INFO);
            }
        }

        private void AddServiceCategoryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menu = new NewServiceCategoryWindow();
            if (menu.ShowDialog() == true)
            {
                db.ServiceCategories.Add(menu.Category);
                db.SaveChanges();
            }
        }

        private void CategoryListMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menu = new ServiceCategoriesList();
            if (menu.ShowDialog() == true)
            {
            
            }
        }
        private void ServiceListMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menu = new SelectServiceWindow();
            if (menu.ShowDialog() == true)
            {

            }
        }

        private void AddServiceMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menu = new NewServiceWindow();
            if (menu.ShowDialog() == true)
            {
                db.Services.Add(menu.Service);
                db.SaveChanges();
            }
        }

        private void RefreshOrdersTable(object sender, RoutedEventArgs e)
        {
            dataGridView1.ItemsSource = db.Orders.ToArray();
            nameInput.ItemsSource = db.Clients.ToArray();
        }
    }
}
