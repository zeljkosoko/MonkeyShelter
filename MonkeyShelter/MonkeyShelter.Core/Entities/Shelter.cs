﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyShelter.Core.Entities
{
    public class Shelter
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Monkey> Monkeys { get; set; } = new();
    }
}
