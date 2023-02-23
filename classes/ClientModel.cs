using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_practical
{
    public class ClientModel : DbContext
    {
        public ClientModel() 
            :base("PracticalConnecntion")
        { }
        public DbSet<Client> Clients { get; set; }
    }
}
