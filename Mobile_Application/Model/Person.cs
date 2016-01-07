using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Application.Model
{
    public class Person
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public int KeyLength { get; set; }

        public string KeyUsed { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Algorithm TypeAlgo { get; set; }

        public Company Company { get; set; }

        public Person()
        {
            Company = new Company();
            TypeAlgo = new Algorithm();
        }
    }
}
