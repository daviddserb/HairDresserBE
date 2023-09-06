using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.HairServices.Commands.DeleteHairService
{
    public class DeleteHairServiceCommand : IRequest<HairService>
    {
        public int HairServiceId { get; set; }
    }
}
