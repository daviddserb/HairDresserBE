﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.HairServices.Commands.CreateHairService
{
    public class CreateHairServiceCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int DurationInMinutes { get; set; }
        public float Price { get; set; }
    }
}
