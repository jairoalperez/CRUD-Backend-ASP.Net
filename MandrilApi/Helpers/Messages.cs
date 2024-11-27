using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MandrilApi.Helpers
{
    public static class Messages
    {
        public static class API
        {
            public const string Working                 
                                = "The Mandriles Rest API is working good!";
        }

        public static class Database
        {
            public const string NoConnectionString      
                                = "Couldn't Find a Database Connection String";
            public const string ConnectionSuccess       
                                = "Database Connected Successfully!";
            public const string ConnectionFailed        
                                = "Couldn't Connect to the Database";
        }

        public static class Mandril
        {
            public const string NotFound                
                                = "Couldn't find any Mandril with this Id";
            public const string Created                 
                                = "Mandril Created Successfully!";
            public const string Edited                  
                                = "Mandril Edited Successfuly!";
            public const string Deleted                 
                                = "Mandril has been deleted";
            public const string AllDeleted              
                                = "All the Mandriles have been deleted";
            public const string NoMandriles             
                                = "Couldn't find any Mandriles in the database";
        }

        public static class Skill
        {
            public const string NoSkills                
                                = "Couldn't find any Skills asociated with this Mandril";
            public const string NotFound                
                                = "Couldn't find any Skill with this Id";
            public const string Repeated
                                = "There is already a skill with the same name";   
            public const string Created
                                = "Skill created successfully!"; 
            public const string Edited
                                = "Skill edited successfully!";
            public const string Deleted
                                = "Skill has been deleted";
            public const string AllDeleted
                                = "All the Skills have been deleted";
        }
        
    }
}