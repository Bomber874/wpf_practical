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
    /// Логика взаимодействия для SelectServiceWindow.xaml
    /// </summary>
    public partial class SelectServiceWindow : Window
    {
        classes.dbModel db;
        public Service Service { get; set; }
        public SelectServiceWindow()
        {
            InitializeComponent();
            db = classes.dbModel.Instance;

            var Categories = db.ServiceCategories.ToArray();
            var Servcies = db.Services.ToArray();
            // Штука неоптимизированная, много лишних итераций
            // Если во время обхода db.ServiceCategories попытаться работать с базой, вызовется исключение. Поэтому я сохранил объекты в массивы
            // Следовало бы подписаться на событие раскрытия категории, чтобы в этот момент подгружать только необходимые услуги
            foreach (ServiceCategory c in Categories)
            {
                var item = new TreeViewItem();
                item.Header = c.Name;
                foreach (Service s in Servcies.Where(s => (s.ServiceCategoryID == c.ID)) )
                {
                    item.Items.Add(s);
                }
                tvServices.Items.Add(item);
            }
        }

        private void tvServices_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (!(tvServices.SelectedItem is Service))
                return;
            Service = (Service)tvServices.SelectedItem;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Service == null)
            {
                MessageBox.Show("Вы не выбрали услугу");
                return;
            }
            DialogResult = true;
            Close();
        }
    }
}
