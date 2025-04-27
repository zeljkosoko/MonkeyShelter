using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyShelter.Core.Entities
{
    public class Monkey
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public double Weight { get; set; }
        public DateTime ArrivalDate { get; set; } = DateTime.UtcNow;
        public DateTime? DepartureDate { get; set; }  // Nullable-he must not go out
        // Foreign Keys
        public int SpeciesId { get; set; }
        public Species Species { get; set; }

        public int ShelterId { get; set; }
        public Shelter Shelter { get; set; }

        public List<VetCheck> VetChecks { get; set; } = new();
    }
}
