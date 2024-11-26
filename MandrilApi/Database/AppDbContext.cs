using MandrilApi.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    {
        
    }

    public required DbSet<Mandril> Mandriles { get; set;}
    public required DbSet<Skill> Skills { get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mandril>().ToTable("mandril"); 
        modelBuilder.Entity<Skill>().ToTable("skill"); 

        modelBuilder.Entity<Skill>()
            .HasOne(s => s.Mandril)
            .WithMany(m => m.Skills)
            .HasForeignKey(s => s.MandrilId);
    }
}
