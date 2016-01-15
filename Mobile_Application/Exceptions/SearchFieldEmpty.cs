using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Application.Exceptions
{

    class SearchFieldEmpty : Exception
    {
        public string Explain { get; set; }

        public SearchFieldEmpty(string explain)
        {
            this.Explain = explain;
        }

        public string toString()
        {
            return Explain;
        }
    }
}
