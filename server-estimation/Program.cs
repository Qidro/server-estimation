
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using server_estimation.DataAccess;
using server_estimation.SenderE;
using System.ComponentModel.DataAnnotations;

using IEmailSender = server_estimation.SenderE.IEmailSender;

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


            app.MapControllers();

            app.Run();
        }
    }
}
