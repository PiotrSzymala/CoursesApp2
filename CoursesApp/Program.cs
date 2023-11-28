using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using CoursesApp.Data;
using CoursesApp.Repositories;
using Microsoft.EntityFrameworkCore;
using CoursesApp.IoC;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Text.Json;

namespace CoursesApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Swagger //https://localhost:7154/swagger/index.html
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
            });
            builder.Services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            // Custom swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.UseAllOfToExtendReferenceSchemas();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { Description = "JWT Authorization header using the Bearer scheme.", Type = SecuritySchemeType.Http, Scheme = "bearer" });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement { { new OpenApiSecurityScheme { Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme } }, new List<string>() } });
            });
            #endregion

            #region HealthCheck
            builder.Services.AddHealthChecks()
                .AddSqlServer(
                    connectionString: builder.Configuration.GetConnectionString("MsSqlConnectionString"),
                    healthQuery: "SELECT 1;");
            #endregion HealthCheck

            #region Auth
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidateActor = true,
                    ValidateTokenReplay = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new Exception("JWT key is required"))),
                };
            });
            #endregion

            #region EF core - DbContext registrations
            builder.Services.AddTransient<MsSqlAppDbContextConnectionString>();
            builder.Services.AddDbContext<ApplicationDbContext>((sp, o) =>
            {
                var connectionString = sp.GetRequiredService<MsSqlAppDbContextConnectionString>().Value;
                o.UseSqlServer(connectionString);
            });
            #endregion


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            #region Connector Database migrations
            app.UseDatabaseMigrations();
            #endregion

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            #region Use swagger
            app.UseSwagger();
            var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    c.RoutePrefix = "swagger";
                    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                }
            });
            #endregion

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            #region UseHealthChecks
            app.MapHealthChecks("/hc", new()
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(
                        new
                        {
                            status = report.Status.ToString(),
                            checks = report.Entries.Select(e => new
                            {
                                name = e.Key,
                                status = e.Value.Status.ToString(),
                                exception = e.Value.Exception?.Message,
                                duration = e.Value.Duration.ToString()
                            })
                        }));
                }
            });
            #endregion UseHealthChecks

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
