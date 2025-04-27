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
    public class MonkeyRepository: IMonkeyRepository
    {
        private readonly MonkeyShelterDbContext _context;

        public MonkeyRepository(MonkeyShelterDbContext context)
        {
            _context = context;
        }

        public async Task<Monkey> GetByIdAsync(Guid id)
        {
            return await _context.Monkeys
                .Include(m => m.Species)
                .Include(m => m.Shelter)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Monkey>> GetAllAsync()
        {
            return await _context.Monkeys
                .Include(m => m.Species)
                .Include(m => m.Shelter)
                .ToListAsync();
        }

        public async Task<IEnumerable<Monkey>> GetBySpeciesAsync(int speciesId)
        {
            return await _context.Monkeys
                .Where(m => m.SpeciesId == speciesId)
                .Include(m => m.Species)
                .Include(m => m.Shelter)
                .ToListAsync();
        }

        public async Task<IEnumerable<Monkey>> GetArrivalsBetweenDatesAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Monkeys
                .Where(m => m.ArrivalDate >= startDate && m.ArrivalDate <= endDate)
                .Include(m => m.Species)
                .Include(m => m.Shelter)
                .ToListAsync();
        }

        public async Task AddAsync(Monkey monkey)
        {
            await _context.Monkeys.AddAsync(monkey);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var monkey = await _context.Monkeys.FindAsync(id);
            if (monkey != null)
            {
                _context.Monkeys.Remove(monkey);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateWeightAsync(Guid id, double newWeight)
        {
            var monkey = await _context.Monkeys.FindAsync(id);
            if (monkey != null)
            {
                monkey.Weight = newWeight;
                await _context.SaveChangesAsync();
            }
        }
    }
}
