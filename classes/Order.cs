using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using wpf_practical.classes;

namespace wpf_practical
{
    public class Order
    {
        public Order() { }

        public Order(int iD, DateTime date, int clientID, Client client, int serviceID, Service service, string time, int discount, bool done)
        {
            ID = iD;
            Date = date;
            ClientID = clientID;
            Client = client;
            ServiceID = serviceID;
            Service = service;
            Time = time;
            Discount = discount;
            Done = done;
        }

        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int ClientID { get; set; }
        public virtual Client Client { get; set; }
        public int ServiceID { get; set; }
        public virtual Service Service { get; set; }    //Нужно уточнить, как это работает
        public string Time { get; set; }
        public int Discount { get; set; }
        public bool Done { get; set; }

        public override string ToString()
        {
            return $"Id: {this.ID}\nКлиент: {this.Client.FullName()}\nДата: {this.Date}\nУслуга: {this.Service.Name}\nКатегория услуги: {this.Service.ServiceCategory.Name}\nВремя: {this.Time}\nСкидка: {this.Discount}\nСтоимость: {this.Service.Cost}\nОказана: {this.Done}\n";
        }
        // Если столбцы будут не в своих изначальных местах, выгрузка в файл будет некорректна
        public string[] ToArray() {
            return new string[] { ID.ToString(), Date.ToString(), ClientID.ToString(), Service.ID.ToString(), Service.ServiceCategory.Name, Time, Discount.ToString(), Service.Cost.ToString(), Done.ToString() };
        }
    }
}
