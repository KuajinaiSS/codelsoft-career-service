using System.Text.Json;
using career_service.Models;
using MassTransit;

namespace career_service.Data;

public class Seed
{
    /// <summary>
    /// Seed the database with examples models in the json files if the database is empty.
    /// </summary>
    /// <param name="context">Database Context</param>
    public static async Task SeedData(DataContext context, IPublishEndpoint publishEndpoint)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        await CallEachSeeder(context, options, publishEndpoint);
    }

    /// <summary>
    /// Centralize the call to each seeder method.
    /// </summary>
    public static async Task CallEachSeeder(DataContext context, JsonSerializerOptions options, IPublishEndpoint publishEndpoint)
    {
        await SeedFirstOrderTables(context, options, publishEndpoint);
        await SeedSecondOrderTables(context, options);
    }

    /// <summary>
    /// Seed the database with the tables that don't depend on other tables.
    /// </summary>
    private static async Task SeedFirstOrderTables(DataContext context, JsonSerializerOptions options, IPublishEndpoint publishEndpoint)
    {
        await SeedCareers(context, options);
        await SeedSubjects(context, options, publishEndpoint);
    }

    /// <summary>
    /// Seed the database with the tables whose data depends on exactly one table.
    /// </summary>
    private static async Task SeedSecondOrderTables(DataContext context, JsonSerializerOptions options)
    {
        SeedSubjectsRelationships(context, options);
    }

    private static async Task SeedCareers(DataContext context, JsonSerializerOptions options)
    {
        if (context.Careers?.Any() == true) return;

        var path = "Data/DataSeeders/CareersData.json";
        var careersData = await File.ReadAllTextAsync(path);
        var careersList = JsonSerializer.Deserialize<List<Career>>(careersData, options) ??
                          throw new Exception("CareersData.json is empty");

        // Normalize the name of the careers
        careersList.ForEach(c => c.Name = c.Name.ToLower());

        context.Careers?.AddRange(careersList);
        await context.SaveChangesAsync();
    }

    private static async Task SeedSubjects(DataContext context, JsonSerializerOptions options, IPublishEndpoint publishEndpoint)
    {
        if (context.Subjects?.Any() == true) return;

        var path = "Data/DataSeeders/SubjectsData.json";
        var subjectsData = await File.ReadAllTextAsync(path);
        var subjectsList = JsonSerializer.Deserialize<List<Subject>>(subjectsData, options) ??
                           throw new Exception("SubjectsData.json is empty");

        // Normalize the name, code, and department
        subjectsList.ForEach(s =>
        {
            s.Code = s.Code.ToLower();
            s.Name = s.Name.ToLower();
            s.Department = s.Department.ToLower();
        });

        context.Subjects?.AddRange(subjectsList);
        await context.SaveChangesAsync();

        // Publish each subject to RabbitMQ
        foreach (var subject in subjectsList)
        {
            await publishEndpoint.Publish(subject);
        }
    }

    private static void SeedSubjectsRelationships(DataContext context, JsonSerializerOptions options)
    {
        if (context.SubjectsRelationships?.Any() == true) return;

        var path = "Data/DataSeeders/SubjectsRelationsData.json";
        var subjectsRelationshipsData = File.ReadAllText(path);
        var subjectsRelationshipsList = JsonSerializer.Deserialize<List<SubjectRelationship>>(subjectsRelationshipsData, options) ??
                                        throw new Exception("SubjectsRelationsData.json is empty");

        // Normalize the codes
        subjectsRelationshipsList.ForEach(s =>
        {
            s.SubjectCode = s.SubjectCode.ToLower();
            s.PreSubjectCode = s.PreSubjectCode.ToLower();
        });

        context.SubjectsRelationships?.AddRange(subjectsRelationshipsList);
        context.SaveChanges();
    }
}
