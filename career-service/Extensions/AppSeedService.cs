using career_service.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace career_service.Extensions;

public class AppSeedService
{
    public static async Task SeedDatabase(WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
        try
        {
            // Migrate the database, create if it doesn't exist
            context.Database.Migrate();
            await Seed.SeedData(context, publishEndpoint);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, " A problem ocurred during seeding ");
        }
    }
    
}