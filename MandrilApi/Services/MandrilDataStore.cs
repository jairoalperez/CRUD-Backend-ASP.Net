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
                    

                },
                new Mandril() 
                {
                    Id = 2,
                    FirstName = "Crizmo",
                    LastName = "Morales",
                    

                },
                new Mandril() 
                {
                    Id = 3,
                    FirstName = "Vini",
                    LastName = "Jr",
                    
                },
                new Mandril() 
                {
                    Id = 4,
                    FirstName = "Vorsindar",
                    LastName = "Sindanvor",
                    
                }
            };
        }
    }
}
