using hairDresser.Application.Interfaces;
using hairDresser.Infrastructure;
using hairDresser.Infrastructure.Repositories;
using hairDresser.Presentation.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;

//??? Intrebari:
// 1. HairServiceRepository - GetAllHairServicesByIdsAsync.
// 2. AppointmentControllerFixture - ultimele 2 teste.
// 3.
//Cu Dto comunicam cu clientul, asta inseamna ca toate input-urile, din API, trebuie sa fie de tip Dto (in loc de Query de ex.)?
//Si la fel cand ii trimitem rezultatul pe API (in return-ul metodei din Controller), trebuie sa fie tot de tipul Dto sau nu? Sau nu trebuie neapart Dto, dar sa nu fie de tipul clasei din Domain, adica poate sa fie de tipul unui Command de ex.?

//!!! De facut:
// 1. Comunicare cu clientul se face doar prin Dto-uri, astfel DataAnnotations (atributele) de pe proprietatile din Domain trebuie sa le pun pe proprietatile din Dto-uri.
// 2. Trebuie sa pun Async la toate metodele din Controllers.

//$$$ De citit despre:
// many in the middle concept
// pagination (skip, test)

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
