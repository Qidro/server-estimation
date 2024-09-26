
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using server_estimation.DataAccess;
using server_estimation.Models;
using System.ComponentModel.DataAnnotations;


namespace server_estimation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // builder.Services.AddTransient<EmailSender>();
            // Add services to the container.
            //builder.Services.AddTransient<IEmailSender, EmailSender>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<EstimationDbContext>();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy => {
                    policy.WithOrigins("http://localhost:3000");
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });   
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();
            app.UseAuthorization();
            //builder.Services.AddIdentity<Users, IdentityRole>()
            //    .AddEntityFrameworkStores<EstimationDbContext>();

            app.MapControllers();

            app.Run();
        }
    }
}
