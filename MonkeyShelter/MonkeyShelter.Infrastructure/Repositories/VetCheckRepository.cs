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
    public class VetCheckRepository: IVetCheckRepository
    {
        private readonly MonkeyShelterDbContext _context;

        public VetCheckRepository(MonkeyShelterDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VetCheck>> GetAllAsync() =>
            await _context.VetChecks.Include(vc => vc.Monkey).ToListAsync();

        public async Task AddAsync(VetCheck check)
        {
            await _context.VetChecks.AddAsync(check);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Monkey>> GetMonkeysMissingCheckAsync()
        {
            var today = DateTime.UtcNow;

            return await _context.Monkeys
                .Include(m => m.VetChecks)
                .Where(m => !m.VetChecks.Any() || m.VetChecks.Max(vc => vc.CheckDate).AddDays(60) < today)
                .ToListAsync();
        }
    }
}
