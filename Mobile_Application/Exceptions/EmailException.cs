using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Application.Exceptions
{
    class EmailException : System.Exception
    {
        private String Explain { get; set; }

        public EmailException (string explain)
        {
            this.Explain = explain;
        }

        public string toString()
        {
            return Explain;
        }
    }
}
