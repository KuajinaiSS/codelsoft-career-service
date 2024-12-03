using System.Text.Json;
using career_service.Models;

namespace career_service.Data;

public class Seed
{
    /// <summary>
    /// Seed the database with examples models in the json files if the database is empty.
    /// </summary>
    /// <param name="context">Database Context </param>
    public static void SeedData(DataContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        CallEachSeeder(context, options);
    }

    /// <summary>
    /// Centralize the call to each seeder method
    /// </summary>
    /// <param name="context">Database context</param>
    /// <param name="options">Options to deserialize json</param>
    public static void CallEachSeeder(DataContext context, JsonSerializerOptions options)
    {
        SeedFirstOrderTables(context, options);
        SeedSecondtOrderTables(context, options);
    }

    /// <summary>
    /// Seed the database with the tables that don't depend on other tables.
    /// </summary>
    /// <param name="context">Database context</param>
    /// <param name="options">Options to deserialize json</param>
    private static void SeedFirstOrderTables(DataContext context, JsonSerializerOptions options)
    {
        SeedCareers(context, options);
    }

    /// <summary>
    /// Seed the database with the tables whose data depends on exatly one table.
    /// </summary>
    /// <param name="context">Database context</param>
    /// <param name="options">Options to deserialize json</param>
    private static void SeedSecondtOrderTables(DataContext context, JsonSerializerOptions options)
    {
    }
    
    private static void SeedCareers(DataContext context, JsonSerializerOptions options)
    {
        var result = context.Careers?.Any();
        if (result is true or null) return;
        var path = "Data/CareersData.json";
        var careersData = File.ReadAllText(path);
        var careersList = JsonSerializer.Deserialize<List<Career>>(careersData, options) ??
                          throw new Exception("CareersData.json is empty");
        // Normalize the name and code of the careers
        careersList.ForEach(s =>
        {
            s.Name = s.Name.ToLower();
        });

        context.Careers?.AddRange(careersList);
        context.SaveChanges();
    }
    
    
    
}