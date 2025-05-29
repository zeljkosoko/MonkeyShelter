using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyShelter.Core.Domain_exceptions
{
    public class SpeciesAlreadyExistsException: Exception
    {
        public SpeciesAlreadyExistsException(string message)
            :base(message)
        {
        }
    }
}
