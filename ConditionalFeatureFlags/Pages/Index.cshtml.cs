using ConditionalFeatureFlags.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.FeatureManagement;

namespace ConditionalFeatureFlags.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IFeatureManager _featureManager;

        public IndexModel(ILogger<IndexModel> logger, IFeatureManager featureManager)
        {
            _logger = logger;
            _featureManager = featureManager;
        }

        public async Task OnGet()
        {
            if (await _featureManager.IsEnabledAsync("Omega", new MyApplicaionContext {
                BranchNumber = "123"
            }))
            {
                ViewData["Beta"] = "Omega enabled";
            }
            else
            {
                ViewData["Beta"] = "Omega disabled";
            }
        }

    }
}