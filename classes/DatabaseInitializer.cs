using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Text.RegularExpressions;

namespace wpf_practical.classes
{
    public class DatabaseInitializer : CreateDatabaseIfNotExists<dbModel>
    {
        protected override void Seed(dbModel db)
        {
            var Categories = new ServiceCategory[]
            {
                new ServiceCategory(){Name="Обслуживание авто"},
                new ServiceCategory(){Name="Ремонт одежды"},
                new ServiceCategory(){Name="Уход за здоровьем"}
            };

            var Services = new Service[]
            {
                new Service() {ServiceCategory=Categories[0], Name="Замена фильтров", Cost=1000},
                new Service() {ServiceCategory=Categories[0], Name="Прокачка тормозной системы", Cost=2000},
                new Service() {ServiceCategory=Categories[1], Name="Замена молнии", Cost=500},
                new Service() {ServiceCategory=Categories[1], Name="Замена пуговиц", Cost=400},
                new Service() {ServiceCategory=Categories[2], Name="Излечение от рака", Cost=999},
                new Service() {ServiceCategory=Categories[2], Name="Таблетка от смерти", Cost=89}
            };

            var Clients = new Client[]
            {
                new Client(){FirstName="Иван",LastName="Иванов", BirthDay=DateTime.Today, PhoneNumber="88005553535"},
                new Client(){FirstName="Евгений",LastName="Понамарёв", BirthDay=DateTime.Today, PhoneNumber="+77777777777"},
                new Client(){FirstName="Екатерина",LastName="Завитская", BirthDay=DateTime.Today, PhoneNumber="79514567532"}
            };

            var Orders = new Order[]
            {
                new Order(){Date=DateTime.Today, Client = Clients[0], Service=Services[0], Time = "тут время, согласно маске", Discount=2, Done = true},
                new Order(){Date=DateTime.Today, Client = Clients[1], Service=Services[2], Time = "тут время, согласно маске", Discount=3, Done = true},
                new Order(){Date=DateTime.Today, Client = Clients[2], Service=Services[5], Time = "тут время, согласно маске", Discount=50, Done = false},
            };



            db.ServiceCategories.AddRange(Categories);
            db.Services.AddRange(Services);
            db.Clients.AddRange(Clients);
            db.Orders.AddRange(Orders);
            db.SaveChanges();
        }
    }
}
