using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snips.Domain.BusinessObjects;
using Snips.Domain.Enums;

namespace Snips.Pages.Snippets
{
    public class Edit : PageModel
    {
        [BindProperty(SupportsGet = true)] 
        public Guid? Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public void OnGet()
        {
            Id = Guid.NewGuid();
            Content = string.Join('\n',
                "## Titre de section",
                "Contenu du snippet",
                "* Liste",
                "* à",
                "* puces é",
                "",
                "<a href=\"#\">link</a>"
            );
            Title = "Le titre du snippet";
        }
    }
}