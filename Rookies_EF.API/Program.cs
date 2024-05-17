using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Rookies_EF.API.Dtos;
using Rookies_EF.API.Services;
using Rookies_EF.Common.GenericRepository;
using Rookies_EF.Infrastructure.Repositories;
using Rookies_EFCore.Infrastructure.Models;

namespace Rookies_EF.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<RookiesEFDBContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyDB"))
            );

            // Add services to the container.
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //DI for repo
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<ISalariesRepository, SalariesRepository>();
            builder.Services.AddScoped<IProjectEmployeeRepository, ProjectEmployeeRepository>();
            //DI for service
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<ISalariesService, SalariesService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IProjectEmployeeService, ProjectEmployeeService>();
            //configure auto mapper
            builder.Services.AddAutoMapper(typeof(MapperProfile));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
        }
    }
}