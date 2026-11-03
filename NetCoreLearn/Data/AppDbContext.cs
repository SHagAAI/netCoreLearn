using System.Collections.Generic;
using EFCoreMovieCRUD.Models;
using Microsoft.EntityFrameworkCore;
using netCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NetCoreLearn.Models;
using Microsoft.AspNetCore.Identity;

namespace netCore.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Director> Directors => Set<Director>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Fluent API: Ensure genre names are unique
        modelBuilder.Entity<Genre>(genreEntity =>
        {
            genreEntity.HasIndex(g => g.Name).IsUnique();
        });

        // 2. Fluent API: One-to-Many relationship between Director and Movies
        modelBuilder.Entity<Movie>(movieEntity =>
        {
            movieEntity.HasOne(m => m.Director)
                .WithMany(d => d.Movies)
                .HasForeignKey(m => m.DirectorId)
                .OnDelete(DeleteBehavior.Restrict);
                
                // delete director will delete all movie belong to that dir
                // .OnDelete(DeleteBehavior.Cascade);
        });

        // 3. Fluent API: Explicit Many-to-Many relationship between Movie and Genre
        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Genres)
            .WithMany(g => g.Movies)
            .UsingEntity<Dictionary<string, object>>(
                "MovieGenre",
                j => j.HasOne<Genre>().WithMany().HasForeignKey("GenreId"),
                j => j.HasOne<Movie>().WithMany().HasForeignKey("MovieId"),
                j =>
                {
                    j.HasKey("MovieId", "GenreId");
                    j.ToTable("MovieGenres");
                }
            );
    }
}
