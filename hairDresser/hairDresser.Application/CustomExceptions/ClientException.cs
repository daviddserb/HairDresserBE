using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.CustomExceptions
{
    public class ClientException : ClientCustomExceptionBase
    {
        public ClientException(string message) : base(message) {}
    }
}
