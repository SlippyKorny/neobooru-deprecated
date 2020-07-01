using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using neobooru.Models;

namespace neobooru.ViewModels
{
    public class PoolThumbnailViewModel
    {
        public readonly string PoolName;

        public readonly string UserName;

        public readonly DateTime CreationDate;

        public readonly DateTime LastUpdateDate;

        public readonly string[] thumbnails = new string[3];

        private PoolThumbnailViewModel() { }

        // TODO: Change the constructor after u add User reference to the model
        public PoolThumbnailViewModel(Pool pool, string creator)
        {
            PoolName = pool.PoolName;
            UserName = creator;
            CreationDate = pool.CreatedAt;
            LastUpdateDate = pool.UpdatedAt;
            var artEnumerator = pool.Arts.GetEnumerator();
            artEnumerator.MoveNext();
            thumbnails[0] = artEnumerator.Current.PreviewFileUrl;
            artEnumerator.MoveNext();
            thumbnails[1] = artEnumerator.Current.PreviewFileUrl;
            artEnumerator.MoveNext();
            thumbnails[2] = artEnumerator.Current.PreviewFileUrl;
        }
    }
}
