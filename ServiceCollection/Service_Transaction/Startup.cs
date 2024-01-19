using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Shared.Data.Context;
using Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
namespace Service_Transaction
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
            services.AddRepository();
            services.AddDbContext<ActuatorContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddCors(corsOpt =>
            {
                corsOpt.AddPolicy(name: "MyPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:5209").AllowCredentials()
                        .AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(ori => true);
                });
            });
            services.AddControllers().AddNewtonsoftJson();
            services.AddAuthentication().AddJwtBearer(Configuration["JwtAccess:AuthProviderKey"], options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtAccess:Key"])),
                    ValidAudience = Configuration["JwtAccess:Audience"],
                    ValidIssuer = Configuration["JwtAccess:Issuer"],
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(Configuration["JwtAccess:AuthProviderKey"])
                    .Build();
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service_Transaction", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("MyPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service_Transaction"));
            }

            app.UseRouting();
            app.UseAuthorization();
            //app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute("default", "{controller?}/{action?}/{id?}/{vara?}/{varb?}/{varc?}/{vard?}");
                endpoints.MapControllers();
            });
        }
    }

}

