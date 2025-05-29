using MonkeyShelter.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyShelter.Services.BusinessLogic.Abstractions
{
    public interface IMonkeyService
    {
        //IEnumerable- only for iterate
        //IList - iterate + add,remove,insert,indexing..
        Task<IEnumerable<MonkeyDto>> GetAllAsync();
        Task<MonkeyDto?> GetMonkeyAsync(Guid id);
        Task UpdateWeightAsync(Guid id, double neWeight);
        Task DeleteMonkeyAsync(Guid id);

        /// <summary>
        /// Registering of monkey arrival to the shelter
        /// </summary>
        /// <param name="createMonkeyDto"></param>
        /// <returns></returns>
        Task<MonkeyDto> RegisterMonkeyArrivalAsync(CreateMonkeyDto createMonkeyDto);

        /// <summary>
        /// Registering of monkey departure from the shelter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MonkeyDto> RegisterMonkeyDepartureAsync(Guid id);
    }
}
