namespace CarPark.Domain.Config
{
    public class PricingOptions
    {
        public const string SectionName = "Pricing";
        public decimal SmallRatePerMinute { get; set; }
        public decimal MediumRatePerMinute { get; set; }
        public decimal LargeRatePerMinute { get; set; }
        public decimal ExtraPerFiveMinutes { get; set; }
    }
}
