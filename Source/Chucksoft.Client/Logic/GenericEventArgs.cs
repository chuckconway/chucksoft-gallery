using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chucksoft.Client.Logic
{
    public class GenericEventArgs<T> : EventArgs
    {
        public GenericEventArgs(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
    }
}
