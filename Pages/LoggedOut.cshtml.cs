using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OidcLogoutScenarios.Pages;

public class LoggedOutModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string Mode { get; set; } = "(not provided)";

    public void OnGet()
    {
    }
}
