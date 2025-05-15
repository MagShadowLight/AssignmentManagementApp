
using AssignmentManagementApp.Core;
using AssignmentManagementApp.Core.Interfaces;

namespace AssignmentManagementApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.
            builder.Services.AddSingleton<IAssignmentFormatter, AssignmentFormatter>();
            builder.Services.AddSingleton<IAppLogger, ConsoleAppLogger>();
            builder.Services.AddSingleton<IAssignmentService, AssignmentService>();
            //new AssignmentService(new AssignmentFormatter(), new ConsoleAppLogger());
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
