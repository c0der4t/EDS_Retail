using System.Diagnostics;
using System.Security.Cryptography;

namespace databaseAPI
{
    public static class utilities
    {

        public static string RandomUniqueID()
        {
            //Get date and time to second
            //Get a random number of randomintsize
            //Format date and time into simple string (no : , no / nothing)
            //Concat of datetime and random number

            string dateTime = DateTime.Now.ToString("yyyyMMddhhmmss");

            var randomGenny = new Random();
            var randomNumber = randomGenny.Next(999999);

            return $"{dateTime}{randomNumber}";
        }

    }
}
