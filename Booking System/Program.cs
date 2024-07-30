
using BookingSystem.DataAccess.Models;
using BookingSystem.Provider;

namespace Booking_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.Limits.MaxRequestBodySize = 104857600; // 100 MB
            });

            // Add services to the container.

            builder.Services.AddControllersWithViews();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<BookingCodeProvider>();
            builder.Services.AddScoped<BookingSystemDlsContext>();
            builder.Services.AddScoped<ResourceProvider>();
            builder.Services.AddScoped<RoomProvider>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:5196") // Ganti dengan asal yang sesuai
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }



            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();


            app.UseCors("AllowSpecificOrigin");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }


    }
}
