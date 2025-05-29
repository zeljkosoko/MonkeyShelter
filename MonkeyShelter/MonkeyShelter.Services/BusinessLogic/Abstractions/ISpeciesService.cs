using MonkeyShelter.Core.DTOs;
using MonkeyShelter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyShelter.Services.BusinessLogic.Abstractions
{
    public interface ISpeciesService
    {
        //IEnumerable - only for iterate
        Task<IEnumerable<SpeciesDto>> GetAllSpeciesAsync();
        Task<SpeciesDto?> GetSpeciesByAsync(int id);
        Task CreateAsync(CreateSpeciesDto species);
    }
}
