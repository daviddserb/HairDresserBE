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
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Insert JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        Array.Empty<string>()
        }
    });
});

//When somebody depends on an interface (creates an object from an interface, which is impossible), we create an object of the class type that represents that interface: IAppointmentRepository -> AppointmentRepository
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IHairServiceRepository, HairServiceRepository>();
builder.Services.AddScoped<IWorkingIntervalRepository, WorkingIntervalRepository>();
builder.Services.AddScoped<IWorkingDayRepository, WorkingDayRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//For Entity Framework, the link with the DB server
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

//For User Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();

//Authentication = verify if the user is authenticated, if they are who they say they are (for example they need to be logged in with an account).
//Authorization = verify if the user has access to what they say they have (for example they need a specific role).
builder.Services
    .AddAuthentication(options =>
    {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    //Add the support we need, what we need to validate for JWT (https://jwt.io/).
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = "https://localhost:7192", //Issuer = who creates the token.
            ValidAudience = "https://localhost:4200", //Audience = Who is the Token intended for.
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdee-312423d-dsa213321")), //Random key.
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });
builder.Services.AddAuthorization();

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
            builder.WithOrigins(new string[] { "http://localhost:4200", "http://yourdomain.com" })
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

//(because of some errors with LINQ)
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy"); //Links with the builder.Services.AddCors(CorsPolicy)...

app.UseMyMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

//To make the class visible
public partial class Program{ }