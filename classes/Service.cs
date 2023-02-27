using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_practical.classes
{
    public class Service
    {
        public Service() { }
        public int ID { get; set; }
        public int ServiceCategoryID { get; set; }
        // Virtual или Public
        public virtual ServiceCategory ServiceCategory { get; set; }

        public string Name { get; set; }
        public int Cost { get; set; }

        public override string ToString()
        {
            //return $"id:{this.ID} Название:{this.Name} Стоимость:{this.Cost} Категория:{this.ServiceCategory.Name}";
            return $"id:{this.ID} Название:{this.Name} Стоимость:{this.Cost}";
        }

    }
}
