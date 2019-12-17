using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
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

        protected override void OnActivate()
        {
            base.OnActivate();
            if(ImageSource != null)
            {
                Image = new BitmapImage(new Uri(ImageSource));
            }
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

        private BitmapImage _image = null;
        public BitmapImage Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                NotifyOfPropertyChange(() => Image);
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

        public IImageProcessor CurrentImageProcessor
        {
            get;
            set;
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

        public async void ApplyImageProcessor()
        {
            if(CurrentImageProcessor != null)
            {
                var uiContext = TaskScheduler.FromCurrentSynchronizationContext();

                CurrentImageProcessor.SetImageSource(ImageSource);

                // Enforce update image to run on UI thread
                await Task.Factory.StartNew(async () => {
                    BitmapImage result = await CurrentImageProcessor.ProcessImage();
                    Image = result;
                } , CancellationToken.None, TaskCreationOptions.None, uiContext);

            }
        }
    }
}
