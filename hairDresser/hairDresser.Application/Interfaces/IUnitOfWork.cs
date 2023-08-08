using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IAppointmentRepository AppointmentRepository { get; }
        public IHairServiceRepository HairServiceRepository { get; }
        public IWorkingDayRepository WorkingDayRepository { get; }
        public IWorkingIntervalRepository WorkingIntervalRepository { get; }
        public IUserRepository UserRepository { get; }
        Task SaveAsync();
    }
}