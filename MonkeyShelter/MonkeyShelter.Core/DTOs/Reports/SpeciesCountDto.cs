using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyShelter.Core.DTOs.Reports
{
    public class SpeciesCountDto
    {
        public string SpeciesName { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
