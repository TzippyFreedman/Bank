using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using UserService.Api.Middlewares;
using UserService.Data;
using UserService.Services;

namespace UserService.Api
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
            services.AddAutoMapper(typeof(ApiMappingProfile), typeof(DataMappingProfile));
            services.AddScoped(typeof(IUserService), typeof(UserService.Services.UserService));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(smtpSender => Configuration.GetSection("AppSettings:SmtpSettings").Get<SmtpSettings>());
            services.AddDbContext<UserDbContext>
              (options => options
              .UseSqlServer(Configuration.GetConnectionString("BankUserServiceConnectionString")));
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

            var swaggerTitle = Configuration["AppSettings:Swagger:Title"];
            var swaggerName = Configuration["AppSettings:Swagger:Name"];
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerName, new OpenApiInfo()
                {
                    Title = swaggerTitle,
                    Description = Configuration["AppSettings:Swagger:OpenApiContact:Description"],
                    Contact = new OpenApiContact()
                    {
                        Name = Configuration["AppSettings:Swagger:OpenApiContact:Name"],
                        Email = Configuration["AppSettings:Swagger:OpenApiContact:Email"]
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var swaggerName = Configuration["AppSettings:Swagger:SwaggerName"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("MyPolicy");

            }
            app.UseErrorHandlingMiddleware();
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{swaggerName}/swagger.json", swaggerName);
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
