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
    /// Логика взаимодействия для NewServiceCategoryWindow.xaml
    /// </summary>
    public partial class NewServiceCategoryWindow : Window
    {
        public ServiceCategory Category;
        public NewServiceCategoryWindow(ServiceCategory category)
        {
            InitializeComponent();
            Category = category;
            DataContext = Category;
        }
        public NewServiceCategoryWindow()
        {
            InitializeComponent();
            Category = new ServiceCategory();
            DataContext = Category;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
