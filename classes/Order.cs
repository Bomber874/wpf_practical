using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace wpf_practical
{
    public class Order
    {
        public Order() { }
        public Order(int orderId, string date, Client client, string service, string serviceType, string time, int discount, int cost, bool done)
        {
            this.id = orderId;
            this.date = date;
            this.Client = client;
            this.service = service;
            this.serviceType = serviceType;
            this.time = time;
            this.discount = discount;
            this.cost = cost;
            this.done = done;
            //MessageBox.Show($"Вызов конструктора для объекта {name}");
        }
        public int id { get; set; }
        public string date { get; set; }
        public int ClientID { get; set; }
        public virtual Client Client { get; set; }
        public string name { get; set; }
        public string service { get; set; }
        public string serviceType { get; set; }
        public string time { get; set; }
        public int discount { get; set; }
        public int cost { get; set; }
        public bool done { get; set; }

        public override string ToString()
        {
            return $"Id: {this.id}\nКлиент: {this.Client.FullName()}\nДата: {this.date}\nУслуга: {this.service}\nТип услуги: {this.serviceType}\nВремя: {this.time}\nСкидка: {this.discount}\nСтоимость: {this.cost}\nОказана: {this.done}\n";
        }
    }
}
