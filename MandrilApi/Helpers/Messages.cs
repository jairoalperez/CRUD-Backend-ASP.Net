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
            public const string Working                 = "The Mandriles Rest API is working good!";
        }
        public static class Mandril
        {
            public const string NotFound                = "";
        }

        public static class Database
        {
            public const string NoConnectionString      = "Couldn't Find a Database Connection String";
            public const string ConnectionSuccess       = "Database Connected Successfully!";
            public const string ConnectionFailed        = "Couldn't Connect to the Database";
        }
    }
}