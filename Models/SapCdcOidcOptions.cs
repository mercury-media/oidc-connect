namespace OidcLogoutScenarios.Models;

public class SapCdcOidcOptions
{
    public const string SectionName = "SapCdcOidc";

    public string Authority { get; set; } = "https://<your-site>.accounts.ondemand.com";
    public string MetadataAddress { get; set; } = "https://<your-site>.accounts.ondemand.com/.well-known/openid-configuration";
    public string EndSessionEndpoint { get; set; } = "https://<your-site>.accounts.ondemand.com/oidc/op/v1.0/end_session";
    public string ClientId { get; set; } = "<client-id>";
    public string ClientSecret { get; set; } = "<client-secret>";
    public string CallbackPath { get; set; } = "/signin-oidc";
    public string SignedOutCallbackPath { get; set; } = "/loggedout";
    public string TrustedPostLogoutUrl { get; set; } = "https://localhost:5001/loggedout";
}
