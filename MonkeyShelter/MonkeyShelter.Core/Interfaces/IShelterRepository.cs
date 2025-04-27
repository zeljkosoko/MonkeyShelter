using MonkeyShelter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyShelter.Core.Interfaces
{
    public interface IShelterRepository
    {
        Task<IEnumerable<Shelter>> GetAllAsync();
        Task<Shelter?> GetByIdAsync(int id);
        Task AddAsync(Shelter shelter);
    }
}
