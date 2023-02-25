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
        public dbModel()
            : base("PracticalConnecntion")
        { }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}