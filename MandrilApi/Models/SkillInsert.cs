using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MandrilApi.Models;
using MandrilApi.Services;
using static MandrilApi.Models.Skill;

namespace MandrilApi.Models
{
    public class SkillInsert
    {
        public string Name { get; set; } = string.Empty;
        public EPower Power { get; set; }
    }
}