using System.Data.Entity;

namespace wpf_practical
{
    public class OrderModel : DbContext
    {
        public OrderModel()
            :base("PracticalConnecntion")
        {}
        public DbSet<Order> Services { get; set; }
    }
}