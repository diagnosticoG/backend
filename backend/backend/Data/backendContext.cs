using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data
{
    public class backendContext : DbContext
    {
        public backendContext (DbContextOptions<backendContext> options)
            : base(options)
        {
        }

        public DbSet<backend.Models.Genero> Genero { get; set; } = default!;

        public DbSet<backend.Models.Persona>? Persona { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>()
                .HasOne(p => p.Genero)
                .WithMany()
                .HasForeignKey(p => p.Genero_Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
