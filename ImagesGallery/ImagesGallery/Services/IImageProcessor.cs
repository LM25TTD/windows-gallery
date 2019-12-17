using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ImagesGallery.Services
{
    interface IImageProcessor
    {
        string Label { get; }
        Task<BitmapImage> ProcessImage();
        void SetImageSource(string source);
    }
}
