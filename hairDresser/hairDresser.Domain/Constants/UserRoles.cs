namespace hairDresser.Domain.Constants
{
    //static class instead of enum because we want to use string values for roles
    public static class UserRoles
    {
        public const string Admin = "admin";
        public const string Employee = "employee";
        public const string Customer = "customer";
    }
}