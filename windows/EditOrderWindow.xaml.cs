using System;
using System.Collections.Generic;
using System.Linq;
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
using wpf_practical.classes;

namespace wpf_practical.windows
{
    /// <summary>
    /// Логика взаимодействия для EditOrderWindow.xaml
    /// </summary>
    public partial class EditOrderWindow : Window
    {
        classes.dbModel db;
        public Service Service;
        Order EditingOrder;
        Client[] Clients;
        public EditOrderWindow(Order editingOrder)
        {
            InitializeComponent();
            this.EditingOrder = editingOrder;
            db = classes.dbModel.Instance;
            Clients = db.Clients.ToArray();
            ClientsComboBox.ItemsSource = db.Clients.ToArray();
            DataContext = EditingOrder;
        }

        private void SelectServiceMenu_Click(object sender, RoutedEventArgs e)
        {
            var menu = new SelectServiceWindow();
            if (menu.ShowDialog() == true)
            {
                Service = menu.Service;
                ServiceLabel.Content = Service.ToString();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
