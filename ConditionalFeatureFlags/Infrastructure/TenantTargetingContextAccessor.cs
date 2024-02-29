using Microsoft.FeatureManagement.FeatureFilters;

namespace ConditionalFeatureFlags.Infrastructure
{
    public class TenantTargetingContextAccessor : ITargetingContextAccessor
    {
        private const string TargetingContextLookup = "TenantTargetingContextAccessor.TargetingContext";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantTargetingContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public ValueTask<TargetingContext> GetContextAsync()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;

            if (httpContext.Items.TryGetValue(TargetingContextLookup, out object value))
            {
                return new ValueTask<TargetingContext>((TargetingContext)value);
            }

            string tenant = _httpContextAccessor.HttpContext.Request.Cookies["Tenant"];

            List<string> groups = new List<string>();

            groups.Add(tenant);

            TargetingContext targetingContext = new TargetingContext
            {
                UserId = tenant,
                Groups = groups
            };

            httpContext.Items[TargetingContextLookup] = targetingContext;

            return new ValueTask<TargetingContext>(targetingContext);
        }
    }
}
