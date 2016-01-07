using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Application.Exceptions
{
    class ErrorNomException : System.Exception
    {
        public string Explain { get; set; }

        public ErrorNomException (string explain)
        {
            this.Explain = explain;
        }

        public string toString()
        {
            return Explain;
        }
    }
}
