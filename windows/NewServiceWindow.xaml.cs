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
    /// Логика взаимодействия для NewServiceWindow.xaml
    /// </summary>
    public partial class NewServiceWindow : Window
    {
        public classes.Service Service;
        dbModel db = dbModel.Instance;
        public NewServiceWindow(classes.Service service)
        {
            InitializeComponent();
            this.Service = service;
            Categories.ItemsSource = db.ServiceCategories.ToArray();
            DataContext = service;
        }
        public NewServiceWindow()
        {
            InitializeComponent();
            this.Service = new classes.Service();
            Categories.ItemsSource = db.ServiceCategories.ToArray();
            DataContext = this.Service;
        }

        private void Categories_Selected(object sender, RoutedEventArgs e)
        {
            this.Service.ServiceCategory = Categories.SelectedItem as classes.ServiceCategory;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Categories.SelectedItem == null)
            {
                MessageBox.Show("Не выбрана категория услуги");
                return;
            }
            DialogResult = true;
            Close();
        }
    }
}
