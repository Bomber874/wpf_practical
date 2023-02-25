using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace wpf_practical.windows
{
    /// <summary>
    /// Логика взаимодействия для ClientsList.xaml
    /// </summary>
    public partial class ClientsList : Window
    {
        classes.dbModel model = classes.dbModel.Instance;
        public ClientsList()
        {
            InitializeComponent();
            dgClients.ItemsSource = model.Clients.ToArray();
        }

        private void AddClient(object sender, RoutedEventArgs e)
        {
            var ncWindow = new NewClient();
            if (ncWindow.ShowDialog() == true)  // Ибо если просто закрыть форму, значение не будет булевым
            {
                // Клиент добавляется внутри формы, но я не знаю, насколько это хорошая затея
                // update: Затея была авантюрной. Понял это тогда, когда начал писать редактирование сущетсвующего клиента
                model.Clients.Add(ncWindow.client);
                model.SaveChanges();
                dgClients.ItemsSource = model.Clients.ToArray();
            }
        }

        private void EditClient(object sender, RoutedEventArgs e)
        {
            if (dgClients.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента для редактирования");
                return;
            }
            Client client = dgClients.SelectedItem as Client;
            var ncWindow = new NewClient(client);
            if (ncWindow.ShowDialog() == true)
            {
                //Client oldClient = model.Clients.Where(c => c.id == client.id).Select(c => c).FirstOrDefault();
                //oldClient.firstname = client.firstname;
                //oldClient.lastname = client.lastname;
                //oldClient.birthday = client.birthday;
                //oldClient.phonenumber = client.phonenumber;
                if (model.Entry(client).State == System.Data.Entity.EntityState.Modified)
                {
                    model.SaveChanges();
                    //dgClients.ItemsSource = model.Clients.ToArray();
                }
            }
            else
            {
                model.Entry(client).Reload();
                dgClients.ItemsSource = model.Clients.ToArray();
            }
        }

        private void DeleteClient(object sender, RoutedEventArgs e)
        {

        }
    }
}
