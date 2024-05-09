namespace BookShop.Api.Options;

public class JwtOptions
{
    public static string SectionName = "JWT";
    public string SecretKey { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
}
