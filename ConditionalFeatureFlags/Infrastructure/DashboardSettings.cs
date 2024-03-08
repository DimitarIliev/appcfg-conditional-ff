namespace ConditionalFeatureFlags.Infrastructure
{
    public class DashboardSettings
    {
        public string[] AllowedLocations { get; set; }
    }

    public class DashboardAdminSettings
    {
        public string[] AllowedAdminCodes { get; set; }
    }
}
