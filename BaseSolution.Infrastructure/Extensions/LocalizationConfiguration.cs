namespace BaseSolution.Infrastructure.Extensions
{
    public class LocalizationConfiguration
    {
        public string? DefaultCulture { get; set; }

        public bool FallBackToParentCultures { get; set; }

        public bool FallBackToParentUICultures { get; set; }

        public bool ApplyCurrentCultureToResponseHeaders { get; set; }

        public List<string> SupportedCultures { get; set; } = new();

        public List<string> SupportedUICultures { get; set; } = new();
    }
}
