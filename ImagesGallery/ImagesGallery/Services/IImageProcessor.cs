using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ImagesGallery.Services
{
    /// <summary>
    /// Interface abstraction for image processor services. Every processor must implement
    /// this interface to become usable inside the ViewModels.
    /// </summary>
    interface IImageProcessor
    {
        /// <summary>
        /// UI Label to be shown for the user
        /// </summary>
        string Label { get; }

        /// <summary>
        /// Generic data storage that can be used as message holder to callers.
        /// </summary>
        List<object> Metadata { get; }

        /// <summary>
        /// Apply the processor logic to the source image and return a BitmapImage to 
        /// be shown on the UI.
        /// </summary>
        /// <returns>BitmapImage instance</returns>
        Task<BitmapImage> ProcessImage();

        /// <summary>
        /// Stores the image URI where the processor will load the image data.
        /// </summary>
        /// <param name="source"></param>
        void SetImageSource(string source);
    }
}
