using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTill.till
{
    public class ArgumentNullException : Exception
    {
        public ArgumentNullException() : base(){}

        public ArgumentNullException(string message) : base(message){}

        public ArgumentNullException(string message, System.Exception inner) : base(message, inner){}

        protected ArgumentNullException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
