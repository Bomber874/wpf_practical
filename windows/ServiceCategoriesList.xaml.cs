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
    /// Логика взаимодействия для ServiceCategoriesList.xaml
    /// </summary>
    public partial class ServiceCategoriesList : Window
    {
        public ServiceCategoriesList()
        {
            InitializeComponent();
            Categories.ItemsSource = dbModel.Instance.ServiceCategories.ToArray();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var menu = new NewServiceCategoryWindow();
            if (menu.ShowDialog() == true)
            {
                dbModel.Instance.ServiceCategories.Add(menu.Category);
                dbModel.Instance.SaveChanges();
                Categories.ItemsSource = dbModel.Instance.ServiceCategories.ToArray();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (Categories.SelectedItem == null)
            {
                MessageBox.Show("Не выбрана категория для редактирования");
                return;
            }
 
            var menu = new NewServiceCategoryWindow(Categories.SelectedItem as ServiceCategory);
            if (menu.ShowDialog() == true)
            {
                dbModel.Instance.SaveChanges();
            }
            else
            {
                dbModel.Instance.Entry(Categories.SelectedItem as ServiceCategory).Reload();
                Categories.ItemsSource = dbModel.Instance.ServiceCategories.ToArray();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Categories.SelectedItem == null)
            {
                MessageBox.Show("Не выбрана категория для редактирования");
                return;
            }
            ServiceCategory SelectedCategory = Categories.SelectedItem as ServiceCategory;
            Service[] services = dbModel.Instance.Services.ToArray();
            //services.Where(s => s.ServiceCategoryID == SelectedCategory.ID).FirstOrDefault()
            if (services.Where(s => s.ServiceCategoryID == SelectedCategory.ID).FirstOrDefault() != null)
            {
                MessageBox.Show("Сначала удалите все услуги, связанные с этой категорией");
                return;
            }
            dbModel.Instance.ServiceCategories.Remove(SelectedCategory);
            dbModel.Instance.SaveChanges();
            Categories.ItemsSource = dbModel.Instance.ServiceCategories.ToArray();
            //if (services.Where(s => s.ServiceCategoryID == SelectedCategory.ID).FirstOrDefault())
        }
    }
}
