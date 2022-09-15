using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.CustomExceptions
{
    public class NotFoundException : ClientCustomExceptionBase
    {
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
