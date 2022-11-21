using hairDresser.Application.Interfaces;
using hairDresser.Domain;
using hairDresser.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Infrastructure.Repositories
{
    public class HairServiceRepository : IHairServiceRepository
    {
        private readonly DataContext context;

        public HairServiceRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task CreateHairServiceAsync(HairService hairService)
        {
            await context.HairServices.AddAsync(hairService);
        }

        public async Task<IQueryable<HairService>> ReadHairServicesAsync()
        {
            var allHairServices = context.HairServices;
            return allHairServices;
        }

        public async Task<HairService> GetHairServiceByIdAsync(int hairServiceId)
        {
            var hairService = await context.HairServices.FirstOrDefaultAsync(HairService => HairService.Id == hairServiceId);
            if (hairService == null) return null;
            return hairService;
        }

        // ???
        public async Task<IQueryable<EmployeeHairService>> GetHairServicesByEmployeeId(string employeeId) {
            var employeeHairServices = context.EmployeesHairServices
                .Where(employeeHairService => employeeHairService.EmployeeId == employeeId)
                .Include(employeeHairService => employeeHairService.HairService);

            return employeeHairServices;
        }

        public async Task<List<HairService>> GetMissingHairServicesByEmployeeId(string employeeId)
        {
            var allHairServices = context.HairServices;

            var allEmployeeHairServices = context.EmployeesHairServices
                .Where(employeeHairService => employeeHairService.EmployeeId == employeeId)
                .Include(employeeHairService => employeeHairService.HairService);

            var missingHairServices = new List<HairService>();
            foreach (var hairService in allHairServices)
            {
                var cnt = 0;
                foreach (var employeeHairService in allEmployeeHairServices)
                {
                    if(hairService.Id == employeeHairService.HairServiceId)
                    {
                        cnt = 1;
                    }
                }
                if (cnt == 0)
                {
                    missingHairServices.Add(hairService);
                }
            }

            return missingHairServices;
        }

        public async Task<IQueryable<HairService>> GetAllHairServicesByIdsAsync(List<int> hairServicesIds)
        {
            //???
            // Imi aduce doar rezultatele care sunt in lista, dar de ex. daca am dat: 1, 5, 9 si 9 nu se afla in lista, returneaza 1 si 5, dar eu vreau sa nu mai returneze nimic, ca nu toate se afla in lista.
            // Cum pot imbunatati si atunci nu mai am nevoie de if/else, returnez direct ce trebuie.
            var hairServices = context.HairServices
                .Where(hairService => hairServicesIds.Contains(hairService.Id));

            if (hairServices.Count() == hairServicesIds.Count()) return hairServices;
            else return null;

            // Varianta 2:
            // ??? Merge bine dar este cu true si fals...
            var result2 = hairServicesIds
                .Intersect(context.HairServices.Select(hairService => hairService.Id))
                .Count() == hairServicesIds.Count();

            // Varianta 3:
            // ??? Merge bine dar este cu true si fals...
            var result3 = hairServicesIds
                .All(hairServiceId => context.HairServices.Select(hairService => hairService.Id).Contains(hairServiceId));

            //??? gresit
            var result4 = context.HairServices
                .Where(hairService => hairServicesIds.All(hairServiceId => hairService.Id == hairServiceId));

            //??? gresit
            var result5 = context.HairServices
                .Where(hairService => hairServicesIds.All(hairServiceId => hairServicesIds.Contains(hairService.Id)));

            //Testing:
            // Metoda 1: ??? Merge, dar eu trebuie sa fac pe tabela.Except(lista input) si atunci nu ii bine.
            var superset = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var subset = new[] { 2, 4, 6, 8 };
            var test1 = subset.Except(superset);
            bool contained = !subset.Except(superset).Any();
        }

        public async Task<TimeSpan> GetDurationByHairServicesIds(List<int> hairServicesIds)
        {
            // ??? Nu este foarte rapid, cred ca se poate sa fac count dupa ce filtrez.

            var selectedHairServices = context.HairServices
                .Where(hairService => hairServicesIds.Contains(hairService.Id));
              //.Count(hairServices => hairServices.Duration); //???

            var durationHairServices = new TimeSpan();

            foreach (var hairService in selectedHairServices)
            {
                durationHairServices += hairService.Duration;
            }

            return durationHairServices;
        }

        public async Task<float> GetPriceByHairServicesIds(List<int> hairServicesIds)
        {
            // ??? Nu este foarte rapid, cred ca se poate sa fac count dupa ce filtrez.

            var selectedHairServices = context.HairServices
                .Where(hairService => hairServicesIds.Contains(hairService.Id));
                //.Count(hairServices => hairServices.Price); //???

            float priceHairServices = 0;

            foreach (var hairService in selectedHairServices)
            {
                priceHairServices += hairService.Price;
            }

            return priceHairServices;
        }

        public async Task<HairService> UpdateHairServiceAsync(HairService hairService)
        {
            context.HairServices.Update(hairService);
            return hairService;
        }
        public async Task DeleteHairServiceAsync(int hairServiceId)
        {
            var hairService = await context.HairServices.FirstOrDefaultAsync(hairService => hairService.Id == hairServiceId);
            context.HairServices.Remove(hairService);
        }
    }
}
