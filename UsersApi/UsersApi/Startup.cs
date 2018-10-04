using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using NSwag.AspNetCore;
using UsersApi.Data;

namespace UsersApi
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
            services.AddMvc();

            ConfigureOpenApi(services);// adding the swagger configuration to the container

            services.AddDbContext<UsersApiContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("UsersApiContext")));
        }

        //configure Swagger(OpenApi)
        private void ConfigureOpenApi(IServiceCollection services)
        {
            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Register the Swagger generator and the Swagger UI middlewares
                app.UseSwaggerUi3WithApiExplorer(settings =>
                {
                    settings.GeneratorSettings.DefaultPropertyNameHandling =
                        PropertyNameHandling.CamelCase;

                    settings.GeneratorSettings.Description = "An user ASP.NET Core web api";
                    settings.GeneratorSettings.Title = "User Web Api";
                });
            }
            else
            {
                app.UseMvc();
            }
            app.UseHttpsRedirection();
            app.UseMvcWithDefaultRoute();
        }
    }
}
