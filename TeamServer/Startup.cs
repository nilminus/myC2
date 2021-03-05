using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NSwag.Generation.Processors.Security;
using TeamServer.Controllers;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace TeamServer
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
            services.AddControllers().AddNewtonsoftJson(j =>
            {
                j.SerializerSettings.ContractResolver = new DefaultContractResolver();
                j.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            services.AddSwaggerDocument(c =>
            {
                c.PostProcess = d =>
                {
                    d.Info.Version = "v0.1";
                    d.Info.Title = "myC2 API (essentially forked from Rastamouse)";
                    d.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Nil Minus",
                        Url = "http://github.com/nilminus"
                    };
                };
                c.DocumentProcessors.Add(new SecurityDefinitionAppender("Bearer", new NSwag.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "JWT Authorition header using the bearer scheme.",
                    Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
                    In = NSwag.OpenApiSecurityApiKeyLocation.Header
                }));
                c.OperationProcessors.Add(new OperationSecurityScopeProcessor("Bearer"));
            });

            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(j =>
            {
                j.RequireHttpsMetadata = false;
                j.SaveToken = true;
                j.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(AuthenticationController.JWTSecret),
                    ValidateIssuer = false,
                    ValidateAudience = false
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
