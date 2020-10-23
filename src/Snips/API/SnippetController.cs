using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Snips.Domain.BusinessObjects;
using Snips.Domain.Interfaces;

namespace Snips.API
{
    [Route("api/snippets")]
    public class SnippetController : ControllerBase
    {
        private readonly IRepository<Snippet> _repository;
        private readonly IValidator<Snippet> _validator;

        public SnippetController(
            IRepository<Snippet> repository, 
            IValidator<Snippet> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _repository.GetSingle(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Snippet snippet)
        {
            snippet.Id = Guid.NewGuid();
            var validationResult = await _validator.ValidateAsync(snippet);
            if (!validationResult.IsValid)
                return BadRequest();
            await _repository.Insert(snippet);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Snippet snippet)
        {
            var validationResult = await _validator.ValidateAsync(snippet);
            if (!validationResult.IsValid)
                return BadRequest();
            await _repository.Update(snippet);
            return NoContent();
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