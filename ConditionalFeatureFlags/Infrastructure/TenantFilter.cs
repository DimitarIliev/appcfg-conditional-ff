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
            var isLoggedIn = true;

            bool isEnabled = false;

            if (isLoggedIn)
            {
                // check tenant
            }
            else
                isEnabled = true;

            return Task.FromResult(isEnabled);
        }
    }
}
