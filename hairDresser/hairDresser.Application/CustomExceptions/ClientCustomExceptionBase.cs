using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.CustomExceptions
{
    public abstract class ClientCustomExceptionBase : Exception
    {
        public ClientCustomExceptionBase(string message) : base(message)
        {

        }
    }
}
