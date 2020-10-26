using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Snips.Domain.BusinessObjects;
using Snips.Domain.Interfaces;
using Snips.ViewModels;

namespace Snips.Services.Directories
{
    public class TreeviewService : ITreeviewService
    {
        private readonly IRepository<Directory> _directoryRepository;
        private readonly IRepository<Snippet> _snippetRepository;

        public TreeviewService(
            IRepository<Directory> directoryRepository,
            IRepository<Snippet> snippetRepository)
        {
            _directoryRepository = directoryRepository;
            _snippetRepository = snippetRepository;
        }

        public async Task<IList<TreeviewItem>> GetDirectoriesAsTree()
        {
            var directories = await _directoryRepository.GetAll(); //Will change when UAC is added

            var tree = directories
                .Where(dir => dir.IsRoot)
                .Select(dir => BuildTree(dir, directories))
                .ToList();

            return tree;
        }

        public async Task<IList<TreeviewItem>> GetDirectoriesAndSnippetsAsTree()
        {
            var directories = await _directoryRepository.GetAll();
            var snippets = await _snippetRepository.GetAll();

            var tree = directories
                .Where(dir => dir.IsRoot)
                .Select(dir => BuildTree(dir, directories, snippets))
                .ToList();

            tree.AddRange(snippets
                .Where(s => s.IsSolo)
                .Select(s => new TreeviewItem(s)));

            return tree;
        }

        private static TreeviewItem BuildTree(Directory directory, IList<Directory> directories, IList<Snippet> snippets = null)
        {
            var tree = new TreeviewItem(directory);

            snippets?.Where(s => s.DirectoryId == directory.Id)
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