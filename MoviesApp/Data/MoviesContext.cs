using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Models;

namespace MoviesApp.Data
{
    public class MoviesContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }

        public MoviesContext(DbContextOptions<MoviesContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            throw new InvalidOperationException("Context was not configured");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(e =>
            {
                e.Property(m => m.Price).HasColumnType("decimal(15, 2)");
                e.Property(m => m.ReleaseDate).HasColumnType("smalldatetime");

                e
                    .HasMany(m => m.Actors)
                    .WithMany(a => a.Movies)
                    .UsingEntity<Dictionary<string, object>>(
                        "MoviesActors",
                        r => r.HasOne<Actor>().WithMany()
                            .HasForeignKey("ActorId")
                            .OnDelete(DeleteBehavior.ClientSetNull),
                        l => l.HasOne<Movie>().WithMany()
                            .HasForeignKey("MovieId")
                            .OnDelete(DeleteBehavior.ClientSetNull),
                        j => j.HasKey("MovieId", "ActorId")
                    );
            });

            modelBuilder.Entity<Actor>(e =>
            {
                e.Property(a => a.BirthDate).HasColumnType("smalldatetime");
            });
        }
    }
}