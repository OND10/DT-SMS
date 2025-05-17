using DataTrans.Domain.Entities;
using DataTrans.Infustracture.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data;
using DataTrans.Infustracture.Extensions;
using DataTrans.Application.Extensions;
using System.Text;
using DataTrans.Domain.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Retrieve the connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure Serilog from program.cs
//Log.Logger = new LoggerConfiguration()
//    .Enrich.FromLogContext()
//    .MinimumLevel.Information() // Set minimum log level
//    .WriteTo.Console() // Log to console
//    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day) // Log to file
//    .WriteTo.MSSqlServer(
//        connectionString: connectionString,
//        sinkOptions: new MSSqlServerSinkOptions
//        {
//            TableName = "Logs", // Log table
//            AutoCreateSqlTable = true // Automatically create the table if it doesn't exist
//        },
//        restrictedToMinimumLevel: LogEventLevel.Information, // Log only Information or above
//        columnOptions: new ColumnOptions
//        {
//            AdditionalDataColumns = new DataColumn[]
//            {
//                new DataColumn { DataType = typeof(string), ColumnName = "UserId" } // Custom data column
//            }
//        })
//    .CreateLogger();

// Use Serilog as the logging provider
//builder.Host.UseSerilog();

//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
//{
//    containerBuilder.RegisterModule(new AutofacModule());
//});


// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger with JWT authentication
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter 'Bearer' followed by a space and your token.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[] { }
        }
    });
});

// Identity services
builder.Services.AddIdentityCore<SystemUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<SystemUser>>("CMS")
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
});

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
            ValidAudience = builder.Configuration["JwtOptions:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:Secret"]))
        };
    });

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddPresistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
