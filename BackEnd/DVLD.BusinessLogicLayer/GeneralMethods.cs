using System.Text.RegularExpressions;

namespace DVLD.BusinessLogicLayer
{
    public static class GeneralMethods
    {
        public static int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;

            int age = today.Year - dateOfBirth.Year;

            if (dateOfBirth.Date > today.AddYears(-age))
                age--;

            return age;
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Regex بسيط للتحقق من صيغة الإيميل
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
