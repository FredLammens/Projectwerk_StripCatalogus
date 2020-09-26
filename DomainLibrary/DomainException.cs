using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLibrary
{
    public class DomainException: Exception
    {
        public DomainException(string message):base(message)
        {

        }
    }
}
