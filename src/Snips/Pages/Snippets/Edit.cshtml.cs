using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snips.Domain.BusinessObjects;
using Snips.Domain.Enums;
using Snips.Domain.Interfaces;

namespace Snips.Pages.Snippets
{
    public class Edit : PageModel
    {
        private readonly IRepository<Snippet> _repository;
        public Edit(IRepository<Snippet> repository)
        {
            _repository = repository;
        }
        
        [BindProperty(SupportsGet = true)] 
        public Guid? Id { get; set; }

        public Snippet Snippet { get; private set; }

        public async Task OnGet()
        {
            if (Id.HasValue) //Edit existing snippet
                Snippet = await _repository.GetSingle(Id.Value) 
                          ?? throw new Exception("Snippet not found");
            else //Add new snippet
                Snippet = new Snippet
                {
                    Content = string.Join("\n",
                        "Insert your content here with a *Markdown* formatting",
                        string.Empty,
                        "[Markdown cheatsheet](https://www.markdownguide.org/cheat-sheet/)")
                };
        }
    }
}