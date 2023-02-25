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
            var clients = new Client[]
           {
                new Client(){ firstname = "Пётр", lastname = "Евгеньевич", birthday = DateTime.Today, phonenumber="010203" },
                new Client(){ firstname = "Алексей", lastname = "Инокеньевич", birthday = DateTime.Today, phonenumber="112233" }
           };
            var nc = new Client() { firstname = "Иван", lastname = "Иванов", birthday = DateTime.Today, phonenumber = "123456" };
            var orders = new Order[]
            {
                new Order(){date = DateTime.Today, Client = nc, cost = 100, time = "10 часов", service = "Помыть авто",
                    serviceType = "Обслуживание авто", done = true, discount = 0 }
            };

            db.Clients.AddRange(clients);
            db.Clients.Add(nc);
            db.Orders.AddRange(orders);
            db.SaveChanges();
        }
    }
}
