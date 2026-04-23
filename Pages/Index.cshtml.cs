using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OidcLogoutScenarios.Pages;

public class IndexModel : PageModel
{
    public bool IsAuthenticated { get; private set; }
    public string DisplayName { get; private set; } = "Anonymous";

    public void OnGet()
    {
        IsAuthenticated = User.Identity?.IsAuthenticated ?? false;
        if (IsAuthenticated)
        {
            DisplayName = User.Identity?.Name ?? "Authenticated user";
        }
    }
}
