using AutoMapper;
using hairDresser.Application.Appointments.Commands.CreateAppointment;
using hairDresser.Application.Appointments.Queries.GetAllAppointments;
using hairDresser.Application.Appointments.Queries.GetAppointmentById;
using hairDresser.Domain.Models;
using hairDresser.Presentation.Controllers;
using hairDresser.Presentation.Dto.AppointmentDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.UnitTests
{
    public class AppointmentControllerFixture
    {
        private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly Mock<ILogger<AppointmentController>> _mockLogger = new Mock<ILogger<AppointmentController>>();

        [Fact]
        public async Task GetAllAppointments_GetAllAppointmentsQueryIsCalled()
        {
            //Arrange:
            _mockMediator
                .Setup(mediator => mediator.Send(It.IsAny<GetAllAppointmentsQuery>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            //Act:
            var controller = new AppointmentController(_mockMediator.Object, _mockMapper.Object, _mockLogger.Object);

            var pagination = new GetAllAppointmentsQuery
            {
                PageNumber = 1,
                PageSize = 1
            };
            await controller.GetAllAppointments(pagination);

            //Assert:
            _mockMediator.Verify(x => x.Send(It.IsAny<GetAllAppointmentsQuery>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task GetAppointmentById_GetAppointmentByIdQueryIsCalled()
        {
            //Arrange:
            _mockMediator
                .Setup(mediator => mediator.Send(It.IsAny<GetAppointmentByIdQuery>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            //Act:
            var controller = new AppointmentController(_mockMediator.Object, _mockMapper.Object, _mockLogger.Object);
            int appointmentId = 1;
            await controller.GetAppointmentById(appointmentId);

            //Assert:
            _mockMediator.Verify(x => x.Send(It.IsAny<GetAppointmentByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task GetAppointmentById_GetAppointmentByIdQueryWithCorrectAppointmentIdIsCalled()
        {
            // Just to be declared, and we must initialize to don't get error.
            var appointmentId = 0;

            //Arrange:
            _mockMediator
                .Setup(mediator => mediator.Send(It.IsAny<GetAppointmentByIdQuery>(), It.IsAny<CancellationToken>()))
                .Returns<GetAppointmentByIdQuery, CancellationToken>(async (appointment, cancelToken) =>
                {
                    appointmentId = appointment.AppointmentId;
                    return await Task.FromResult(
                        new Appointment
                        {
                            Id = appointment.AppointmentId
                        });
                });

            //Act:
            var controller = new AppointmentController(_mockMediator.Object, _mockMapper.Object, _mockLogger.Object);
            await controller.GetAppointmentById(1);

            //Assert:
            Assert.Equal(appointmentId, 1);
        }

        [Fact]
        public async Task GetAppointmentById_ShouldReturnOkStatusCode()
        {
            //Arrange:
            _mockMediator
                .Setup(mediator => mediator.Send(It.IsAny<GetAppointmentByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    new Appointment
                    {
                        Id = 1
                    });

            //Act:
            var controller = new AppointmentController(_mockMediator.Object, _mockMapper.Object, _mockLogger.Object);
            var result = await controller.GetAppointmentById(1);
            var okResult = result as OkObjectResult;

            //Assert:
            Assert.Equal((int) HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetAppointmentById_ShouldReturnFoundAppointment()
        {
            //Arrange:
            var appointment = new Appointment
            {
                Id = 1
            };

            _mockMediator
                .Setup(mediator => mediator.Send(It.IsAny<GetAppointmentByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(appointment);

            _mockMapper
                .Setup(mapper => mapper.Map<AppointmentGetDto>(It.Is<Appointment>(app => app == appointment)))
                .Returns(new AppointmentGetDto
                {
                    Id = 1
                });

            //Act:
            var controller = new AppointmentController(_mockMediator.Object, _mockMapper.Object, _mockLogger.Object);
            var result = await controller.GetAppointmentById(5);
            var okResult = result as OkObjectResult;

            //Assert:
            Assert.Equal(appointment.Id, ((AppointmentGetDto)okResult.Value).Id);
        }

        [Fact]
        public async Task CallCreateAppointmentAsync_ReturnsAppointment()
        {
            //Arrange:
            var appointmentPostDto = new AppointmentPostDto
            {
                // Nu-i obligatoriu sa pun toate campurile: HairServicesIds = new List<int> { 1, 2 },
                CustomerId = 1,
                EmployeeId = 1,
                HairServicesIds = new List<int> { 1, 2 },
                StartDate = DateTime.Now.AddHours(1),
                EndDate = DateTime.Now.AddHours(2)
            };

            _mockMapper
                .Setup(mapper => mapper.Map<CreateAppointmentCommand>(It.Is<AppointmentPostDto>(app => app == appointmentPostDto)))
                .Returns(new CreateAppointmentCommand
                {
                    CustomerId = new Guid("c59b1665-49b8-4d43-a1df-f10fc280ca17"),
                    EmployeeId = new Guid("c59b1665-49b8-4d44-a1df-f10fc280ca17"),
                    StartDate = DateTime.Now.AddHours(1),
                    EndDate = DateTime.Now.AddHours(4)
                });

            var appointment = new Appointment
            {
                CustomerId = new Guid("c59b1665-49b8-4d44-a2df-f10fc280ca17"),
                EmployeeId = new Guid("c59b1665-49b8-4d44-a1df-f10fc280ca17"),
                StartDate = DateTime.Now.AddHours(1),
                EndDate = DateTime.Now.AddHours(6)
            };

            _mockMediator
                .Setup(mediator => mediator.Send(It.IsAny<CreateAppointmentCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(appointment);

            _mockMapper
                .Setup(mapper => mapper.Map<AppointmentGetDto>(It.Is<Appointment>(app => app == appointment)))
                .Returns(new AppointmentGetDto
                {
                    CustomerName = "Gigel",
                    CustomerId = 1,
                    EmployeeName = "Ana",
                    StartDate = DateTime.Now.AddHours(1),
                    EndDate = DateTime.Now.AddHours(7)
                });

            var controller = new AppointmentController(_mockMediator.Object, _mockMapper.Object, _mockLogger.Object);

            //Act:
            var result = await controller.CreateAppointmentAsync(appointmentPostDto);

            var okResult = result as CreatedAtActionResult;

            //Assert:
            Assert.Equal(appointmentPostDto.CustomerId, ((AppointmentGetDto)okResult.Value).CustomerId);
        }
    }
}
