﻿using System;
using System.Net;
using System.Text;
using MeetingDoc.Api.Data;
using MeetingDoc.Api.Data.Interfaces;
using MeetingDoc.Api.Data.Repositories;
using MeetingDoc.Api.Data.Repositories.Interfaces;
using MeetingDoc.Api.Helpers;
using MeetingDoc.Api.Managers;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.Validators;
using MeetingDoc.Api.Validators.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql;

namespace MeetingDoc.Api
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
            var connectionString = Configuration.GetConnectionString("Default");

            services.AddDbContextPool<DataContext>(options => options.UseMySql(connectionString));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors();
            services.AddTransient<Seed>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMeetingTypeRepository, MeetingTypeRepository>();
            services.AddScoped<IMeetingTopicRepository, MeetingTopicRepository>();
            services.AddScoped<IMeetingTimeRepository, MeetingTimeRepository>();
            services.AddScoped<IMeetingAgendaRepository, MeetingAgendaRepository>();
            services.AddScoped<IMeetingContentRepository, MeetingContentRepository>();
            services.AddScoped<IMeetingNoteRepository, MeetingNoteRepository>();
            services.AddScoped<IMeeitngAgendaUserRepository, MeeitngAgendaUserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IUserValidator, UserValidator>();
            services.AddScoped<IMeetingTypeManager, MeetingTypeManager>();
            services.AddScoped<IMeetingTypeValidator, MeetingTypeValidator>();
            services.AddScoped<IMeetingTopicManager, MeetingTopicManager>();
            services.AddScoped<IMeetingTopicValidator, MeetingTopicValidator>();
            services.AddScoped<IMeetingTimeManager, MeetingTimeManager>();
            services.AddScoped<IMeetingTimeValidator, MeetingTimeValidator>();
            services.AddScoped<IMeetingAgendaManager, MeetingAgendaManager>();
            services.AddScoped<IMeetingAgendaValidator, MeetingAgendaValidator>();
            services.AddScoped<IMeetingContentManager, MeetingContentManager>();
            services.AddScoped<IMeetingContentValidator, MeetingContentValidator>();
            services.AddScoped<IMeetingNoteManager, MeetingNoteManager>();
            services.AddScoped<IMeetingNoteValidator, MeetingNoteValidator>();
            services.AddScoped<IMeetingScheduleManager, MeetingScheduleManager>();
            services.AddScoped<IEmailManager, EmailManager>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Seed seeder)
        {
            if (false && env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }

            //app.UseHttpsRedirection();
            seeder.SeedUsers();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Fallback", action = "Index" }
                );
            });
        }
    }
}
