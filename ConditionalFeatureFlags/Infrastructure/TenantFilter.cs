using Microsoft.FeatureManagement;

namespace ConditionalFeatureFlags.Infrastructure
{
    [FilterAlias("TenantFilter")]
    public class TenantFilter : IFeatureFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TenantFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            // Get the ClaimsFilterSettings from configuration
            var settings = context.Parameters.Get<TenantSettings>();

            var userTenant = "TenantA";

            var isEnabled = settings.AllowedTenants.Any(tenant => userTenant.Equals(tenant));

            return Task.FromResult(isEnabled);
        }
    }
}
