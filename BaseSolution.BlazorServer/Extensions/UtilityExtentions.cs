using System.Globalization;
using System.Text;

namespace BaseSolution.BlazorServer.Extensions
{
    public static class UtilityExtentions
    {
        public static List<string> CacDonViTinh = new List<string>()
        {
            "bộ",
            "chiếc",
            "cái",
            "cặp",
            "đôi",
            "hộp",
            "ổ",
            "tấm",
            "cây",
            "bình",
            "ly",
            "chai",
            "lon",
            "lọ",
            "tô",
            "cốc",
            "hũ",
            "ấm",
            "ống",
            "kg",
            "kí lô",
            "kilo",
            "kí lô gram",
            "kilogram",
            "gram",
            "g",
            "miếng",
            "ổ",
            "quả",
            "bịch",
            "gói",
            "hộp",
            "lon",
            "lọ",
            "giờ",
            "buổi",
            "lần",
            "đêm",
            "ngày",
            "tuần",
            "tháng",
            "năm"
        };
        public static List<string> GetRecommendedStrings(string inputString, List<string> listDefaultString)
        {
            // Create a list to store the recommended strings
            List<string> listRecommendString = new List<string>();

            // Convert the input string to lowercase and remove leading/trailing white spaces
            inputString = inputString.ToLower().Trim();

            // Calculate the number of equal characters between two strings
            int NumberOfEqualChars(string str1, string str2)
            {
                int minLength = Math.Min(str1.Length, str2.Length);
                int equalCount = 0;

                for (int i = 0; i < minLength; i++)
                {
                    if (str1[i] == str2[i])
                    {
                        equalCount++;
                    }
                }

                return equalCount;
            }

            // Sort the list of default strings based on their similarity to the input string
            listRecommendString = listDefaultString.Distinct()
                .OrderByDescending(str => str == inputString)
                .ThenByDescending(str => str.ToLower() == inputString)
                .ThenByDescending(str => RemoveDiacritics(str) == RemoveDiacritics(inputString))
                .ThenByDescending(str => RemoveDiacritics(str.ToLower()) == RemoveDiacritics(inputString.ToLower()))
                .ThenByDescending(str => NumberOfEqualChars(inputString, str))
                .ThenByDescending(str => NumberOfEqualChars(inputString.ToLower(), str.ToLower()))
                .ToList();

            return listRecommendString;
        }

        // Remove diacritics (accents) from a string
        public static string RemoveDiacritics(string input)
        {
            string normalizedString = input.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
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
