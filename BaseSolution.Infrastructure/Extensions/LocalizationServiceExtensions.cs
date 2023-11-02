using BaseSolution.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace BaseSolution.Infrastructure.Extensions
{
    public static class LocalizationServiceExtensions
    {
        public static IServiceCollection AddLocalization(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            LocalizationConfiguration localizationConfig = new LocalizationConfiguration();
            configurationManager.GetSection("Localization").Bind(localizationConfig);

            services.AddLocalization(delegate (LocalizationOptions options)
            {
                options.ResourcesPath = "Resources";
            });

            List<CultureInfo> supportedCultures = localizationConfig.SupportedCultures.Select((x) => new CultureInfo(x)).ToList();
            List<CultureInfo> supportedUiCultures = localizationConfig.SupportedUICultures.Select((x) => new CultureInfo(x)).ToList();

            services.Configure(delegate (RequestLocalizationOptions options)
            {
                options.DefaultRequestCulture = new RequestCulture(localizationConfig.DefaultCulture!);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedUiCultures;
                options.FallBackToParentCultures = localizationConfig.FallBackToParentCultures;
                options.FallBackToParentUICultures = localizationConfig.FallBackToParentUICultures;
                options.ApplyCurrentCultureToResponseHeaders = localizationConfig.ApplyCurrentCultureToResponseHeaders;
            });
            return services;
        }
    }
}
