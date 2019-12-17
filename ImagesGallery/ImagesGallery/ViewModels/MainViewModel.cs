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
    /// <summary>
    /// Root view model of the application. It function is get the result of load a directory
    /// images and set it references to a ObservableCollection, this can be consumed by
    /// the View layer through binding
    /// </summary>
    class MainViewModel : Screen
    {
        /// <summary>
        /// Delegate the images loader procces to a interface service, so we can
        /// replace it implementation easily.
        /// </summary>
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

        /// <summary>
        /// Load action:
        /// Call the image loader service and update the Images collection
        /// </summary>
        public void LoadDirectory()
        {
            ImageBatch batch = imagesPathLoader?.LoadImagePaths();
            if (batch != null)
            {
                WindowTitle = "Loaded folder: " + batch.SourceLabel;
                Images = new ObservableCollection<string>(batch.ImagePaths);
            }
        }

        /// <summary>
        /// Close app action.
        /// </summary>
        public void CloseApplication()
        {
            App.Current.Shutdown();
        }

        /// <summary>
        /// Show detail action. Must be bind to image click UI action.
        /// </summary>
        /// <param name="imageUrl"></param>
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
