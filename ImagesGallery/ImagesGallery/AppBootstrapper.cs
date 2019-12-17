using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using ImagesGallery.ViewModels;
using ImagesGallery.Services;
using ImagesGallery.Providers;


namespace ImagesGallery
{
    /// <summary>
    /// Caliburn Bootstrap for the app. Configures the application environment by defining
    /// the container, registering instances and defining the initial view to be loaded.
    /// </summary>
    class AppBootstrapper: BootstrapperBase
    {
        /// <summary>
        /// The IoC container
        /// </summary>
        private SimpleContainer container;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            // Configure the root view
            DisplayRootViewFor<MainViewModel>();
        }

        /// <summary>
        /// Here is the place to register all instances of IoC
        /// </summary>
        protected override void Configure()
        {
            container = new SimpleContainer();

            container.Singleton<IWindowManager, WindowManager>();
       
            container.Singleton<MainViewModel>();

            container.Singleton<ImageDetailsViewModel>();

            // Register a single type service

            container.PerRequest<IImagesPathLoaderService, FolderImagesLoaderService>();

            // We can register many implementations for one service by defining a common
            // interface.

            container.PerRequest<IImageProcessor, FacesDetectorImageProcessor>();

            container.PerRequest<IImageProcessor, OriginalContentImageProcessor>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}
