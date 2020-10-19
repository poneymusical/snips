using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snips.Domain.BusinessObjects;
using Snips.Domain.Enums;

namespace Snips.Pages.Snippets
{
    public class Edit : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        
        public Snippet Snippet { get; set; }

        public void OnGet()
        {
            Snippet = new Snippet
            {
                Id = Guid.NewGuid(),
                Content = string.Join('\n',
                    "## Titre de section", 
                    "Contenu du snippet",
                    "* Liste",
                    "* à",
                    "* puces"
                ),
                Title = "Le titre du snippet"
            };
        }
    }
}