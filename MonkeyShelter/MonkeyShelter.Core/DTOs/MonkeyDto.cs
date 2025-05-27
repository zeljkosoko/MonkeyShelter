using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyShelter.Core.DTOs
{
    public class MonkeyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Weight { get; set; }
        public string SpeciesName { get; set; } = string.Empty;
        public string ShelterName { get; set; } = string.Empty;
        public DateTime ArrivalDate { get; set; }
        public DateTime? DepartureDate { get; set; }
    }
}
