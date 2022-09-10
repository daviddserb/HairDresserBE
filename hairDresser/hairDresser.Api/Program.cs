using hairDresser.Application.Interfaces;
using hairDresser.Infrastructure;
using hairDresser.Infrastructure.Repositories;
using hairDresser.Presentation.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;

//??? Intrebari:
//1. EmployeeController (am aceeasi problema si la WorkingIntervalController).
//2. De ex. cand apelez metoda de GetAllCustomers(), normal ca vreau sa vad fiecare customer in parte cu mai multe informatii despre fiecare, dar cand apelez metoda de GetAllAppointments,
//eu vreau sa vad doar CustomerName de ex., nu vreau sa vad toate proprietatile pe care le vad si cand fac GetAllCustomers. Astfel, are rost sa fac ceva pt. asta? Stiu ca se va complica
//treaba pt. ca va trebui sa fac multe Dto-uri si multe legaturi in Profile.
//3. GetEmployeeFreeIntervalsForAppointmentByDateQueryHandler -> Dupa ce calculez cate appointment-uri are customer-ul in ultima luna si vad ca nu mai poate sa faca appointment-uri pt.
//ca a depasit limita impusa de mine, ce ar trebui sa fac, pt. ca eu verificarea o fac in Handler, astfel trebuie sa returnez ceva si apoi sa tratez exceptia in Controller sau o tratez
//direct in metoda din Handler dar atunci tot cred ca o sa mi se intre si in Controller, pt. ca nu am pus un return ceva.
//* HairServiceRepository -> GetAllHairServicesByIdsAsync() - merge dar se poate imbunatati si nu stiu cum sa o fac intr-un singur query.

//!!! De facut:
// Custom Data Validations pt. AppointmentPostDto la StartDate sa NU fie in trecut (adica sa fie >= prezent) si EndDate sa fie mai mare decat StartDate.
// Trebuie sa pun Async la toate metodele din fiecare Controller (boring...).

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// What I added (START):
// De fiecare data cand vei vedea ca cineva depinde de IHairServiceRepository, creezi o instanta de HairServiceRepository (la fel si pt. restul).
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IHairServiceRepository, HairServiceRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IWorkingIntervalRepository, WorkingIntervalRepository>();
builder.Services.AddScoped<IWorkingDayRepository, WorkingDayRepository>();

//Facem legatura cu server-ul nostru din DB.
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Adaugam MediatR, care scaneaza toate mesajele (Queries/Commands) si toate handle-urile, de tipul typeof().
// Cu toate ca noi avem mai multe .AddScoped(), adaugam doar unul dintre ele la typeof() si MediatR le scaneaza pe toate din layer-ul de unde typeof() face parte, adica IHairServiceRepository face parte din Application.
builder.Services.AddMediatR(typeof(IHairServiceRepository));

 // maparea
builder.Services.AddAutoMapper(typeof(Program));
// What I added (STOP).

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMyMiddleware(); // What I added.

app.MapControllers();

app.Run();

public partial class Program{ } // What I added.