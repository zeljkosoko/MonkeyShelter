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
    public class ShelterRepository: IShelterRepository
    {
        private readonly MonkeyShelterDbContext _context;

        public ShelterRepository(MonkeyShelterDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Shelter>> GetAllAsync() =>
            await _context.Shelters.ToListAsync();

        public async Task<Shelter?> GetByIdAsync(int id) =>
            await _context.Shelters.FindAsync(id);

        public async Task AddAsync(Shelter shelter)
        {
            await _context.Shelters.AddAsync(shelter);
            await _context.SaveChangesAsync();
        }
    }
}
