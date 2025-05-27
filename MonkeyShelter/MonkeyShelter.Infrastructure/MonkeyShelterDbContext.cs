using Microsoft.EntityFrameworkCore;
using MonkeyShelter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

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
                new Species { Name = "Kapucin" },
                new Species { Name = "Zlatni lavlji tamari" },
                new Species { Name = "Mandril" },
                new Species { Name = "Babun" },
                new Species { Name = "Langur" },
                new Species { Name = "Pauk majmun" },
                new Species { Name = "Veveričasti majmun" },
                new Species { Name = "Rezus makaki" },
                new Species { Name = "Gibon" },
                new Species { Name = "Uakari" },
                new Species { Name = "Nosati majmun" },
                new Species { Name = "Džepni marmoset" },
                new Species { Name = "Howler majmun" },
                new Species { Name = "Gvenon" },
                new Species { Name = "Dril" }
            );

            //Seed examples shelters
            modelBuilder.Entity<Shelter>().HasData(
                new Shelter { Name = "Krnjaca" },
                new Shelter { Name = "Batajnica" }
                );

            //Seed monkeys
            modelBuilder.Entity<Monkey>().HasData(
                new Monkey { Name = "Pera1", Weight = 22, ArrivalDate = new DateTime(2025, 5, 1), SpeciesId = 1, ShelterId = 1 },
                new Monkey { Name = "Mika1", Weight = 33, ArrivalDate = new DateTime(2025, 5, 1), SpeciesId = 1, ShelterId = 1 },
                new Monkey { Name = "Laza1", Weight = 44, ArrivalDate = new DateTime(2025, 5, 1), SpeciesId = 1, ShelterId = 1 },
                new Monkey { Name = "Jova1", Weight = 55, ArrivalDate = new DateTime(2025, 5, 1), SpeciesId = 1, ShelterId = 1 },
                new Monkey { Name = "Sima1", Weight = 66, ArrivalDate = new DateTime(2025, 5, 1), SpeciesId = 1, ShelterId = 1 },
                new Monkey { Name = "Cica1", Weight = 77, ArrivalDate = new DateTime(2025, 5, 1), SpeciesId = 1, ShelterId = 1 },
                new Monkey { Name = "Pera2", Weight = 22, ArrivalDate = new DateTime(2025, 5, 2), SpeciesId = 2, ShelterId = 1 },
                new Monkey { Name = "Mika2", Weight = 33, ArrivalDate = new DateTime(2025, 5, 2), SpeciesId = 2, ShelterId = 1 },
                new Monkey { Name = "Laza2", Weight = 44, ArrivalDate = new DateTime(2025, 5, 2), SpeciesId = 2, ShelterId = 1 },
                new Monkey { Name = "Jova2", Weight = 55, ArrivalDate = new DateTime(2025, 5, 2), SpeciesId = 2, ShelterId = 1 },
                new Monkey { Name = "Sima2", Weight = 66, ArrivalDate = new DateTime(2025, 5, 2), SpeciesId = 2, ShelterId = 1 },
                new Monkey { Name = "Cica2", Weight = 77, ArrivalDate = new DateTime(2025, 5, 2), SpeciesId = 2, ShelterId = 1 },
                new Monkey { Name = "Pera3", Weight = 22, ArrivalDate = new DateTime(2025, 5, 3), SpeciesId = 3, ShelterId = 1 },
                new Monkey { Name = "Mika3", Weight = 33, ArrivalDate = new DateTime(2025, 5, 3), SpeciesId = 3, ShelterId = 1 },
                new Monkey { Name = "Laza3", Weight = 44, ArrivalDate = new DateTime(2025, 5, 3), SpeciesId = 3, ShelterId = 1 },
                new Monkey { Name = "Jova3", Weight = 55, ArrivalDate = new DateTime(2025, 5, 3), SpeciesId = 3, ShelterId = 1 },
                new Monkey { Name = "Sima3", Weight = 66, ArrivalDate = new DateTime(2025, 5, 3), SpeciesId = 3, ShelterId = 1 },
                new Monkey { Name = "Cica3", Weight = 77, ArrivalDate = new DateTime(2025, 5, 3), SpeciesId = 3, ShelterId = 1 },
                new Monkey { Name = "Pera4", Weight = 22, ArrivalDate = new DateTime(2025, 5, 4), SpeciesId = 4, ShelterId = 1 },
                new Monkey { Name = "Mika4", Weight = 33, ArrivalDate = new DateTime(2025, 5, 4), SpeciesId = 4, ShelterId = 1 },
                new Monkey { Name = "Laza4", Weight = 44, ArrivalDate = new DateTime(2025, 5, 4), SpeciesId = 4, ShelterId = 1 },
                new Monkey { Name = "Jova4", Weight = 55, ArrivalDate = new DateTime(2025, 5, 4), SpeciesId = 4, ShelterId = 1 },
                new Monkey { Name = "Sima4", Weight = 66, ArrivalDate = new DateTime(2025, 5, 4), SpeciesId = 4, ShelterId = 1 },
                new Monkey { Name = "Cica4", Weight = 77, ArrivalDate = new DateTime(2025, 5, 4), SpeciesId = 4, ShelterId = 1 },
                new Monkey { Name = "Pera5", Weight = 22, ArrivalDate = new DateTime(2025, 5, 5), SpeciesId = 5, ShelterId = 1 },
                new Monkey { Name = "Mika5", Weight = 33, ArrivalDate = new DateTime(2025, 5, 5), SpeciesId = 5, ShelterId = 1 },
                new Monkey { Name = "Laza5", Weight = 44, ArrivalDate = new DateTime(2025, 5, 5), SpeciesId = 5, ShelterId = 1 },
                new Monkey { Name = "Jova5", Weight = 55, ArrivalDate = new DateTime(2025, 5, 5), SpeciesId = 5, ShelterId = 1 },
                new Monkey { Name = "Sima5", Weight = 66, ArrivalDate = new DateTime(2025, 5, 5), SpeciesId = 5, ShelterId = 1 },
                new Monkey { Name = "Cica5", Weight = 77, ArrivalDate = new DateTime(2025, 5, 5), SpeciesId = 5, ShelterId = 1 },
                new Monkey { Name = "Pera6", Weight = 22, ArrivalDate = new DateTime(2025, 5, 6), SpeciesId = 6, ShelterId = 1 },
                new Monkey { Name = "Mika6", Weight = 33, ArrivalDate = new DateTime(2025, 5, 6), SpeciesId = 6, ShelterId = 1 },
                new Monkey { Name = "Laza6", Weight = 44, ArrivalDate = new DateTime(2025, 5, 6), SpeciesId = 6, ShelterId = 1 },
                new Monkey { Name = "Jova6", Weight = 55, ArrivalDate = new DateTime(2025, 5, 6), SpeciesId = 6, ShelterId = 1 },
                new Monkey { Name = "Sima6", Weight = 66, ArrivalDate = new DateTime(2025, 5, 6), SpeciesId = 6, ShelterId = 1 },
                new Monkey { Name = "Cica6", Weight = 77, ArrivalDate = new DateTime(2025, 5, 6), SpeciesId = 6, ShelterId = 1 },
                new Monkey { Name = "Pera7", Weight = 22, ArrivalDate = new DateTime(2025, 5, 7), SpeciesId = 7, ShelterId = 1 },
                new Monkey { Name = "Mika7", Weight = 33, ArrivalDate = new DateTime(2025, 5, 7), SpeciesId = 7, ShelterId = 1 },
                new Monkey { Name = "Laza7", Weight = 44, ArrivalDate = new DateTime(2025, 5, 7), SpeciesId = 7, ShelterId = 1 },
                new Monkey { Name = "Jova7", Weight = 55, ArrivalDate = new DateTime(2025, 5, 7), SpeciesId = 7, ShelterId = 1 },
                new Monkey { Name = "Sima7", Weight = 66, ArrivalDate = new DateTime(2025, 5, 7), SpeciesId = 7, ShelterId = 1 },
                new Monkey { Name = "Cica7", Weight = 77, ArrivalDate = new DateTime(2025, 5, 7), SpeciesId = 7, ShelterId = 1 },
                new Monkey { Name = "Pera8", Weight = 22, ArrivalDate = new DateTime(2025, 5, 8), SpeciesId = 8, ShelterId = 1 },
                new Monkey { Name = "Mika8", Weight = 33, ArrivalDate = new DateTime(2025, 5, 8), SpeciesId = 8, ShelterId = 1 },
                new Monkey { Name = "Laza8", Weight = 44, ArrivalDate = new DateTime(2025, 5, 8), SpeciesId = 8, ShelterId = 1 },
                new Monkey { Name = "Jova8", Weight = 55, ArrivalDate = new DateTime(2025, 5, 8), SpeciesId = 8, ShelterId = 1 },
                new Monkey { Name = "Sima8", Weight = 66, ArrivalDate = new DateTime(2025, 5, 8), SpeciesId = 8, ShelterId = 1 },
                new Monkey { Name = "Cica8", Weight = 77, ArrivalDate = new DateTime(2025, 5, 8), SpeciesId = 8, ShelterId = 1 },                
                new Monkey { Name = "Pera9", Weight = 22, ArrivalDate = new DateTime(2025, 5, 9), SpeciesId = 9, ShelterId = 1 },
                new Monkey { Name = "Mika9", Weight = 33, ArrivalDate = new DateTime(2025, 5, 9), SpeciesId = 9, ShelterId = 1 },
                new Monkey { Name = "Laza9", Weight = 44, ArrivalDate = new DateTime(2025, 5, 9), SpeciesId = 9, ShelterId = 1 },
                new Monkey { Name = "Jova9", Weight = 55, ArrivalDate = new DateTime(2025, 5, 9), SpeciesId = 9, ShelterId = 1 },
                new Monkey { Name = "Sima9", Weight = 66, ArrivalDate = new DateTime(2025, 5, 9), SpeciesId = 9, ShelterId = 1 },
                new Monkey { Name = "Cica9", Weight = 77, ArrivalDate = new DateTime(2025, 5, 9), SpeciesId = 9, ShelterId = 1 },
                new Monkey { Name = "Pera10", Weight = 22, ArrivalDate = new DateTime(2025, 5, 10), SpeciesId = 10, ShelterId = 1 },
                new Monkey { Name = "Mika10", Weight = 33, ArrivalDate = new DateTime(2025, 5, 10), SpeciesId = 10, ShelterId = 1 },
                new Monkey { Name = "Laza10", Weight = 44, ArrivalDate = new DateTime(2025, 5, 10), SpeciesId = 10, ShelterId = 1 },
                new Monkey { Name = "Jova10", Weight = 55, ArrivalDate = new DateTime(2025, 5, 10), SpeciesId = 10, ShelterId = 1 },
                new Monkey { Name = "Sima10", Weight = 66, ArrivalDate = new DateTime(2025, 5, 10), SpeciesId = 10, ShelterId = 1 },
                new Monkey { Name = "Cica10", Weight = 77, ArrivalDate = new DateTime(2025, 5, 10), SpeciesId = 10, ShelterId = 1 },
                new Monkey { Name = "Pera11", Weight = 22, ArrivalDate = new DateTime(2025, 5, 11), SpeciesId = 11, ShelterId = 1 },
                new Monkey { Name = "Mika11", Weight = 33, ArrivalDate = new DateTime(2025, 5, 11), SpeciesId = 11, ShelterId = 1 },
                new Monkey { Name = "Laza11", Weight = 44, ArrivalDate = new DateTime(2025, 5, 11), SpeciesId = 11, ShelterId = 1 },
                new Monkey { Name = "Jova11", Weight = 55, ArrivalDate = new DateTime(2025, 5, 11), SpeciesId = 11, ShelterId = 1 },
                new Monkey { Name = "Sima11", Weight = 66, ArrivalDate = new DateTime(2025, 5, 11), SpeciesId = 11, ShelterId = 1 },
                new Monkey { Name = "Cica11", Weight = 77, ArrivalDate = new DateTime(2025, 5, 11), SpeciesId = 11, ShelterId = 1 },
                new Monkey { Name = "Pera12", Weight = 22, ArrivalDate = new DateTime(2025, 5, 12), SpeciesId = 12, ShelterId = 1 },
                new Monkey { Name = "Mika12", Weight = 33, ArrivalDate = new DateTime(2025, 5, 12), SpeciesId = 12, ShelterId = 1 },
                new Monkey { Name = "Laza12", Weight = 44, ArrivalDate = new DateTime(2025, 5, 12), SpeciesId = 12, ShelterId = 1 },
                new Monkey { Name = "Jova12", Weight = 55, ArrivalDate = new DateTime(2025, 5, 12), SpeciesId = 12, ShelterId = 1 },
                new Monkey { Name = "Sima12", Weight = 66, ArrivalDate = new DateTime(2025, 5, 12), SpeciesId = 12, ShelterId = 1 },
                new Monkey { Name = "Cica12", Weight = 77, ArrivalDate = new DateTime(2025, 5, 12), SpeciesId = 12, ShelterId = 1 },
                new Monkey { Name = "Pera13", Weight = 22, ArrivalDate = new DateTime(2025, 5, 13), SpeciesId = 13, ShelterId = 1 },
                new Monkey { Name = "Mika13", Weight = 33, ArrivalDate = new DateTime(2025, 5, 13), SpeciesId = 13, ShelterId = 1 },
                new Monkey { Name = "Laza13", Weight = 44, ArrivalDate = new DateTime(2025, 5, 13), SpeciesId = 13, ShelterId = 1 },
                new Monkey { Name = "Jova13", Weight = 55, ArrivalDate = new DateTime(2025, 5, 13), SpeciesId = 13, ShelterId = 1 },
                new Monkey { Name = "Sima13", Weight = 66, ArrivalDate = new DateTime(2025, 5, 13), SpeciesId = 13, ShelterId = 1 },
                new Monkey { Name = "Cica13", Weight = 77, ArrivalDate = new DateTime(2025, 5, 13), SpeciesId = 13, ShelterId = 1 },
                new Monkey { Name = "Pera14", Weight = 22, ArrivalDate = new DateTime(2025, 5, 14), SpeciesId = 14, ShelterId = 1 },
                new Monkey { Name = "Mika14", Weight = 33, ArrivalDate = new DateTime(2025, 5, 14), SpeciesId = 14, ShelterId = 1 },
                new Monkey { Name = "Laza14", Weight = 44, ArrivalDate = new DateTime(2025, 5, 14), SpeciesId = 14, ShelterId = 1 },
                new Monkey { Name = "Jova14", Weight = 55, ArrivalDate = new DateTime(2025, 5, 14), SpeciesId = 14, ShelterId = 1 },
                new Monkey { Name = "Sima14", Weight = 66, ArrivalDate = new DateTime(2025, 5, 14), SpeciesId = 14, ShelterId = 1 },
                new Monkey { Name = "Cica14", Weight = 77, ArrivalDate = new DateTime(2025, 5, 14), SpeciesId = 14, ShelterId = 1 },
                new Monkey { Name = "Pera15", Weight = 22, ArrivalDate = new DateTime(2025, 5, 15), SpeciesId = 15, ShelterId = 1 },
                new Monkey { Name = "Mika15", Weight = 33, ArrivalDate = new DateTime(2025, 5, 15), SpeciesId = 15, ShelterId = 1 },
                new Monkey { Name = "Laza15", Weight = 44, ArrivalDate = new DateTime(2025, 5, 15), SpeciesId = 15, ShelterId = 1 },
                new Monkey { Name = "Jova15", Weight = 55, ArrivalDate = new DateTime(2025, 5, 15), SpeciesId = 15, ShelterId = 1 },
                new Monkey { Name = "Sima15", Weight = 66, ArrivalDate = new DateTime(2025, 5, 15), SpeciesId = 15, ShelterId = 1 },
                new Monkey { Name = "Cica15", Weight = 77, ArrivalDate = new DateTime(2025, 5, 15), SpeciesId = 15, ShelterId = 1 }
                );
        }
    }
}
