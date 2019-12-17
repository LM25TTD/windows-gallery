using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace ImagesGallery.ViewModels
{
    class ImageDetailsViewModel : Screen
    {
        public ImageDetailsViewModel()
        {
        }

        private string _imageSource = null;
        public string ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                NotifyOfPropertyChange(() => ImageSource);
            }
        }

        private string _windowTitle = "Image Details";
        public string WindowTitle
        {
            get
            {
                return _windowTitle;
            }
            set
            {
                _windowTitle = value;
                NotifyOfPropertyChange(()=> WindowTitle);
            }
        }
    }
}
