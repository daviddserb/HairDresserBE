using hairDresser.Application.Interfaces;
using hairDresser.Infrastructure;
using hairDresser.Infrastructure.Repositories;
using hairDresser.Presentation.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;

//??? Intrebari:
//1. HairServiceRepository - GetAllHairServicesByIdsAsync (# ii rezolvata dar se poate imbunatati).
//2. CreateAppointmentCommandHandler (# merge dar am intrebari la verificari).
//3. Cu Dto comunicam cu clientul, asta inseamna ca toate input-urile, din API, trebuie sa fie de tip Dto (in loc de Query de ex.)? Si la fel cand ii trimitem rezultatul pe API
//(in return-ul metodei din Controller), trebuie sa fie tot de tipul Dto sau nu trebuie neapart Dto, dar sa nu fie de tipul clasei din Domain, adica poate sa fie de tipul unui Command de ex.?
//4. Am vorbit ca la nice to have, cand un Customer face un appointment si eu verific doar disponibilitatea employee-ului, sa verific si disponibilitatea customer-ului. Ca si algoritm
//ma gandesc ca ma duc in DB, extrag toate in work (nefacute) appointments ale acelui customer si fac verificarea cu data aleasa de el, sa vad daca se face overlap sau nu. Este ok asa?
//Totusi nu stiu unde sa fac aceasta verificare.
//5. Tot la nice to have, sa il 'oblig' pe Customer sa faca un nr. finit de appointments pe (aici nu stiu ce sa aleg) zi/saptamana/luna? De ex. daca setez pe luna, cum as verifica asta?
//6. AppointmentController -> in regulile de numire a rutelor, zice ca nu trebuie sa avem litere mari, dar in Route, daca punem [Route("api/[controller]")], cum numele clasei este cu
//litera mare, o sa fie si aici in ruta. Astfel, cum numele clasei nu ar trebui sa il schimb in litera mica, schimb manual in fiecare ruta, adica: [Route("api/appointment")]?

//!!! De facut:
// 1. Comunicare cu clientul se face doar prin Dto-uri, astfel DataAnnotations (atributele) de pe proprietatile din Domain trebuie sa le pun pe proprietatile din Dto-uri.
// 2. Trebuie sa pun Async la toate metodele din Controllers.

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