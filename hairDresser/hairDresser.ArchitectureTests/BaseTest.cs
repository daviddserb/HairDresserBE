using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using hairDresser.Infrastructure;
using hairDresser.Presentation.Controllers;

using Assembly = System.Reflection.Assembly;

namespace hairDresser.ArchitectureTests
{
    public abstract class BaseTest
    {
        protected static readonly Assembly DomainAssembly = typeof(User).Assembly;
        protected static readonly Assembly ApplicationAssembly = typeof(IUserRepository).Assembly;
        protected static readonly Assembly InfrastructureAssembly = typeof(DataContext).Assembly;
        protected static readonly Assembly PresentationAssembly = typeof(AppointmentController).Assembly;
    }
}