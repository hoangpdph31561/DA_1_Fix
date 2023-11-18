using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Extensions
{
    public static class UtilityExtensions
    {
        private static Random random = new Random();
        public static string GenerateRandomString(int totalLength)
        {
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";
            

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < totalLength; i++)
            {
                bool isAlphabet = random.Next(2) == 0;

                if (isAlphabet)
                {
                    int index = random.Next(alphabet.Length);
                    sb.Append(alphabet[index]);
                }
                else
                {
                    int index = random.Next(numbers.Length);
                    sb.Append(numbers[index]);
                }
            }

            return sb.ToString();
        }
    }
}
