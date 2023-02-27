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
        public Client(int iD, string firstName, string lastName, DateTime birthDay, string phoneNumber)
        {
            ID = iD;
            FirstName = firstName;
            LastName = lastName;
            BirthDay = birthDay;
            PhoneNumber = phoneNumber;
        }

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string PhoneNumber { get; set; }
        public string FName
        {
            get { return this.FullName(); }
        }

        public string FullName()
        {
            return $"{this.FirstName} {this.LastName}";
        }
        public override string ToString()
        {
            return $"id:{ID} Имя:{FirstName} Фамилия:{LastName} Дата Рождения:{BirthDay} Номер телефона:{PhoneNumber}";
        }
    }
    
}