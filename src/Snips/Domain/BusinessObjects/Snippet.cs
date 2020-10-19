using System;
using Newtonsoft.Json;

namespace Snips.Domain.BusinessObjects
{
    public class Snippet
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}