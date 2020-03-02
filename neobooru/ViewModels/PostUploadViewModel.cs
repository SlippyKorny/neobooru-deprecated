using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using neobooru.Models;

namespace neobooru.ViewModels
{
    public class PostUploadViewModel
    {
        // TODO: < =====================================================================================================================================================================================
        public string File { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Source { get; set; }

        public Art.ArtRating Rating { get; set; }

        // public readonly ICollection<Tag> tags;
        public string TagString { get; set; }

        // private PostUploadViewModel() { }
        // public PostUploadViewModel()
    }
}
