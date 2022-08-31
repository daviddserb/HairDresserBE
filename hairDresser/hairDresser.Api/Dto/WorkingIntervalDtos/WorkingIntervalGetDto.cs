﻿
using hairDresser.Presentation.Dto.EmployeeDtos;
using hairDresser.Presentation.Dto.WorkingDayDtos;

namespace hairDresser.Presentation.Dto.WorkingIntervalDtos
{
    public class WorkingIntervalGetDto
    {
        public int Id { get; set; }

        public WorkingDayGetDto WorkingDay { get; set; }

        public EmployeeGetDto Employee { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
