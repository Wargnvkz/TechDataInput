using Blazored.LocalStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechDataInput.Client.Services;
using TechDataInput.Components;
using TechDataInput.DB;

namespace TechDataInput
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();
            builder.Services.AddScoped<MeasurementSessionForm>();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddHttpClient();
            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.MapControllers();
            app.MapGet("/debug/routes", ([FromServices] IEnumerable<EndpointDataSource> endpointDataSources) =>
            {
                try
                {
                    //app.Logger.LogDebug("Accessing endpoint data sources");
                    var routes = new List<string>();

                    foreach (var dataSource in endpointDataSources)
                    {
                        foreach (var endpoint in dataSource.Endpoints)
                        {
                            var httpMethodMetadata = endpoint.Metadata.GetMetadata<IHttpMethodMetadata>();
                            var routeEndpoint = endpoint as RouteEndpoint;

                            string displayName = endpoint.DisplayName ?? "Unnamed endpoint";
                            if (httpMethodMetadata != null && routeEndpoint != null)
                            {
                                var methods = string.Join(",", httpMethodMetadata.HttpMethods);
                                var pattern = routeEndpoint.RoutePattern.RawText;
                                displayName = $"HTTP: {methods} {pattern}";
                            }

                            routes.Add(displayName);
                        }
                    }

                    if (!routes.Any())
                    {
                        //app.Logger.LogInformation("No routes found");
                        return Results.Ok("No routes found.");
                    }

                    //app.Logger.LogDebug("Returning {Count} routes", routes.Count);
                    return Results.Ok(string.Join("\n", routes));
                }
                catch (Exception ex)
                {
                    //app.Logger.LogError(ex, "Error retrieving routes: {Message}", ex.Message);
                    return Results.Problem("Failed to retrieve routes: " + ex.Message, statusCode: 500);
                }
            });


            app.Run();
        }
    }
}
