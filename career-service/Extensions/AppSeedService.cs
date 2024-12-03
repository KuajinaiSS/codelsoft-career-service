using career_service.Data;
using Microsoft.EntityFrameworkCore;

namespace career_service.Extensions;

public class AppSeedService
{
    public static void SeedDatabase(WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        try
        {
            // Migrate the database, create if it doesn't exist
            context.Database.Migrate();
            Seed.SeedData(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, " A problem ocurred during seeding ");
        }
    }
    
}