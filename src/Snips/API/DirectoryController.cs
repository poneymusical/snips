using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Snips.Domain.BusinessObjects;
using Snips.Domain.Interfaces;

namespace Snips.API
{
    [Route("api/directories")]
    public class DirectoryController : ControllerBase
    {
        private readonly IRepository<Directory> _repository;
        private readonly IValidator<Directory> _validator;

        public DirectoryController(
            IRepository<Directory> repository, 
            IValidator<Directory> validator)
        {
            _repository = repository;
            _validator = validator;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id) => 
            Ok(await _repository.GetSingle(id));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Directory directory)
        {
            directory.Id = Guid.NewGuid();
            var validationResult = await _validator.ValidateAsync(directory);
            if (!validationResult.IsValid)
                return BadRequest();
            await _repository.Insert(directory);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Directory directory)
        {
            var validationResult = await _validator.ValidateAsync(directory);
            if (!validationResult.IsValid)
                return BadRequest();
            await _repository.Update(directory);
            return NoContent();
        }
        
        //TODO: for deletion, all snippets and children directories should be moved to root
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var directory = await _repository.GetSingle(id);
            if (directory == null)
                return NotFound();
            await _repository.Delete(directory);
            return NoContent();
        }
    }
}