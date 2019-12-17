using ImagesGallery.Model;

namespace ImagesGallery.Services
{
    /// <summary>
    /// Interface abstraction for image path loader services. Them implementations 
    /// can find URI's from whatever and returns them as ImageBatch model.
    /// </summary>
    interface IImagesPathLoaderService
    {
        ImageBatch LoadImagePaths();
    }
}