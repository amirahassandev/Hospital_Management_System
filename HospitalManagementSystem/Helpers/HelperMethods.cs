namespace HospitalManagementSystem.Helpers
{
    public static class HelperMethods
    {
        public static string? GetFullName(string? firstName, string? lastName)
        {
            if (firstName == null && lastName == null) return null;
            if (firstName == null || lastName == null) return firstName ?? lastName;
            return (firstName + " " + lastName);
        }

        public static int CalculateAge(DateOnly dateOfBirth)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            int age = today.Year - dateOfBirth.Year;
            if (today < dateOfBirth.AddYears(age))
                age--;
            return age;
        }
    }
}
