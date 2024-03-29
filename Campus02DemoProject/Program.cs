
using Azure.Storage.Blobs;
using Campus02DemoProject.Models;
using Campus02DemoProject.Services;
using Microsoft.EntityFrameworkCore;

namespace Campus02DemoProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("TodoList"));
            builder.Services.AddSingleton<ICalcService,CalcService>();
            builder.Services.AddSingleton(x => new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName="));
            builder.Services.AddSingleton<ICCBlobService, CCBlobService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
           

            var app = builder.Build();

            // Configure the HTTP request pipeline.
           // if (app.Environment.IsDevelopment())
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