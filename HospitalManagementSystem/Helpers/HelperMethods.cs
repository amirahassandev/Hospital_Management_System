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
    }
}
