using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_practical.classes
{
    public class SearchViewModel
    {
        public ObservableCollection<Order> Orders { get; set; }
        public Order SelectedOrder { get; set; }

        public SearchViewModel()
        {
            Orders = new ObservableCollection<Order>(dbModel.Instance.Orders.ToArray());    // Тут хранятся все заказы
            FilteredOrders = new ObservableCollection<Order>(dbModel.Instance.Orders.ToArray());    // Тут соответствующие строке поиска
        }

        public ObservableCollection<Order> FilteredOrders { get; set;  } = new ObservableCollection<Order>();

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                FilterOrder();
            }
        }

        private void FilterOrder()
        {
            FilteredOrders.Clear();
            foreach (var order in Orders)
            {
                if (string.IsNullOrEmpty(SearchQuery) || order.Client.LastName.Contains(SearchQuery))
                {
                    FilteredOrders.Add(order);
                    
                }
            }
        }

    }
}
