using System.Text.RegularExpressions;

namespace FAMS.Core.Helpers
{
    public static class StringHelper
    {
        public static string GenerateRandomObjectiveCode()
        {
            string pattern = @"^[A-Z0-9]{4}$"; // Regex pattern for 4-character string containing only uppercase letters and digits
            string objectiveCode;

            do
            {
                objectiveCode = Guid.NewGuid().ToString().Substring(0, 4).ToUpper();
            } while (!Regex.IsMatch(objectiveCode, pattern));

            return objectiveCode;
        }

        public static string GenerateRandomSyllabusCode()
        {
            // Regex pattern for 1 uppercase letter followed by 2 digits
            string pattern = @"^[A-SKHA-Z]\d{2}$";
            string objectiveCode;

            Random random = new Random();

            do
            {
                // Generating a random letter from A to Z
                char letter = (char)('A' + random.Next(0, 26));
                // Generating random two-digit number
                int number = random.Next(0, 100);
                // Formatting the objective code
                objectiveCode = string.Format("{0}{1:D2}", letter, number);
            } while (!Regex.IsMatch(objectiveCode, pattern));

            return objectiveCode;
        }
        public static bool IsConsecutive(int[] arr)
        {
            Array.Sort(arr);
            if (arr[0] != 1)
            {
                return false;
            }
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] != arr[i - 1] + 1)
                {
                   if(arr[i] != arr[i - 1]) return false;
                }
            }
            return true;
        }
    }
}
