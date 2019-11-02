using System.IO;
using System.Text;
using System.Threading.Tasks;
using LabDayBackend.Models;
using LabDayBackend.Models.Logic;
using LabDayBackend.Repositories;
using LabDayBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LabDayBackend
{
    public class Startup
    {
        public static byte[] JWTSecret { get; private set; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LabDayContext>(options => options.UseSqlite("Data Source=database.db"));
            
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = 
                new DefaultContractResolver() { NamingStrategy = new SnakeCaseNamingStrategy() }
            );

            var serializedSecrets = File.ReadAllText("secrets.json", Encoding.UTF8);
            var secrets = JsonConvert.DeserializeObject<Secrets>(serializedSecrets);

            var key = Encoding.ASCII.GetBytes(secrets.JWTSecret);
            JWTSecret = key;

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var authService = context.HttpContext.RequestServices.GetRequiredService<IAuthService>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = authService.GetById(userId);
                        if (user == null) context.Fail("Unauthorized");
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(JWTSecret),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IUpdateTimeRepository, UpdateTimeRepository>();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
