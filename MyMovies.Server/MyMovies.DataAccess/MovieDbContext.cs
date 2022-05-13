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
            public MovieDbContext(DbContextOptions options) : base(options)
            {
            }
        public DbSet<Movie> Movies { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Movie>()
                    .Property(m => m.Title)
                    .IsRequired()
                    .HasMaxLength(200);

            }

        }
}

