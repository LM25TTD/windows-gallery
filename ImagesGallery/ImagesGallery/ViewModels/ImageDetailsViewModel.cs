using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using ImagesGallery.Services;

namespace ImagesGallery.ViewModels
{
    class ImageDetailsViewModel : Screen
    {
        public ImageDetailsViewModel()
        {
            LoadImageProcessors();
        }

        public ObservableCollection<IImageProcessor> _imageProcessors = null;
        public ObservableCollection<IImageProcessor> ImageProcessors
        {
            get
            {
                return _imageProcessors;
            }
            set
            {
                _imageProcessors = value;
                NotifyOfPropertyChange(()=> ImageProcessors);
            }
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

        private void LoadImageProcessors()
        {
            IEnumerable<object> imageProcessors =
                IoC.GetAllInstances(typeof(IImageProcessor));

            if (imageProcessors != null)
            {
                ImageProcessors = 
                    new ObservableCollection<IImageProcessor>(imageProcessors.Cast<IImageProcessor>());
            }
        }
    }
}
