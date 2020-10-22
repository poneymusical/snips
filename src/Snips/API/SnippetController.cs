using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Snips.Domain.BusinessObjects;
using Snips.Domain.Interfaces;

namespace Snips.API
{
    [Route("api/snippets")]
    public class SnippetController : ControllerBase
    {
        private readonly IRepository<Snippet> _repository;

        public SnippetController(IRepository<Snippet> repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _repository.GetSingle(id));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var snippet = await _repository.GetSingle(id);
            if (snippet == null)
                return NotFound();
            await _repository.Delete(snippet);
            return NoContent();
        }
    }
}