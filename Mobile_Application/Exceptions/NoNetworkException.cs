using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Application.Exceptions
{
    class NoNetworkException : Exception
    {
        public string Explain { get; set; }

        public NoNetworkException (string explain)
        {
            this.Explain = explain;
        }

        public string ToString()
        {
            return Explain;
        }
    }
}
