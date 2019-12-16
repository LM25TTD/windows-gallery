using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace ImagesGallery.ViewModels
{
    class MainViewModel : PropertyChangedBase
    {
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
    }
}
