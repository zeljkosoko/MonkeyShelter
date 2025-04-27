using MonkeyShelter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyShelter.Core.Interfaces
{
    public interface IMonkeyRepository
    {
        Task<Monkey> GetByIdAsync(Guid id);
        Task<IEnumerable<Monkey>> GetAllAsync();
        Task<IEnumerable<Monkey>> GetBySpeciesAsync(int speciesId);
        Task<IEnumerable<Monkey>> GetArrivalsBetweenDatesAsync(DateTime startDate, DateTime endDate);
        Task AddAsync(Monkey monkey);
        Task RemoveAsync(Guid id);
        Task UpdateWeightAsync(Guid id, double newWeight);
    }
}
