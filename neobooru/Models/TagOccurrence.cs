using System;

namespace neobooru.Models
{
    public class TagOccurrence
    {
        public Guid TagId { get; set; }
        
        public Tag Tag { get; set; }
        
        public Guid ArtId { get; set; }
        
        public Art Art { get; set; }
    }
}