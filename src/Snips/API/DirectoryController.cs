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
        private readonly IRepository<Directory> _directoryRepository;
        private readonly IRepository<Snippet> _snippetRepository;
        private readonly IValidator<Directory> _validator;

        public DirectoryController(
            IRepository<Directory> directoryRepository, 
            IValidator<Directory> validator, 
            IRepository<Snippet> snippetRepository)
        {
            _directoryRepository = directoryRepository;
            _validator = validator;
            _snippetRepository = snippetRepository;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id) => 
            Ok(await _directoryRepository.GetSingle(id));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Directory directory)
        {
            directory.Id = Guid.NewGuid();
            var validationResult = await _validator.ValidateAsync(directory);
            if (!validationResult.IsValid)
                return BadRequest();
            await _directoryRepository.Insert(directory);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Directory directory)
        {
            var validationResult = await _validator.ValidateAsync(directory);
            if (!validationResult.IsValid)
                return BadRequest();
            await _directoryRepository.Update(directory);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var directory = await _directoryRepository.GetSingle(id);
            if (directory == null)
                return NotFound();

            var childrenDirectories = await _directoryRepository.Get(d => d.ParentDirectoryId == directory.Id);
            foreach (var childDirectory in childrenDirectories)
            {
                childDirectory.ParentDirectoryId = directory.ParentDirectoryId;
                await _directoryRepository.Update(childDirectory);
            }

            var childrenSnippets = await _snippetRepository.Get(s => s.DirectoryId == directory.Id);
            foreach (var childSnippet in childrenSnippets)
            {
                childSnippet.DirectoryId = directory.ParentDirectoryId;
                await _snippetRepository.Update(childSnippet);
            }
            
            await _directoryRepository.Delete(directory);
            return NoContent();
        }
    }
}