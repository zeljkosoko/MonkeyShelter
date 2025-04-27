using MonkeyShelter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyShelter.Core.Interfaces
{
    public interface IVetCheckRepository
    {
        Task<IEnumerable<VetCheck>> GetAllAsync();
        Task<IEnumerable<Monkey>> GetMonkeysMissingCheckAsync();

        Task AddAsync(VetCheck check);
    }
}
