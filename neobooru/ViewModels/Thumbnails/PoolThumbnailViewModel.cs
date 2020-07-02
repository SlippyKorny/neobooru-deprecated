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

        public PoolThumbnailViewModel(Pool pool, string creator)
        {
            PoolName = pool.PoolName;
            UserName = creator;
            CreationDate = pool.CreatedAt;
            LastUpdateDate = pool.UpdatedAt;
            thumbnails = pool.Arts.Select(e => e.PreviewFileUrl).ToArray();
        }
    }
}
