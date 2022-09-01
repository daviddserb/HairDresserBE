using hairDresser.Application.Interfaces;
using hairDresser.Infrastructure;
using hairDresser.Infrastructure.Repositories;
using hairDresser.Presentation.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;

//??? Intrebari:
// 1. Atunci cand creezi un appointment si dai un customerId care nu exista sau un employeeId care nu exista sau un hairServiceId care nu exista, in loc sa imi apara exceptie in cod, cum fac sa las sa mearga aplicatia (codul) si sa ii trimit un Http Code gresit (adica care sa il avertizeze de eroarea produsa) si in care sa ii zic exact ce a introdus gresit. Acest lucru il fac la cererile de Get (ca verifici daca ce ai cautat este null de ex.), dar la cel de Post, am vazut ca pot sa il realizez doar cu try catch (sunt si alte metode?) in Handler, pt. ca alta idee nu am cum sa verific daca este ceva gresit, pt. ca eroarea imi da cand se face await _unitOfWork.SaveAsync();. Totusi, eu in Handler returnez un id, care este de tip int, si atunci cand am eroare returnez -1, doar ca sa stiu eu sa o tratez. Cum pot sa fac altcumva, in loc sa returnez -1?
// 2. Trebuie sa pun Async la numele metodelor din Controllers?
// 3. Sa mai adaug si WorkingDays pe API... are rost?

//!!! De facut:
// 1. Dupa ce primesc raspuns la intreabarea 1., sa fac si aici: CreateWorkingIntervalCommandHandler. Si cred ca trebuie si in restul claselor de Controller, la Create, adica Post.
// 2. Nu imi dau seama in care Controller sa pun 'GetEmployeeIntervalsByDate', unde tu dai un employeeId si un date, si primesti inapoi, calculat pe intervalele de lucru ale unui employee si appointment-urile lui, intervalele posibile la care iti poti face un appointment la employee-ul ales (dupa id primit).


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// What I added (START):
// De fiecare data cand vei vedea ca cineva depinde de IHairServiceRepository, creezi o instanta de HairServiceRepository (la fel si pt. restul).
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IHairServiceRepository, HairServiceRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IWorkingIntervalRepository, WorkingIntervalRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
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
