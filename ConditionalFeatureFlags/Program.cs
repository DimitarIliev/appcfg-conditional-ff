using ConditionalFeatureFlags.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(builder.Configuration["ConnectionStrings:AppConfiguration"])
           .Select("*", "Development")
           .ConfigureRefresh(refreshOptions =>
                refreshOptions.Register("Settings:Sentinel", refreshAll: true));
    options.UseFeatureFlags(featureFlagOptions =>
    {
        featureFlagOptions.Select("*", "Development");
        featureFlagOptions.CacheExpirationInterval = TimeSpan.FromSeconds(30);
    });
    // Load all feature flags with no label
});

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAzureAppConfiguration();
builder.Services.AddFeatureManagement()
    //.AddFeatureFilter<TargetingFilter>();
    .AddFeatureFilter<TenantFilter>()
    .AddFeatureFilter<OmegaFilter>();
// Add targeting filter
builder.Services.AddSingleton<ITargetingContextAccessor, TenantTargetingContextAccessor>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAzureAppConfiguration();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
