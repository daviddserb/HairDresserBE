using hairDresser.Application.Interfaces;
using hairDresser.Infrastructure;
using hairDresser.Infrastructure.Repositories;
using hairDresser.Presentation.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;

//??? Intrebari:
//1. Cand apelez metoda GetAllCustomers(), vreau sa vad toate informatiile despre fiecare customer, dar cand apelez metoda de GetAllAppointments(), vreau sa vad doar CustomerName de ex.
//si nu cu toate proprietatile pe care le vad si la  GetAllCustomers(), asa ar fi normal, nu? Astfel, ce este de facut? Singura mea idee este ca va trebui sa fac mai multe multe Dto-uri,
//de ex. daca acum am un singur CustomerGetDto, care are toate proprietatile unui Customer, astfel trebuie sa fac inca unul separat in care sa pun doar numele de ex...? Totusi asta duce
//la multe Dto-uri pt. ca va trebui sa fac asta la mai multe clase si stiu ca s-a spus in lectie sa nu ajungem la foarte multe Dto-uri. Pe langa asta vor fi si mai multe legaturi in
//Profile. Este o alta posibilitate? Are rost sa fac asta? Sau cumva cand ajung pe partea de front-end, voi putea sa le filtrez cumva?
/*
 * 
 */

//2. GetEmployeeFreeIntervalsForAppointmentByDateQueryHandler -> Dupa ce calculez cate appointment-uri are customer-ul in ultima luna si vad ca nu mai poate sa faca appointment-uri
//pt. ca a depasit limita impusa de mine, ce ar trebui sa fac? Adica eu verificarea o fac in Handler, astfel trebuie sa returnez aici ceva si apoi sa tratez cazul (exceptia) in
//Controller sau o tratez direct aici, in Handler, dar atunci tot cred ca o sa mi se intre si in Controller daca nu returnez ceva. Apoi ma gandesc ca trebuie sa fac ceva Custom Error
//Handling, dar unde sa pun aceasta clasa, in ce layer?
/*
 * 
 */

//3. EmployeeController -> Cand creez un Employee pe API nu se vad si serviciile pe care le poate face, dar cand fac GetAllEmployees() sau GetEmployeeById() le vad. Stiu ca este din
//cauza ca nu am Include() in metoda de CreateEmployee() din EmployeeRepository, dar este pt. ca nu pot sa pun Include() impreuna cu AddAsync(). Astfel, singura mea idee este ca in
//metoda din Handler, in loc sa returnez employee-ul creat, sa fac un GetEmployeeById() unde va trebui cumva sa ii trimit ca parametru id-ul employee-ului creat (nu sunt sigur cum o sa
//fac asta dar cred ca se poate) si apoi sa returnez employee-ul primit din GetEmployeeById() care vine cu Include-uri. Asa ar trebui sa fac sau nu?
/*
 * 
 */
//! Sa implementez apoi si la WorkingIntervalController, pt. ca ii aceeasi problema.

//4. CreateAppointmentCommandHandler -> Cand fac un GetById la un FK (de ex. CustomerId) si nu exista in DB, in loc sa returnez null si apoi in Controller sa returnez BadRequest(),
//ar trebui  sa fac ceva Custom Exception Handling? Daca da ok, dar atunci cum fac? Adica ce returnez in Handler, adica unde tratez exceptia si in ce layer o pun?
/*
 * 
 */

//^ HairServiceRepository -> GetAllHairServicesByIdsAsync() - merge dar se poate imbunatati dar nu-mi dau seama cum s-o fac intr-un singur query.

//!!! De facut:
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