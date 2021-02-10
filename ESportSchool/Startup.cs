using System;
using System.Drawing;
using System.Text;
using ESportSchool.DAL.Repositories;
using ESportSchool.Domain.Repositories;
using ESportSchool.Services;
using ESportSchool.Services.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace ESportSchool.Web
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
            Console.WriteLine(Configuration["Jwt:Key"]);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });
            
            services.AddControllersWithViews();
            services.AddRazorPages();
            
            //add repositories
            services.AddScoped<ICoachRepository, CoachRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IGuideRepository, GuideRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IScheduleIntervalRepository, ScheduleIntervalRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ITrainingRepository, TrainingRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            //add services
            services.AddScoped<PaymentService>();
            services.AddScoped<ScheduleService>();
            services.AddScoped<TrainingService>();
            services.AddScoped<UserService>();
            services.AddScoped<TeamService>();

            services.AddScoped<DAL.ESportSchoolDbContext>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
