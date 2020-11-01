using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snips.Domain.BusinessObjects;
using Snips.Domain.Interfaces;
using Snips.Services.Directories;
using Snips.ViewModels;

namespace Snips.Pages.Directories
{
    [Authorize]
    public class Edit : PageModel
    {
        private readonly IRepository<Directory> _repository;
        private readonly ITreeviewService _treeviewService;

        [BindProperty(SupportsGet = true)] 
        public Guid? Id { get; set; }

        public Directory Directory { get; private set; }
        
        public IList<TreeviewItem> Tree { get; private set; }        

        public Edit(IRepository<Directory> repository, ITreeviewService treeviewService)
        {
            _repository = repository;
            _treeviewService = treeviewService;
        }

        public async Task OnGetAsync()
        {
            Tree = await _treeviewService.GetDirectoriesAsTree();
            
            if (Id.HasValue)
                Directory = await _repository.GetSingle(Id.Value)
                            ?? throw new Exception("Directory not found");
            else
                Directory = new Directory();
        }
    }
}