using MonkeyShelter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyShelter.Core.Interfaces
{
    public interface ISpeciesRepository
    {
        Task<IEnumerable<Species>> GetAllAsync();
        Task<Species?> GetByIdAsync(int id);
        Task AddAsync(Species species);
    }
}
