using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using VMMC.IServices;
using VMMC.Models;
using VMMC.Services;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using VMMC.IServices.ILogin;
using VMMC.LoginService;

namespace VMMC.Middleware
{
    public static class ServicesCollection
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionstring = configuration.GetConnectionString("DBCONN");
            services.AddDbContext<VmmcContext>(Options => Options.UseSqlServer(connectionstring));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                try
                {
                    var Key = Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWT:Key"));
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false, // on production make it true
                        ValidateAudience = false, // on production make it true
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration.GetValue<string>("JWT:Issuer"),
                        ValidAudience = configuration.GetValue<string>("JWT:Audience"),
                        IssuerSigningKey = new SymmetricSecurityKey(Key),
                        ClockSkew = TimeSpan.Zero
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An exception occurred: " + ex.Message);
                }
                o.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Voluntary Medical Male Circumcision core (Producer)", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                                        {
                                            new OpenApiSecurityScheme
                                            {
                                                Reference=new OpenApiReference
                                                {
                                                    Type=ReferenceType.SecurityScheme,
                                                    Id="Bearer",
                                                }
                                            },
                                            new string[]{"test"}
                                        }
                                    });
                                 });
            services.AddScoped <IHashingAndSalting,HashPassword> ();
            services.AddScoped <IUserServiceRepository,LoginManagementService >();
            services.AddScoped<IJWTManagerRepository,JWTManagerRepository > ();
            services.AddScoped<IApplicationUser, ApplicationService>();
            services.AddScoped<IMaster, MasterService>();
        }

    }
}
