using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MandrilApi.Helpers
{
    public static class ReplaceConnectionString
    {
        public static string BuildConnectionString(string rawConnectionString)
        {
            if (string.IsNullOrEmpty(rawConnectionString))
                throw new ArgumentException("La cadena de conexión no puede ser nula o vacía.", nameof(rawConnectionString));

            return rawConnectionString
                .Replace("${DB_SERVER}", Environment.GetEnvironmentVariable("DB_SERVER") ?? "")
                .Replace("${DB_PORT}", Environment.GetEnvironmentVariable("DB_PORT") ?? "")
                .Replace("${DB_NAME}", Environment.GetEnvironmentVariable("DB_NAME") ?? "")
                .Replace("${DB_USER}", Environment.GetEnvironmentVariable("DB_USER") ?? "")
                .Replace("${DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "");
        }
    }
}