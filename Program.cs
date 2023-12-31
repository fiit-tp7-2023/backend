using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Neo4jClient;
using TAG.DTOS.Mappings;
using TAG.Services;
using TAG.Services.Interfaces;

namespace TAG
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.SetBasePath(builder.Environment.ContentRootPath);
            builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            builder.Configuration.AddJsonFile(
                $"appsettings.{builder.Environment.EnvironmentName}.json",
                optional: true
            );
            builder.Configuration.AddEnvironmentVariables();

            // Services configuration
            if (builder.Environment.IsProduction() && builder.Configuration["DeploymentEnv"] != "AZURE")
            {
                builder.Services
                    .AddDataProtection()
                    .PersistKeysToFileSystem(new DirectoryInfo(builder.Configuration["DataProtection:StoragePath"]!))
                    .ProtectKeysWithCertificate(
                        new X509Certificate2(Convert.FromBase64String(builder.Configuration["DataProtection:Key"]!))
                    );
            }

            builder.Services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(TransactionProfile));
            builder.Services.AddAutoMapper(typeof(NFTProfile));

            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<ITagService, TagService>();
            builder.Services.AddScoped<INFTService, NFTService>();

            builder.Services.AddControllers();

            // Neo4j
            var neo4jUri = builder.Configuration["Neo4j:Uri"];
            var neo4jUsername = builder.Configuration["Neo4j:Username"];
            var neo4jPassword = builder.Configuration["Neo4j:Password"];
            var neo4jClient = new BoltGraphClient(new Uri(neo4jUri!), neo4jUsername, neo4jPassword);
            neo4jClient.ConnectAsync();
            builder.Services.AddSingleton<IGraphClient>(neo4jClient);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer"
                    }
                );
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme }
                            },
                            new List<string>()
                        }
                    }
                );
            });

            // Fluent validation
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();

            var googleClientId = builder.Configuration["Google:ClientId"];

            builder.Services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.Authority = "https://accounts.google.com";
                    x.Audience = googleClientId;

                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://accounts.google.com",
                        ValidateAudience = true,
                        ValidAudience = googleClientId,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
