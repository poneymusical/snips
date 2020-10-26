using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snips.Services.Directories;
using Snips.ViewModels;

namespace Snips.Pages.Snippets
{
    public class Index : PageModel
    {
        private readonly ITreeviewService _treeviewService;
        
        public Index(ITreeviewService treeviewService)
        {
            _treeviewService = treeviewService;
        }

        public IList<TreeviewItem> Tree { get; set; }

        public async Task OnGetAsync()
        {
            Tree = await _treeviewService.GetDirectoriesAndSnippetsAsTree();
        }
    }
}