using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TransferService.Contract;
using TransferService.Data;
using TransferService.Services.Interfaces;


namespace TransferService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApiMappingProfile), typeof(DataMappingProfile));
            services.AddScoped(typeof(ITransferService), typeof(Services.TransferService));
            services.AddScoped(typeof(ITransferRepository), typeof(TransferRepository));
            services.AddDbContext<TransferDbContext>
             (options => options
             .UseSqlServer(Configuration.GetConnectionString("TransferConnectionString")));
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200");
                        builder.AllowCredentials();
                        builder.AllowAnyHeader();
                        builder.WithMethods("GET", "POST", "PUT");

                    });

            });

            var swaggerTitle = Configuration["Swagger:Title"];
            var swaggerName = Configuration["Swagger:Name"];
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerName, new OpenApiInfo()
                {
                    Title = swaggerTitle,
                    Description = Configuration["Swagger:OpenApiContact:Description"],
                    Contact = new OpenApiContact()
                    {
                        Name = Configuration["Swagger:OpenApiContact:Name"],
                        Email = Configuration["Swagger:OpenApiContact:Email"]
                    }
                });
            });
        }
    

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var swaggerName = Configuration["Swagger:Name"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("MyPolicy");

            }
            //app.UseErrorHandlingMiddleware();

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{swaggerName}/swagger.json", swaggerName);
            });
            app.UseRouting();
            //app.UseAuthentication();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
