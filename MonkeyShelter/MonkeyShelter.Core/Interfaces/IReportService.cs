using MonkeyShelter.Core.DTOs.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyShelter.Core.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<SpeciesCountDto>> GetMonkeyCountBySpeciesAsync();
        Task<IEnumerable<SpeciesCountDto>> GetArrivalsBySpeciesBetweenAsync(DateTime from, DateTime to);
        void InvalidateSpeciesCountCache();
    }
}
