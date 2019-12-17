using ImagesGallery.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ImagesGallery.Providers
{
    class FacesDetectorImageProcessor : IImageProcessor
    {
        private string imageSource;

        public string Label
        {
            get
            {
                return "Faces Detector";
            }
        }

        public Task<BitmapImage> ProcessImage()
        {
            Task<BitmapImage> task = new Task<BitmapImage>(() =>
            {
                return new BitmapImage(new Uri(this.imageSource));
            });

            task.Start();

            return task;
        }

        public void SetImageSource(string source)
        {
            this.imageSource = source;
        }
    }
}
