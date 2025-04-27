using Microsoft.EntityFrameworkCore;
using MonkeyShelter.Core.Entities;
using MonkeyShelter.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyShelter.Infrastructure.Repositories
{
    public class SpeciesRepository: ISpeciesRepository
    {
        private readonly MonkeyShelterDbContext _context;

        public SpeciesRepository(MonkeyShelterDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Species>> GetAllAsync() =>
            await _context.Species.ToListAsync();

        public async Task<Species?> GetByIdAsync(int id) =>
            await _context.Species.FindAsync(id);

        public async Task AddAsync(Species species)
        {
            await _context.Species.AddAsync(species);
            await _context.SaveChangesAsync();
        }
    }
}
