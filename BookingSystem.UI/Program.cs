


namespace BookingSystem.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Service Dependecy Injection
           

            #endregion


            // Add services to the container.[
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<BookingSystemDlsContext>();
         
            builder.Services.AddHttpClient();
       

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=BookingCode}/{action=Index}");
            //{ id ?}

            app.Run();
        }
    }
}
