using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WhatsOn.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WhatsOn
{
    public class Startup
    {
        //create IConfigurationRoot instance. represents the enrty point to the configuration data
        private IConfigurationRoot _configurationRoot;
        //constructor with hostingEnvironment parameter.
        //hostingEnvironment gives information about where the application is running
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            //instantiate ConfigurationBuilder instance
            _configurationRoot = new ConfigurationBuilder()
               //point it to the root of my application
               .SetBasePath(hostingEnvironment.ContentRootPath)
               //point it to the json file that contains the app settings
               .AddJsonFile("appsettings.json")
               //pass it the configuration using the build method. This results in a value per collection 
               //from appDbConext(Events,Categories,MyEventItems) and the connection string from appsettings
               .Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //register services
            //make sure SqlServer is being used(UseSqlServer) and pass from the configuration the connection string.
            //Takes AppDbContext action parameter and uses 'Default Connection' from appsettings.json
            services.AddDbContext<AppDbContext>(options =>
                                         options.UseSqlServer(_configurationRoot.GetConnectionString("DefaultConnection")));

            //Specify that I'm using the identity Service with built in classes IdentityUser, IdentityRole
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            //removed the 'mock' from MockCategoryRepository and MockPieRepository to reference the database
            //add transient method results in a new instance CategoryRepository being returned eveytime its requested
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddTransient<IEventRepository, EventRepository>();
            //allows you to work with the context
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //AddScoped allows you to create an object associated with a request.It's differenet between different requests
            //When you browse to the site you get an instance of the shooping cart and
            //When I browse to the site I get a different instance of the shooping cart
            services.AddScoped<MyEvent>(sp => MyEvent.GetEventList(sp));
            //adds Mvc
            services.AddMvc();
            //session specific
            //enable working with ApplicationSession state
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //adds support for showing exceptions in the browser when something goes wroung
            app.UseDeveloperExceptionPage();
            //allows us to handle response staus codes between 400 and 600!
            app.UseStatusCodePages();
            //allows us to serve static files
            app.UseStaticFiles();
            //allow working with sessions. must be placed above app.UseMvcWithDefaultRoute() or it won't work 
            app.UseSession();
            //allow working with Identity
            app.UseIdentity();
            //adds support for mvc with a basic route
            //app.UseMvcWithDefaultRoute();
            //app.UseMvc() 
            app.UseMvc(routes =>
            {
                //more specific routes at the top
                routes.MapRoute(
                  name: "categoryfilter",
                  template: "Event/{action}/{category?}",
                  defaults: new { Controller = "Event", action = "List" });

                routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
            });
            //every time the startup is called(called automatically and so is the configure method) the 
            //seed method will be invoked and it will check if the dbintialiizer is there or not
            //applicationBuilder is passed in to here and it will execute the code
            DbInitializer.Seed(app);
        }
    }
}
