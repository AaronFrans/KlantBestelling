using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer
{
    public class DomainException: Exception
    {
        public DomainException(string msg): base(msg)
        {

        }
    }
}
