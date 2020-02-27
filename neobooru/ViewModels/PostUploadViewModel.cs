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
        public readonly String File;

        public readonly string Name;

        public readonly string Author;

        public readonly string Source;

        public readonly Art.ArtRating Rating;

        public readonly ICollection<Tag> tags;

        // private PostUploadViewModel() { }
        // public PostUploadViewModel()
    }
}
