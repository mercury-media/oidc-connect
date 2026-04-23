using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.WebUtilities;
using OidcLogoutScenarios.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SapCdcOidcOptions>(builder.Configuration.GetSection(SapCdcOidcOptions.SectionName));
var sapOptions = builder.Configuration.GetSection(SapCdcOidcOptions.SectionName).Get<SapCdcOidcOptions>() ?? new SapCdcOidcOptions();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddOpenIdConnect(options =>
    {
        options.Authority = sapOptions.Authority;
        options.ClientId = sapOptions.ClientId;
        options.ClientSecret = sapOptions.ClientSecret;
        options.ResponseType = "code";
        options.SaveTokens = true;
        options.CallbackPath = sapOptions.CallbackPath;
        options.GetClaimsFromUserInfoEndpoint = true;

        if (!string.IsNullOrWhiteSpace(sapOptions.SignedOutCallbackPath))
        {
            options.SignedOutCallbackPath = sapOptions.SignedOutCallbackPath;
        }

        if (!string.IsNullOrWhiteSpace(sapOptions.MetadataAddress))
        {
            options.MetadataAddress = sapOptions.MetadataAddress;
        }
    });

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/login", async (HttpContext context) =>
{
    await context.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties
    {
        RedirectUri = "/"
    });
});

app.MapPost("/logout/rp", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    context.Response.Redirect("/");
});

app.MapPost("/logout/standard", async (HttpContext context, IConfiguration configuration) =>
{
    var oidc = configuration.GetSection(SapCdcOidcOptions.SectionName).Get<SapCdcOidcOptions>() ?? new SapCdcOidcOptions();

    if (!context.User.Identity?.IsAuthenticated ?? true)
    {
        context.Response.Redirect("/");
        return;
    }

    var idToken = await context.GetTokenAsync("id_token");

    var query = new Dictionary<string, string?>
    {
        ["id_token_hint"] = idToken,
        ["client_id"] = oidc.ClientId,
        ["postLogoutUrl"] = oidc.TrustedPostLogoutUrl
    };

    var endSessionUrl = QueryHelpers.AddQueryString(oidc.EndSessionEndpoint, query!);
    context.Response.Redirect(endSessionUrl);
});

app.MapRazorPages();

app.Run();
