using System;
using Newtonsoft.Json;
using Snips.Domain.BusinessObjects.Base;

namespace Snips.Domain.BusinessObjects
{
    public class Directory : IIdentifiable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public Guid? ParentDirectoryId { get; set; }
        [JsonIgnore]
        public bool IsRoot => !ParentDirectoryId.HasValue;
    }
}