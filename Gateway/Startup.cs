using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

public class Startup
{
    public IConfiguration Configuration { get; }
    public IConfiguration OcelotConfiguration { get; }
    public Startup(IConfiguration configuration, IHostEnvironment env)
    {
        Configuration = configuration;
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(env.ContentRootPath)
               .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
               .AddEnvironmentVariables();
        OcelotConfiguration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(corsOpt => {
            corsOpt.AddPolicy(name: "MyPolicy", builder => {
                builder.WithOrigins("http://localhost:1302").AllowCredentials()
                    .AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(ori => true);
            });
        });

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

        services.AddOcelot(OcelotConfiguration);
        services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors("MyPolicy");
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute("default", "{controller?}/{action?}/{id?}/{vara?}/{varb?}/{varc?}/{vard?}");
        });
        app.UseOcelot().Wait();
    }
}
