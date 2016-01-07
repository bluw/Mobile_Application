using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Application.Exceptions
{
    class SizeOfKeyException : System.Exception
    {
        private string Explain { get; set; }

        public SizeOfKeyException (string explain)
        {
            this.Explain = explain;
        }

        public string toString()
        {
            return Explain;
        }
    }
}
