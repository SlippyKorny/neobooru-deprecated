using System;

namespace neobooru.ViewModels
{
    public class CommentViewModel
    {
        public readonly string AuthorName;

        public readonly string PfpUrl;
        
        public readonly string Content;

        public readonly DateTime Date;

        public CommentViewModel(string authorName, string pfpUrl, string content, DateTime date)
        {
            AuthorName = authorName;
            PfpUrl = pfpUrl;
            Content = content;
            Date = date;
        }
    }
}