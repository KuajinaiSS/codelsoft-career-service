using career_service.Models;
using Microsoft.EntityFrameworkCore;

namespace career_service.Data;

public class DataContext : DbContext
{
    public DbSet<Career> Careers { get; set; } = null!;
    public DbSet<Subject> Subjects { get; set; } = null!;
    public DbSet<SubjectRelationship> SubjectsRelationships { get; set; } = null!;
    
    public DataContext(DbContextOptions options) : base(options) { }
    
}