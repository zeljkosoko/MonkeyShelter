﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyShelter.Core.Domain_exceptions
{
    public class DepartureNotAllowedException: Exception
    {
        public DepartureNotAllowedException(string message):
            base(message)
        {
        }
    }
}
