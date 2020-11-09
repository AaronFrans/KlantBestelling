using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class DataException : Exception
    {
        public DataException(string msg) : base(msg)
        {

        }
    }
}
