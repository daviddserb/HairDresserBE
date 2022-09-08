using hairDresser.Application.Interfaces;
using hairDresser.Infrastructure;
using hairDresser.Infrastructure.Repositories;
using hairDresser.Presentation.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;

//??? Intrebari:
//1. HairServiceRepository - GetAllHairServicesByIdsAsync - merge dar se poate imbunatati si nu stiu cum sa o fac intr-un singur query.
//2. EmployeeController
//3. WorkingIntervalController (aceeasi problema ca la punctul 2).
//3. Data Annotations pt. StartDate si EndDate din AppointmentPostDto, am vazut ca trebuie sa fac Custom. Astfel, mai are rost sa fac custom data annotations cand le pot verifica direct in Handler? Si daca chiar are rost sa le fac custom, unde sa pun aceasta noua clasa?

//!!! De facut:
// 1. Pagination
// 2. Comunicare cu clientul se face doar prin Dto-uri, astfel DataAnnotations (atributele) de pe proprietatile din Domain trebuie sa le pun pe proprietatile din Dto-uri.
//Sa fac si pt. StartDate si EndDate la Appointments!
// 3. Trebuie sa pun Async la toate metodele din Controllers.
// 4. Am vorbit ca la nice to have, cand un Customer face un appointment si eu verific doar disponibilitatea employee-ului, sa verific si disponibilitatea customer-ului. Ca si algoritm
//ma gandesc ca ma duc in DB, extrag toate in work (nefacute) appointments ale acelui customer si fac verificarea cu data aleasa de el, sa vad daca se face overlap sau nu. Este ok asa?
//Totusi nu stiu unde sa fac aceasta verificare.
//RASPUNS: Dupa ce fac verificarea la employee si am extrasa lista de intervale disponibile de la employee pt customer, fac verificarea si la customer, adica sa nu aibe alte appointment-uri
//care sa nu faca overlap.
// 5. Tot la nice to have, sa il 'oblig' pe Customer sa faca un nr. finit de appointments pe luna (mai exact 30 zile). RASPUNS: Acolo unde extrag intervalele disponibile ale unui employee
//pt. customer, la inceput de tot, sa verific cu un LINQ, ma duc in tabel Appointments, pt CustomerId x, WHERE data curenta - 30 days, si fac COUNT la appointment. Astfel daca nr. lor este
//sub 5, atunci ii spun ca nu poate sa faca appointment din cauza ca a depasit limita de appointments pe luna.

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