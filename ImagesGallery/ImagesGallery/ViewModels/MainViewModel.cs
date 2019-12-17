using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;
using ImagesGallery.Services;
using ImagesGallery.Model;

namespace ImagesGallery.ViewModels
{
    class MainViewModel : Screen
    {
        private IImagesPathLoaderService imagesPathLoader;
        private ImageDetailsViewModel imageDetailsViewModel;
        private IWindowManager windowManager;

        public MainViewModel(
            IImagesPathLoaderService imagesPathLoader, 
            ImageDetailsViewModel imageDetailsViewModel,
            IWindowManager windowManager)
        {
            this.imagesPathLoader = imagesPathLoader;
            this.imageDetailsViewModel = imageDetailsViewModel;
            this.windowManager = windowManager;
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
                WindowTitle = "Loaded folder: " + batch.SourceLabel;
                Images = new ObservableCollection<string>(batch.ImagePaths);
            }
        }

        public void CloseApplication()
        {
            App.Current.Shutdown();
        }

        public void ShowImageDetail(string imageUrl)
        {
            this.imageDetailsViewModel.ImageSource = imageUrl;
            this.imageDetailsViewModel.WindowTitle = "Loaded image: " + imageUrl;

            if (!this.imageDetailsViewModel.IsActive)
            {
                this.windowManager.ShowWindow(this.imageDetailsViewModel);
            }
        }
    }
}
