using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace MandrilApi.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Power { get; set; }

        public int MandrilId { get; set; }

        [JsonIgnore]
        public Mandril? Mandril { get; set; }

        public static class PowerLevels
        {
            public const int Soft = 0;
            public const int Moderate = 1;
            public const int Intense = 2;
            public const int Powerful = 3;
            public const int Extreme = 4;
        }
    }
}