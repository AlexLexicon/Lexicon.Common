namespace Lexicon.Common.Validation.Amenities.Configurations;
public class PasswordRequirementsRuleSetConfiguration
{
    public int? MinimumLength { get; set; }
    public int? MaximumLength { get; set; }
    public bool? RequireDigit { get; set; }
    public bool? RequireNonAlphanumeric { get; set; }
    public bool? RequireLowercase { get; set; }
    public bool? RequireUppercase { get; set; }
    public int? RequiredUniqueChars { get; set; }
}
