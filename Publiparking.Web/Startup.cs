using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Publiparking.Core.Data.SqlServer;
using Publiparking.Web.Configuration;
using Publiparking.Web.Configuration.ValueProvider;
using Publiparking.Web.Models.oAuth2;
using Publiparking.Web.Policies.Resources;
using Publiparking.Web.Policies.Services;
using Publiparking.Web.Token;

namespace Publiparking.Web
{
    public class Startup
    {

        #region Public\Protected Properties

        /// <summary>
        /// OAUTH options property.
        /// </summary>
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }
        public string StartMode { get; private set; }
        public string Environment { get; private set; }
        public bool IsDevelopment { get; private set; }
        #endregion Public\Protected Properties
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}
        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();
            this.Configuration = builder.Build();
            this.IsDevelopment = false;
            this.IsDevelopment = env.IsDevelopment();
            this.Environment = env.EnvironmentName;
            AppSettings.Instance.SetConfiguration(Configuration);
        }
        public IConfiguration Configuration { get; }
        public static string DbParkConnectionString { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(CryptoParamsProtector));

            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));
            var jwtSettings = Configuration.GetSection("JwtSecurityToken").Get<JwtSettings>();

            var startMode = Configuration.GetSection("StartMode").Get<StartMode>();
            this.StartMode = startMode.DefaultHome;
            services.Configure<StartMode>
                        (Configuration.GetSection("StartMode"));

            services.AddControllersWithViews()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddRazorPages();
            services.AddControllers(options =>
                options.EnableEndpointRouting = false)
                .AddXmlSerializerFormatters()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            services.AddControllersWithViews();

            services.AddMvc(options =>
                    options.EnableEndpointRouting = false)
                .AddSessionStateTempDataProvider()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddMvc(mvcOptions =>
            {
                mvcOptions.ValueProviderFactories.Add(new CryptoValueProviderFactory());
            });

            //Prima configurazione base
            //services.AddWebOptimizer();

            services.AddWebOptimizer(pipeline =>
            {
                //File specifici
                pipeline.MinifyJsFiles(" js/adminlte.js ", " js/init.js ", " js/jquery.autocomplete.js ");
                //Tutti i file presenti nella cartella js
                //pipeline.MinifyJsFiles("js/**/*.js");
                //---------------------------------------------------------------------------
                pipeline.MinifyCssFiles("css/PubliparkingSheet.css");
                //Tutti i file presenti nella cartella css
                //pipeline.MinifyCssFiles("css/**/*.css");

                //-----------------------------------------------------------------------------
                //Unire più file in un unico file
                pipeline.AddCssBundle("/css/bundle.css", "css/PubliparkingSheet.css", "css/site.css");
            });
            //Per escludere la minificazione in sviluppo
            //if (this.IsDevelopment)
            //{
            //    services.AddWebOptimizer(minifyJavaScript: false, minifyCss: false);
            //}
            services.AddMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                //options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            //La funzionalità SamSite è disabilitata per la Gestione dei Cookie
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.Strict;
            //});
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
                if (startMode.DefaultHome.ToUpper() == "HOMEUTENTE")
                {
                    options.LoginPath = "/HomeUtente/Login";
                    options.LogoutPath = "/HomeUtente/LogOff";
                    //options.Cookie.SameSite = SameSiteMode.None;
                    //options.Cookie.MinimumSameSitePolicy = SameSiteMode.None;
                    options.AccessDeniedPath = "/HomeUtente/AccessDenied";
                }
                else
                {
                    options.LoginPath = "/HomeOperatore/Login";
                    options.LogoutPath = "/HomeOperatore/LogOff";
                    //options.Cookie.SameSite = SameSiteMode.None;
                    //options.Cookie.MinimumSameSitePolicy = SameSiteMode.None;
                    options.AccessDeniedPath = "/HomeOperatore/AccessDenied";
                }
                options.SlidingExpiration = true;
            });


            services.AddSingleton(Configuration);

            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();
            services.AddDistributedMemoryCache();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddDbContext<DbParkContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DbParkContext")));
            services.AddTransient<DatabaseConfig>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Publiparking", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT containing userid claim",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });

                var security =
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                },
                                UnresolvedReference = true
                            },
                            new List<string>()
                        }
                    };
                options.AddSecurityRequirement(security);
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Authenticated", policy =>
                {
                    policy.RequireClaim("authenticated", "1")
                    .RequireAuthenticatedUser()
                    .Build();
                });
            });
            services.AddDefaultIdentity<User>(
              options => options.SignIn.RequireConfirmedAccount = true)
              .AddRoles<Role>()
              .AddEntityFrameworkStores<DbParkContext>()
              .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1d);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = true;
                //options.Password.RequireLowercase = true;
                //options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                //options.User.AllowedUserNameCharacters =
                //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            // Registro l'handler di autorizzazione
            services.AddTransient<IAuthorizationHandler, CustomerAuthorizationHandler>();
            //services.AddTransient<IAuthorizationHandler, JobLevelRequirementHandler>();

            // Registro i permessi
            services.AddTransient<IPermissionService, PermissionService>();
            //services.AddTransient<IOrganizationService, OrganizationService>();
            services.AddAutoMapper(typeof(Startup));

            //Aggiunta del servizio CookieManager
            //services.AddCookieManager();
            services.AddCookieManager(options =>
            {
                //Consente la crittografia dei dati dei cookie (per impostazione predefinita consente la crittografica)
                options.AllowEncryption = false;
                //Lancia un Throw se not tutti i chunks del cookie sono disponibili nella request per il re-assembly.
                options.ThrowForPartialCookies = true;
                // Set a NULL se non permette di dividere in chunks
                //options.ChunkSize = null;
                //Default Cookie expire time if expire time set to null of cookie
                //default time is 1 day to expire cookie 
                options.DefaultExpireTimeInDays = 10;
            });

            services.AddSingleton<IConfigurationRoot>((IConfigurationRoot)Configuration);

            ConfigurationData.Configuration = Configuration;
            ConfigurationData.IsDevelopment = this.IsDevelopment;
            ConfigurationData.Environment = this.Environment;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //TODO: VERIFICA  E INTERCETTA GLI ERRORI
            app.UseExceptionHandler("/Error");
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseWebOptimizer();
            app.UseStaticFiles();

            #region CSS & JS
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //       Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\css"))
            //       ,
            //    RequestPath = new PathString("/css")
            //});

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //      Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\js"))
            //      ,
            //    RequestPath = new PathString("/js"),

            //});
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //      Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\js\plugins"))
            //      ,
            //    RequestPath = new PathString("/js/plugins"),

            //});
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //      Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\lib\bootstrap\dist\css"))
            //      ,
            //    RequestPath = new PathString("/lib/bootstrap/dist/css"),

            //});
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //      Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\fonts"))
            //      ,
            //    RequestPath = new PathString("/fonts"),
            //});
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //     Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images"))
            //     ,
            //    RequestPath = new PathString("/images"),

            //});
            #endregion CSS & JS

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            //CHIAMA LA CLASSE AuthExtensions
            //CHE GESTISCE SIA L'AUTENTICAZIONE CHE L'AUTORIZZAZIONE
            //app.UseAuth();
            app.UseAuthentication();
            app.UseCookiePolicy();
            //Add JWToken to all incoming HTTP Request Header
            app.Use(async (context, next) =>
            {
                var JWToken = context.Session.GetString("JWToken");
                if (!string.IsNullOrEmpty(JWToken))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
                }
                await next();
            });
            if (this.StartMode.ToUpper() == "HOMEUTENTE")
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=HomeUtente}/{action=Index}/{id?}");
                });
                app.UseMvcWithDefaultRoute();
                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=HomeUtente}/{action=Index}/{id?}");
                });
            }
            else
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=HomeOperatore}/{action=Index}/{id?}");
                });
                app.UseMvcWithDefaultRoute();
                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=HomeOperatore}/{action=Index}/{id?}");
                });
            }
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
