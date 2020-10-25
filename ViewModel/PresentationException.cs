using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class PresentationException : Exception
    {
        public PresentationException(string message): base(message)
        {
        }
    }
}
