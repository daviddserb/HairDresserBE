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
            await controller.GetAllAppointments();

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

        // ???!!! eroare
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

            //Act:
            var controller = new AppointmentController(_mockMediator.Object, _mockMapper.Object, _mockLogger.Object);
            var result = await controller.GetAppointmentById(1);
            // ??? aici pe ex. ii pus result.Result, dar eu nu am optiunea de .Result
            var okResult = result as OkObjectResult;

            //Assert:
            Assert.Equal(appointment, okResult.Value);
        }

        // ???!!! eroare
        [Fact]
        public async Task CallCreateAppointmentAsync_ReturnsAppointmentId()
        {
            //Arrange:
            var createAppointmentCommand = new AppointmentPostDto
            {
                CustomerId = 1,
                EmployeeId = 1,
                HairServicesIds = { 1, 2 },
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };

            _mockMediator
                .Setup(mediator => mediator.Send(It.Is<AppointmentPostDto>(appointment => appointment == createAppointmentCommand), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CreateAppointmentCommand
                {
                    CustomerId = 1,
                    EmployeeId = 1,
                    HairServicesIds = { 1, 2 },
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now
                });
            var controller = new AppointmentController(_mockMediator.Object, _mockMapper.Object, _mockLogger.Object);

            //Act:
            var result = await controller.CreateAppointmentAsync(createAppointmentCommand);
            var okResult = result as OkObjectResult;

            //Assert:
            Assert.Equal(createAppointmentCommand.CustomerId, ((CreateAppointmentCommand)okResult.Value).CustomerId);
        }
    }
}
