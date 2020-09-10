using System;
using System.Collections.Generic;

namespace neobooru.ViewModels
{
    public class TimelinePostViewModel
    {
        public string ArtistName { get; set; }
        
        public string PfpUrl { get; set; }
        
        public DateTime CreationTime { get; set; }
        
        public string PostDescription { get; set; }
        
        public string ArtId { get; set; }
        
        public string ArtUrl { get; set; }
        
        public List<CommentViewModel> RecentComments { get; set; }
    }
}