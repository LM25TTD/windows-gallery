using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesGallery.Model
{
    /// <summary>
    /// Model to carries a directory load information,
    /// bringing the folder and files URIs.
    /// </summary>
    class ImageBatch
    {
        public ImageBatch(string sourceLabel, List<string> imagePaths)
        {
            SourceLabel = sourceLabel;
            ImagePaths = imagePaths;
        }

        public string SourceLabel
        {
            get;
            set;
        }

        public List<string> ImagePaths
        {
            get;
            set;
        }
    }
}
