using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public virtual DbSet<Person> Persons { get; set; }
    public virtual DbSet<Place> Places { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne(p => p.PlaceOfBirth)
            .WithMany(b => b.PeopleBornHere)
            .HasForeignKey(p => p.PlaceOfBirthId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Person>()
            .HasOne(p => p.PlaceOfResidence)
            .WithMany(r => r.PeopleLivingHere)
            .HasForeignKey(p => p.PlaceOfResidenceId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Person>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Place>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
    }
}
