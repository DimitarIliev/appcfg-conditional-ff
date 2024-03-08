using Microsoft.FeatureManagement;

namespace ConditionalFeatureFlags.Infrastructure
{
    [FilterAlias("DashboardFilter")]
    public class DashboardFilter : IFeatureFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DashboardFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var settings = context.Parameters.Get<DashboardSettings>();

            // Retrieve the current logged in user (ClaimsPrincipal)
            var user = _httpContextAccessor.HttpContext.User;

            // Get location from claim or any other claim data
            var userLocation = "North Europe";

            // Custom logic
            var isEnabled = settings.AllowedLocations.Any(location => userLocation.Equals(location));

            return Task.FromResult(isEnabled);
        }
    }

    [FilterAlias("DashboardAdminFilter")]
    public class DashboardAdminFilter : IContextualFeatureFilter<MyApplicaionContext>
    {
        public DashboardAdminFilter()
        {
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context, MyApplicaionContext myApplicaionContext)
        {
            var settings = context.Parameters.Get<DashboardAdminSettings>();

            // If we have no ambient context, we can float a context into the feature management system

            // Custom logic
            var isEnabled = settings.AllowedAdminCodes.Contains(myApplicaionContext.AdminCode);

            return Task.FromResult(isEnabled);
        }
    }
}
