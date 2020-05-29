using System;
using System.ComponentModel.DataAnnotations;

namespace neobooru.Models
{
    public class Tag
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public NeobooruUser Creator { get; set; }

        [Required]
        public string TagString { get; set; }

        [Required]
        public DateTime AddedAt { get; set; }

        // TODO: If there is enough time then do this
        //public TagType Type { get; set; }
        //public enum TagType
        //{
        //    Character,
        //    Series,
        //    Metadata,
        //    General
        //}
    }
}
