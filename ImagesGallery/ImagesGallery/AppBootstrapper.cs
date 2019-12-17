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
    class AppBootstrapper: BootstrapperBase
    {
        private SimpleContainer container;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }

        protected override void Configure()
        {
            container = new SimpleContainer();

            container.Singleton<IWindowManager, WindowManager>();
       
            container.Singleton<MainViewModel>();

            container.Singleton<ImageDetailsViewModel>();

            container.PerRequest<IImagesPathLoaderService, FolderImagesLoaderService>();

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
