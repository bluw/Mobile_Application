using Mobile_Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Mobile_Application.Model
{
    public class Person
    {
        private string lastName;
        private string firstName;
        private int keyLength;
        private string keyUsed;
        private string email;
        private string password;
        private Algorithm typeAlgo;
        private Company company;

        public string LastName
        {
            get
            {
                return lastName;
            }

            set
            {
                Regex rgx = new Regex("\\p{L}*(-\\p{L}*)*");
                if (rgx.IsMatch(value)) {
                    lastName = value;
                } else {
                    var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                    string str = loader.GetString("ErrorNomPrenom");
                    throw new ErrorNomException(str);
                }
            }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                Regex rgx = new Regex("\\p{L}*(-\\p{L}*)*");
                if (rgx.IsMatch(value)) {
                    firstName = value;
                } else {
                    var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                    string str = loader.GetString("ErrorNomPrenom");
                    throw new ErrorNomException(str);
                }
            }
        }

        public int KeyLength
        {
            get
            {
                return keyLength;
            }

            set
            {
                if (value < 128) {
                    keyLength = value;
                } else {
                    var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                    string str = loader.GetString("keyTooShort");
                    throw new SizeOfKeyException(str);
                }
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                if (value.Contains("@") && value.Contains(".")) {
                    email = value;
                } else {
                    var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                    string str = loader.GetString("notAMailAdress");
                    throw new EmailException(str);
                }
            }
        }

        public string KeyUsed
        {
            get
            {
                return keyUsed;
            }

            set
            {
                keyUsed = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public Algorithm TypeAlgo
        {
            get
            {
                return typeAlgo;
            }

            set
            {
                typeAlgo = value;
            }
        }

        public Company Company
        {
            get
            {
                return company;
            }

            set
            {
                company = value;
            }
        }

        public Person()
        {
            Company = new Company();
            TypeAlgo = new Algorithm();
        }
    }
}
