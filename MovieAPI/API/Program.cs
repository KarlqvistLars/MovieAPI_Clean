using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Movie_.API.Services;
using Movie_.Contracts;
using Movie_.Core.Data.Repositories;
using Movie_.Core.DomainContracts;
using Movie_.Core.Services;
using Movie_.Data;
using Movie_.Data.Seed;
using System.Reflection;
using ApiVersion = Asp.Versioning.ApiVersion;

namespace Movie_.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Lokalicera appsettings.json
            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            // Koppla upp mot SQL Server-database
            var connectionString = builder.Configuration.GetConnectionString("Movie2APIContext") ?? throw new InvalidOperationException("Connection string 'Movie2APIContext' not found.");
            builder.Services.AddDbContext<Movie2APIContext>(options => options.UseSqlServer(connectionString));
            // Add services to the container.
            builder.Services.AddControllers()
                .AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            // Registrera Interfacen, Service och DbContext för att kunna mocca för xUnit-tester
            builder.Services.AddScoped<IMovie2APIContext, Movie2APIContext>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<IActorService, ActorService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IMovieRepository, MovieRepository>();
            builder.Services.AddScoped<IActorRepository, ActorRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

            builder.Services.AddApiVersioning(options => {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            }).AddApiExplorer(options => {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

            // Swagger
            builder.Services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Movie2 API",
                    Version = "v1",
                    Description = "API för hantering av filmer, skådespelare och recensioner.",
                    Contact = new OpenApiContact {
                        Name = "Lars Karlqvist",
                        Email = "example@email.se",
                        Url = new Uri("https://www.larskarlqvist.se")
                    },
                    License = new OpenApiLicense {
                        Name = "Apache 2.0 License",
                        Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0")
                    }
                });
                options.SwaggerDoc("v2", new OpenApiInfo {
                    Title = "Movies2 API",
                    Version = "v2",
                    Description = "API för hantering av filmer, skådespelare och recensioner.",
                    Contact = new OpenApiContact {
                        Name = "Lars Karlqvist",
                        Email = "example@email.se",
                        Url = new Uri("https://www.larskarlqvist.se")
                    },
                    License = new OpenApiLicense {
                        Name = "Apache 2.0 License",
                        Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0")
                    }
                });
                options.DocInclusionPredicate((docName, apiDesc) => {
                    return apiDesc.GroupName == docName;
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

                options.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            // Seed-logik
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<Movie2APIContext>();
                try
                {
                    // Säkerställer att databasen finns och är migrerad
                    context.Database.Migrate();
                } catch
                {
                    Console.WriteLine("Något fel uppstod vid kontroll ifall databasen existerar. \nSe till att SQL Server är igång och att anslutningssträngen är korrekt.");
                }
                // Anropar din seed-metod
                DbSeeder.Initialize(context);
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options => {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Movies2 API v1");
                    options.SwaggerEndpoint("/swagger/v2/swagger.json", "Movies2 API v2");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
