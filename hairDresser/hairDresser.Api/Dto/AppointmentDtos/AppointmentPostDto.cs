﻿using hairDresser.Presentation.Dto.AppointmentHairServiceDtos;
using System.ComponentModel.DataAnnotations;

namespace hairDresser.Presentation.Dto.AppointmentDtos
{
    public class AppointmentPostDto
    {
        [Required(ErrorMessage = "Customer id required.")]
        [Range(1, int.MaxValue)]
        public int? CustomerId { get; set; }

        [Required(ErrorMessage = "Employee id required.")]
        [Range(1, int.MaxValue)]
        public int? EmployeeId { get; set; }

        [Required]
        public List<int>? HairServicesIds { get; set; }

        [Required(ErrorMessage = "Start date required.")]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "End date required.")]
        public DateTime? EndDate { get; set; }
    }
}
