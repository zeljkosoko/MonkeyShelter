using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyShelter.Core.DTOs
{
    public class CreateMonkeyDto
    {
        public string Name { get; set; } = string.Empty;
        public double Weight { get; set; }

        public int SpeciesId { get; set; }
        public int ShelterId { get; set; }
    }
}
