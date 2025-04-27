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
        public string Species { get; set; } = string.Empty;
        public string Shelter { get; set; } = string.Empty;
        public DateTime ArrivalDate { get; set; }
    }
}
