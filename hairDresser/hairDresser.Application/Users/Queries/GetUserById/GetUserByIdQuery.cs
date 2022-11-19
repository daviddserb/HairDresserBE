using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public string UserId { get; set; }
    }
}
