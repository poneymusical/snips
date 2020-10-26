using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snips.Domain.BusinessObjects;
using Snips.Domain.Interfaces;
using Snips.Services.Directories;
using Snips.ViewModels;

namespace Snips.Pages.Snippets
{
    public class Edit : PageModel
    {
        private readonly IRepository<Snippet> _repository;
        private readonly ITreeviewService _treeviewService;
        
        public Edit(
            IRepository<Snippet> repository,
            ITreeviewService treeviewService)
        {
            _repository = repository;
            _treeviewService = treeviewService;
        }
        
        [BindProperty(SupportsGet = true)] 
        public Guid? Id { get; set; }

        public Snippet Snippet { get; private set; }
        public IList<TreeviewItem> Tree { get; private set; }

        public async Task OnGetAsync()
        {
            Tree = await _treeviewService.GetDirectoriesAsTree();
            
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