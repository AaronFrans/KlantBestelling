using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    class DataException : Exception
    {
        public DataException(string msg) : base(msg)
        {

        }
    }
}
