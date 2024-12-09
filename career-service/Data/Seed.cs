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
        SeedSubjects(context, options);
    }

    /// <summary>
    /// Seed the database with the tables whose data depends on exatly one table.
    /// </summary>
    /// <param name="context">Database context</param>
    /// <param name="options">Options to deserialize json</param>
    private static void SeedSecondtOrderTables(DataContext context, JsonSerializerOptions options)
    {
        SeedSubjectsRelationships(context, options);
    }
    
    private static void SeedCareers(DataContext context, JsonSerializerOptions options)
    {
        var result = context.Careers?.Any();
        if (result is true or null) return;
        var path = "Data/DataSeeders/CareersData.json";
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
    
    private static void SeedSubjects(DataContext context, JsonSerializerOptions options)
    {
        var result = context.Subjects?.Any();
        if (result is true or null) return;

        var path = "Data/DataSeeders/SubjectsData.json";
        var subjectsData = File.ReadAllText(path);
        var subjectsList = JsonSerializer.Deserialize<List<Subject>>(subjectsData, options) ??
                           throw new Exception("SubjectsData.json is empty");
        // Normalize the name, code and department of the subjects
        subjectsList.ForEach(s =>
        {
            s.Code = s.Code.ToLower();
            s.Name = s.Name.ToLower();
            s.Department = s.Department.ToLower();
        });

        context.Subjects?.AddRange(subjectsList);
        context.SaveChanges();
    }
    
    /// <summary>
    /// Seed the database with the subjects relationships in the json file and save changes if the database is empty.
    /// </summary>
    /// <param name="context">Database context</param>
    /// <param name="options">Options to deserialize json</param>
    private static void SeedSubjectsRelationships(DataContext context, JsonSerializerOptions options)
    {
        var result = context.SubjectsRelationships?.Any();
        if (result is true or null) return;
        var path = "Data/DataSeeders/SubjectsRelationsData.json";
        var subjectsRelationshipsData = File.ReadAllText(path);
        var subjectsRelationshipsList = JsonSerializer
                                            .Deserialize<List<SubjectRelationship>>(subjectsRelationshipsData, options) ??
                                        throw new Exception("SubjectsRelationsData.json is empty");
        // Normalize the codes of the codes
        subjectsRelationshipsList.ForEach(s =>
        {
            s.SubjectCode = s.SubjectCode.ToLower();
            s.PreSubjectCode = s.PreSubjectCode.ToLower();
        });

        context.SubjectsRelationships?.AddRange(subjectsRelationshipsList);
        context.SaveChanges();
    }
    
    
    
}