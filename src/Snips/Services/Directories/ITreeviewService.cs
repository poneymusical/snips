using System.Collections.Generic;
using System.Threading.Tasks;
using Snips.ViewModels;

namespace Snips.Services.Directories
{
    public interface ITreeviewService
    {
        Task<IList<TreeviewItem>> GetDirectoriesAsTree();
        Task<IList<TreeviewItem>> GetDirectoriesAndSnippetsAsTree();
    }
}