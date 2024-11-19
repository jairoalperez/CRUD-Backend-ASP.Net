using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MandrilApi.Models;

namespace MandrilApi.Services
{
    public class MandrilDataStore
    {
        public List<Mandril> Mandriles { get; set; }

        public static MandrilDataStore Current { get; } = new MandrilDataStore();

        public MandrilDataStore()
        {
            Mandriles = new List<Mandril>() {
                new Mandril() 
                {
                    Id = 1,
                    FirstName = "Conyu",
                    LastName = "Martinez",
                    Skills = new List<Skill>()
                    {
                        new Skill()
                        {
                            Id = 1,
                            Name = "Jump",
                            Power = Skill.EPower.Powerful
                        }
                    }

                },
                new Mandril() 
                {
                    Id = 2,
                    FirstName = "Crizmo",
                    LastName = "Morales",
                    Skills = new List<Skill>()
                    {
                        new Skill()
                        {
                            Id = 1,
                            Name = "Aim",
                            Power = Skill.EPower.Moderate
                        },
                        new Skill()
                        {
                            Id = 2,
                            Name = "Gamesense",
                            Power = Skill.EPower.Moderate
                        }
                    }

                },
                new Mandril() 
                {
                    Id = 3,
                    FirstName = "Vini",
                    LastName = "Jr",
                    Skills = new List<Skill>()
                    {
                        new Skill()
                        {
                            Id = 1,
                            Name = "Dribbling",
                            Power = Skill.EPower.Powerful
                        },
                        new Skill()
                        {
                            Id = 2,
                            Name = "Crying",
                            Power = Skill.EPower.Extreme
                        },
                        new Skill()
                        {
                            Id = 3,
                            Name = "Vision",
                            Power = Skill.EPower.Moderate
                        }
                    }
                },
                new Mandril() 
                {
                    Id = 4,
                    FirstName = "Vorsindar",
                    LastName = "Sindanvor",
                    Skills = new List<Skill>() {
                        new Skill() {
                            Id = 1,
                            Name = "Writting",
                            Power = Skill.EPower.Soft
                        },
                        new Skill() {
                            Id = 2,
                            Name = "Jumping",
                            Power = Skill.EPower.Extreme
                        }
                    }
                }
            };
        }
    }
}
