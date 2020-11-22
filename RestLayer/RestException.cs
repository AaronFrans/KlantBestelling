using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestLayer
{

    public class RestException : Exception
    {

        public RestException(string message) : base(message)
        {

        }
    }
}
