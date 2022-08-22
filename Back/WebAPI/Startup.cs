using AutoMapper;
using DAL.Data;
using DAL.DataBaseContexts;
using DAL.Interfaces;
using DAL.IService.Account;
using DAL.Repositories;
using DAL.Service.Account;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Infrastructure;

namespace WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddAuthentication(options =>
                 {
                     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                     options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKeys = ApplicationTokens.Tokens.Values,
                        ValidIssuer = "GLS",
                        ValidAudiences = ApplicationTokens.Tokens.Keys,
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            //Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            //Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                            return Task.CompletedTask;
                        },
                        OnMessageReceived = context =>
                        {
                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddControllers();
            services.AddDbContext<GLSTablesDataBaseContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(GLSTablesDataBaseContext).Assembly.FullName)));

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            #region Repositories
            //services.AddTransient<DbContext, GLSTablesDataBaseContext>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserLoginLogRepository, UserLoginLogRepository>();
            services.AddHttpContextAccessor();
            #endregion
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            using (var serviceScope = app.Services.CreateScope())
            {                
                try
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<GLSTablesDataBaseContext>();
                    DbInitializer.Initialize(context);
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
            app.UseCors("corsapp");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}