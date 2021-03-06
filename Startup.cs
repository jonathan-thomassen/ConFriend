using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ConFriend.Interfaces;
using ConFriend.Models;
using ConFriend.Services;

namespace ConFriend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddSession();

            services.AddTransient<ICrudService<Conference>, CRUD_Service<Conference>>();
            services.AddTransient<ICrudService<Enrollment>, CRUD_Service<Enrollment>>();
            services.AddTransient<ICrudService<Event>, CRUD_Service<Event>>();
            services.AddTransient<ICrudService<EventTheme>, CRUD_Service<EventTheme>>();
            services.AddTransient<ICrudService<Feature>, CRUD_Service<Feature>>();
            services.AddTransient<ICrudService<Floor>, CRUD_Service<Floor>>();
            services.AddTransient<ICrudService<Room>, CRUD_Service<Room>>();
            services.AddTransient<ICrudService<RoomFeature>, CRUD_Service<RoomFeature>>();
            services.AddTransient<ICrudService<RoomSeatCategory>, CRUD_Service<RoomSeatCategory>>();
            services.AddTransient<ICrudService<SeatCategory>, CRUD_Service<SeatCategory>>();
            services.AddTransient<ICrudService<SeatCategoryTaken>, CRUD_Service<SeatCategoryTaken>>();
            services.AddTransient<ICrudService<Speaker>, CRUD_Service<Speaker>>();
            services.AddTransient<ICrudService<Theme>, CRUD_Service<Theme>>();
            services.AddTransient<ICrudService<User>, CRUD_Service<User>>();
            services.AddTransient<ICrudService<Venue>, CRUD_Service<Venue>>();
            services.AddTransient<ICrudService<UserConferenceBinding>, CRUD_Service<UserConferenceBinding>>();

            services.AddTransient<SessionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
