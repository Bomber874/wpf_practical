using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_practical
{
    public class Client
    {
        public Client() { }
        public Client(int id, string fn, string ln, DateTime bd, string pnum) {
            this.id = id;
            this.firstname = fn;
            this.lastname = ln;
            this.birthday = bd;
            this.phonenumber = pnum;
        }

        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public DateTime birthday { get; set; }
        public string phonenumber { get; set; }
        public string FName
        {
            get { return this.FullName(); }
        }

        public string FullName()
        {
            return $"{this.firstname} {this.lastname}";
        }
        public override string ToString()
        {
            return $"id:{id} Имя:{firstname} Фамилия:{lastname} Дата Рождения:{birthday} Номер телефона:{phonenumber}";
        }
    }
    
}