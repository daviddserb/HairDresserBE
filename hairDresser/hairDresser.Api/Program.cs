using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using hairDresser.Infrastructure;
using hairDresser.Infrastructure.Repositories;
using hairDresser.Presentation.Middleware;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//When somebody depends on an interface (creates an object from an interface, which is impossible), we create an object of the class type that represents that interface: IAppointmentRepository -> AppointmentRepository
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IHairServiceRepository, HairServiceRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IWorkingIntervalRepository, WorkingIntervalRepository>();
builder.Services.AddScoped<IWorkingDayRepository, WorkingDayRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//For Entity Framework, the link with the DB server
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

//For User Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();

//Add Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

//Add Jwt Bearer
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = "audience",
        ValidIssuer = "https://localhost:7192",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdee-312423d-dsa213321")) // Random key.
    };
});

//Add MediatR which scans all the messages (Queries and Commands) and all the handlers from inside
//In order to tell the MediatR where to go to scan, we can give a single interface/class (IHairServiceRepository) from the layer that we want MediatR to operate and he will scan all of files from that layer (Application).
builder.Services.AddMediatR(typeof(IHairServiceRepository));

//Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

//Add AddCors to be able to call the API from the client (front-end)
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.WithOrigins(new string[] { "http://localhost:4200", "http://yourdomain.com" }).AllowAnyMethod().AllowAnyHeader();
        });
});

//???(because I had some errors with LINQ).
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy"); //Links with the builder.Services.AddCors(...).

app.UseAuthorization();

app.UseMyMiddleware(); //

app.UseAuthentication(); //
app.UseAuthorization(); //

app.MapControllers();

app.Run();

//To make the class visible
public partial class Program{ }