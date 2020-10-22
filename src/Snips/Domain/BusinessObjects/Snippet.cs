using System;
using Newtonsoft.Json;
using Snips.Domain.BusinessObjects.Base;

namespace Snips.Domain.BusinessObjects
{
    public class Snippet : IIdentifiable
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        
        public Guid? DirectoryId { get; set; }

        [JsonIgnore] 
        public bool IsSolo => !DirectoryId.HasValue;
    }
}