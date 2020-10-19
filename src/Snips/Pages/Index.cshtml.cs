using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Snips.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet() => 
            Redirect("/Snippets/Index");
    }
}