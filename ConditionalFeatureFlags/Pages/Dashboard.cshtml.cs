using ConditionalFeatureFlags.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace ConditionalFeatureFlags.Pages
{
    [FeatureGate("Dashboard")]
    public class DashboardModel : PageModel
    {
        private readonly ILogger<DashboardModel> _logger;
        private readonly IFeatureManager _featureManager;

        public DashboardModel(ILogger<DashboardModel> logger, IFeatureManager featureManager)
        {
            _logger = logger;
            _featureManager = featureManager;
        }

        public async Task OnGet()
        {
            if (await _featureManager.IsEnabledAsync("DashboardAdmin", new MyApplicaionContext
            {
                AdminCode = "XYZ"
            }))
            {
                ViewData["DashboardAdmin"] = "Hello Dashboard Admin";
            }
            else
            {
                ViewData["DashboardAdmin"] = "Hello Dashboard User";
            }
        }
    }
}
