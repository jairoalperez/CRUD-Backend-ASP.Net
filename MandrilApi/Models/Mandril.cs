using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MandrilApi.Models
{
    public class Mandril
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public List<Skill>? Skills { get; set; }
    }
}