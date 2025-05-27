using MonkeyShelter.Core.DTOs;
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
        Task<IList<Monkey>> GetAllAsync();
        Task<Monkey?> GetByIdAsync(Guid id);
        Task AddAsync(Monkey monkey);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Monkey monkey);
        Task UpdateWeightAsync(Guid id, double newWeight);

        Task<IList<Monkey>> GetBySpeciesAsync(int speciesId);
        Task<IList<Monkey>> GetArrivalsBetweenDatesAsync(DateTime startDate, DateTime endDate);

        Task<bool> CanMonkeyArriveAsync(DateTime date);
        Task<bool> CanMonkeyLeaveAsync(DateTime date, int speciesId);
    }
}
