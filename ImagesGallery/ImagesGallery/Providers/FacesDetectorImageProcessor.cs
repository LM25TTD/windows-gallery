using ImagesGallery.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImagesGallery.Providers
{
    class FacesDetectorImageProcessor : IImageProcessor
    {
        public string Label
        {
            get
            {
                return "_Faces Detector";
            }
        }

        public ICommand ProcessImage
        {
            get
            {
                return null;
            }
        }

        public void SetImageSource(string source)
        {
            throw new NotImplementedException();
        }
    }
}
