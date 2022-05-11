using Microsoft.EntityFrameworkCore;
using MyMovies.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies.DataAccess
{
        public class MovieDbContext : DbContext
        {
            private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=MyMoviesDb;Trusted_Connection=True;";
            public DbSet<Movie> Movies { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Movie>()
                    .Property(m => m.Title)
                    .IsRequired()
                    .HasMaxLength(200);

            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
}

