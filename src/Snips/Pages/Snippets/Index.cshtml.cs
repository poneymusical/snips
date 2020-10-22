using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snips.Domain.BusinessObjects;
using Snips.Domain.Interfaces;
using Snips.ViewModels;

namespace Snips.Pages.Snippets
{
    public class Index : PageModel
    {
        private readonly IRepository<Directory> _directoryRepository;
        private readonly IRepository<Snippet> _snippetRepository;

        public Index(
            IRepository<Directory> directoryRepository, 
            IRepository<Snippet> snippetRepository)
        {
            _directoryRepository = directoryRepository;
            _snippetRepository = snippetRepository;
        }

        public List<TreeviewItem> Tree { get; set; }

        public async Task OnGet()
        {
            var directories = await _directoryRepository.GetAll();
            var snippets = await _snippetRepository.GetAll();
            
            Tree = directories
                .Where(dir => dir.IsRoot)
                .Select(dir => BuildTree(dir, directories, snippets))
                .ToList();
            
            Tree.AddRange(snippets
                .Where(s => s.IsSolo)
                .Select(s => new TreeviewItem(s))); 
        }

        private TreeviewItem BuildTree(Directory directory, IList<Directory> directories, IList<Snippet> snippets)
        {
            var tree = new TreeviewItem(directory);

            snippets
                .Where(s => s.DirectoryId == directory.Id)
                .Select(s => new TreeviewItem(s))
                .ToList()
                .ForEach(s => tree.AddChild(s));
            
            directories
                .Where(d => d.ParentDirectoryId == directory.Id)
                .Select(d => BuildTree(d, directories, snippets))
                .ToList()
                .ForEach(d => tree.AddChild(d));
            
            return tree;
        }
    }
}