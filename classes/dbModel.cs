using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_practical.classes
{
    public class dbModel : DbContext
    {
        private dbModel()
            : base("PracticalConnecntion")
        { }
        private static dbModel _instance;
        /// <summary>
        /// Точка доступа для объекта dbModel
        /// </summary>
        public static dbModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new dbModel();
                }
                return _instance;
            }
        }

        static dbModel()
        {
            Database.SetInitializer(new DatabaseInitializer());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
    }
}