using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyShelter.Core.Entities
{
    public class VetCheck
    {
        public int Id { get; set; }
        public DateTime CheckDate { get; set; } = DateTime.UtcNow;

        public Guid MonkeyId { get; set; }
        public Monkey Monkey { get; set; } = null!;
    }
}
