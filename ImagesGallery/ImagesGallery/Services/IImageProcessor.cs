using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImagesGallery.Services
{
    interface IImageProcessor
    {
        string Label { get; }
        ICommand ProcessImage { get; }
        void SetImageSource(string source);
    }
}
