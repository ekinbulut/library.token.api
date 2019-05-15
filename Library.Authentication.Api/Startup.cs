using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Library.Authentication.Service;
using Library.Authentication.Service.Data;
using Library.Authentication.Service.Data.Repositories;
using Library.Authentication.Service.Dispatchers.Author;
using Library.Authentication.Service.Dispatchers.Book;
using Library.Authentication.Service.Dispatchers.Storage;
using Library.Authentication.Service.Settings;
using Library.Services.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RestSharp;
using Swashbuckle.AspNetCore.Swagger;

namespace Library.Authentication.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private const string Url = "/swagger/v1/swagger.json";
        private const string Title = "LibraryOS Gateway API";
        private const string Version = "v1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var tokenConfig = Configuration.GetSection(AppConfigConstants.TokenManagerConfigConstants)
                .Get<TokenManagerConfig>();
            AddAuthentication(services, tokenConfig);
            AddContext(services);
            AddOptions(services);
            AddComponents(services);
            AddServices(services);
            AddSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            // global cors policy
            app.UseCors(x => x
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Url, Title);
                c.RoutePrefix = string.Empty;
            });

            app.UseAuthentication();

            app.UseMvc();
        }

        /// <summary>
        ///     Adds the context.
        /// </summary>
        /// <param name="services">The services.</param>
        private void AddContext(IServiceCollection services)
        {
            // context configuration
            var context = Configuration.GetConnectionString("AuthContext");
            services.AddDbContext<AuthenticationContext>(o =>
                                                             o.UseSqlServer(
                                                                 context
                                                                 , b => b.MigrationsAssembly(
                                                                     "Library.Authentication.Api")));
            services.AddScoped<DbContext, AuthenticationContext>();
        }

        /// <summary>
        ///     Adds the services.
        /// </summary>
        /// <param name="services">The services.</param>
        private void AddServices(IServiceCollection services)
        {
            // configure DI for application services
            services.AddTransient<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<ITokenManager, TokenManager>();

            services.AddSingleton<IDispatcher, Dispatcher>();

            services.AddScoped<IBookServiceDispatcher, BookServiceDispatcher>();
            services.AddScoped<IAuthorServiceDispatcher, AuthorServiceDispatcher>();
            services.AddScoped<IStorageServiceDispatcher, StorageServiceDispatcher>();
        }

        private void AddSwagger(IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Version, new Info {Title = Title, Version = Version});
                var security = new Dictionary<string, IEnumerable<string>>
                               {
                                   {"Bearer", new string[] { }}
                               };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                                                  {
                                                      Description =
                                                          "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
                                                      , Name = "Authorization", In = "header", Type = "apiKey"
                                                  });
                c.AddSecurityRequirement(security);
            });
        }

        /// <summary>
        ///     Adds the authenticaton.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="config"></param>
        private static void AddAuthentication(IServiceCollection services, TokenManagerConfig config)
        {
            // configure strongly typed settings objects
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                                                            {
                                                                ValidateIssuer = true, ValidateAudience = true
                                                                , ValidateLifetime = true
                                                                , ValidateIssuerSigningKey = true
                                                                , ValidIssuer = config.Issuer
                                                                , ValidAudience = config.Audience
                                                                , IssuerSigningKey =
                                                                    new SymmetricSecurityKey(
                                                                        Encoding.UTF8.GetBytes(config.Secret))
                                                            };
                    }
                );
        }

        private void AddOptions(IServiceCollection services)
        {
            services.Configure<TokenManagerConfig>(
                Configuration.GetSection(AppConfigConstants.TokenManagerConfigConstants));
            services.Configure<BookServiceEndPointConstants>(
                Configuration.GetSection(AppConfigConstants.BookServiceEndPointConstants));
            services.Configure<AuthorServiceEndPointConstants>(
                Configuration.GetSection(AppConfigConstants.AuthorServiceEndPointConstants));
            services.Configure<StorageServiceEndPointConstants>(
                Configuration.GetSection(AppConfigConstants.StorageServiceEndPointConstants));
        }

        private void AddComponents(IServiceCollection services)
        {
            services.AddTransient<IRestClient, RestClient>();
            services.AddTransient<IRestRequest, RestRequest>();
        }
    }
}