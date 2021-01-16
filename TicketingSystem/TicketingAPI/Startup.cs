using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingAPI.ContextData;
using TicketingAPI.Repositories;
using TicketingAPI.Repositories.Interfaces;
using TicketingAPI.Services;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _cors = "ticketing";
        }

        public IConfiguration Configuration { get; }
        private readonly string _cors;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(_cors, builder =>
            //    {
            //        builder.AllowAnyOrigin()
            //        .AllowAnyHeader()
            //        .AllowAnyMethod();
            //    });
            //});

            services.AddDbContext<ticketingContext>(
                options => options.UseNpgsql(Configuration.GetConnectionString("ticketingDB"))
            );

            services.AddScoped<IClaimsRepository, ClaimsRepository>();
            services.AddScoped<IOrganizatorRepository, OrganizatorRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAdministratorRepository, AdministratorRepository>();
            services.AddScoped<IPlaceRepository, PlaceRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ISeatRepository, SeatRepository>();
            services.AddScoped<IEventSeatRepository, EventSeatRepository>();

            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
            services.AddScoped<IHashService, HashService>();
            services.AddScoped<IRegisterUserService, RegisterUserService>();
            services.AddScoped<IOrganizatorService, OrganizatorService>();
            services.AddScoped<IAdministratorService, AdministratorService>();
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IClaimsService, ClaimsService>();
           
            services.AddControllers();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
                .AddJwtBearer("JwtBearer", jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Security:SymmetricKey")?.Value)),
                        ValidateLifetime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(
                builder =>
                {
                    //builder.WithOrigins("http://localhost:56813");
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                }
            );

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
