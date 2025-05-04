using Microsoft.EntityFrameworkCore;
using MonkeyShelter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MonkeyShelter.Infrastructure
{
    public class MonkeyShelterDbContext: IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public MonkeyShelterDbContext(DbContextOptions<MonkeyShelterDbContext> options)
        : base(options) { }

        public DbSet<Monkey> Monkeys => Set<Monkey>();
        public DbSet<Species> Species => Set<Species>();
        public DbSet<Shelter> Shelters => Set<Shelter>();
        public DbSet<VetCheck> VetChecks => Set<VetCheck>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed example species (can be modified later)
            modelBuilder.Entity<Species>().HasData(
                new Species { Id = 1, Name = "Chimpanzee" },
                new Species { Id = 2, Name = "Baboon" },
                new Species { Id = 3, Name = "Capuchin" }
            );

            //Seed examples shelters
            modelBuilder.Entity<Shelter>().HasData(
                new Shelter { Id = 1, Name = "Obrenovac" },
                new Shelter { Id = 2, Name = "Batajnica" },
                new Shelter { Id = 3, Name = "Kovilovo"}
                );
        }
    }
}
