using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;
using ImagesGallery.Utils;
using ImagesGallery.Model;

namespace ImagesGallery.ViewModels
{
    class MainViewModel : PropertyChangedBase
    {
        private IImagesPathLoader imagesPathLoader;

        public MainViewModel(IImagesPathLoader imagesPathLoader)
        {
            this.imagesPathLoader = imagesPathLoader;
        }

        private string _windowTitle = "ImagesGallery";
        public string WindowTitle
        {
            get { return _windowTitle; }
            set
            {
                _windowTitle = value;
                NotifyOfPropertyChange(() => WindowTitle);
            }
        }

        public ObservableCollection<string> _images;
        public ObservableCollection<string> Images
        {
            get { return _images; }
            set
            {
                _images = value;
                NotifyOfPropertyChange(() => Images);
            }
        }

        public void LoadDirectory()
        {
            ImageBatch batch = imagesPathLoader?.LoadImagePaths();
            if (batch != null)
            {
                WindowTitle = batch.SourceLabel;
                Images = new ObservableCollection<string>(batch.ImagePaths);
            }
        }

        public void CloseApplication()
        {
            App.Current.Shutdown();
        }
    }
}
