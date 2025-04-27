using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyShelter.Core.DTOs
{
    public class CreateVetCheckDto
    {
        public Guid MonkeyId { get; set; }
        public DateTime CheckDate { get; set; }
    }
}
