using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MandrilApi.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public EPower Power { get; set; }

        public enum EPower
        {
            Soft,
            Moderate,
            Intense,
            Powerful,
            Extreme
        }
    }
}