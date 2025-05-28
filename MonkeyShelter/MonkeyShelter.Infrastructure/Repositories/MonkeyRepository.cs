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
    /// <summary>
    /// *** Repository doesn't know about rules, it knows only the DATA.
    /// </summary>
    public class MonkeyRepository: IMonkeyRepository
    {
        private readonly MonkeyShelterDbContext _context;

        public MonkeyRepository(MonkeyShelterDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Monkey>> GetAllAsync()
        {
            return await _context.Monkeys
                .Include(m => m.Species)
                .Include(m => m.Shelter)
                .ToListAsync();
        }

        public async Task<Monkey?> GetByIdAsync(Guid id)
        {
            return await _context.Monkeys
                .Include(m => m.Species)
                .Include(m => m.Shelter)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IList<Monkey>> GetBySpeciesAsync(int speciesId)
        {
            return await _context.Monkeys
                .Where(m => m.SpeciesId == speciesId)
                .Include(m => m.Species)
                .Include(m => m.Shelter)
                .ToListAsync();
        }

        public async Task<IList<Monkey>> GetArrivalsBetweenDatesAsync(DateTime startDate, DateTime endDate)
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
        public async Task UpdateAsync(Monkey monkey)
        {
            _context.Monkeys.Update(monkey);
            await _context.SaveChangesAsync();
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

        public async Task<bool> CanMonkeyArriveAsync(DateTime date)
        {
            //Arivals today must be up to 7
            var arrivalsToday = await _context.Monkeys
                .Where(m => m.ArrivalDate.Date == date.Date)
                .CountAsync();

            return arrivalsToday < 7;
        }

        public async Task<bool> CanMonkeyLeaveAsync(DateTime date, int speciesId)
        {
            //1.How many monkeys have been departure today?
            var departuresToday = await _context.Monkeys
                .Where(m => m.DepartureDate.HasValue &&
                            m.DepartureDate.Value.Date == date.Date)
                .CountAsync();

            //2.How many monkeys have been arrive today?
            var arrivalsToday = await _context.Monkeys
                .Where(m => m.ArrivalDate.Date == date.Date)
                .CountAsync();    

            //3.How many monkeys of that species are there in the shelter?
            var speciesCount = await _context.Monkeys
                .Where(m => m.SpeciesId == speciesId &&
                            m.DepartureDate == null)
                .CountAsync();

            //4.Rules - ArrivalDepartureDifference and speciesCount
            bool okDeparturesArrivalsDiff = departuresToday - arrivalsToday < 2;
            bool atLeastOneStays = speciesCount > 1;

            return okDeparturesArrivalsDiff && atLeastOneStays;
        }
    }
}
