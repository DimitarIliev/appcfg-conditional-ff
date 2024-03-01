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
            var settings = context.Parameters.Get<TenantSettings>();

            var userTenant = "TenantA";

            var isEnabled = settings.AllowedTenants.Any(tenant => userTenant.Equals(tenant));

            return Task.FromResult(isEnabled);
        }
    }

    [FilterAlias("OmegaFilter")]
    public class OmegaFilter : IContextualFeatureFilter<MyApplicaionContext>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OmegaFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context, MyApplicaionContext myApplicaionContext)
        {            
            var isEnabled = myApplicaionContext.BranchNumber.Equals("123");

            return Task.FromResult(isEnabled);
        }
    }
}
