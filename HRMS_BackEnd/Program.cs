using HRMS_BackEnd.Controllers;
using HRMS_BackEnd.Database.Context;
using HRMS_BackEnd.Middleware;
using HRMS_BackEnd.Repositories.Attendace_Repository;
using HRMS_BackEnd.Repositories.EmployeeRepository;
using HRMS_BackEnd.Repositories.PendingStatus_Repository;
using HRMS_BackEnd.Repositories.RolePosition_Repository;
using HRMS_BackEnd.Repositories.TokenRepository;
using HRMS_BackEnd.Repositries.LeaveRepository;
using HRMS_BackEnd.Repositries.LeaveRepositry;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace HRMS_BackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Logging and Debugging

            var logger = new LoggerConfiguration()
                .WriteTo.File("C:\\Users\\Issa Alshaban\\Desktop\\HRMS Project\\BackEnd\\HRMS_BackEnd\\HRMS_BackEnd\\Trace\\trace_ASP.net HRMS.txt",
                rollingInterval: RollingInterval.Hour) //Type of log to be written on
                .MinimumLevel.Debug() //Levels of debug 0 1 2
                .CreateLogger();  //In order to be able to create the log; 

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            // Add services to the container.

            builder.Services.AddControllers();

            //API versioning
            builder.Services.AddApiVersioning(options =>
            options.AssumeDefaultVersionWhenUnspecified = true

            );

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Database HRMS Connection String and Initiation connection
            builder.Services.AddDbContext<HrmsDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("HRMS_DB_ConnectionString")));

            builder.Services.AddDbContext<HrmsAuthDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Auth_Autho_DB_ConnectionString")));

            //Added dependency injections
            builder.Services.AddScoped<IEmployeeRespository, SqlEmployeeRepository>();
            builder.Services.AddScoped<ILeaveRepository, SqlLeaveRepository>();
            builder.Services.AddScoped<IJwtTokenRepo, TokenRepo>();
            builder.Services.AddScoped<IPendingRepository, SqlPendingStatusHandler>();
            builder.Services.AddScoped<IRoleRepository, SqlRoleRepository>();
            builder.Services.AddScoped<IAttendanceRepository, SqlAttendanceRepository>();


            builder.Services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("HRMS_Provider")
                .AddEntityFrameworkStores<HrmsAuthDbContext>()
                .AddDefaultTokenProviders();


            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                
            });

            //Authentication methods
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //Error handler middleware
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}