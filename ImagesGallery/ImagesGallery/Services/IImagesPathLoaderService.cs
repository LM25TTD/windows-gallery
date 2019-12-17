using ImagesGallery.Model;

namespace ImagesGallery.Services
{
    interface IImagesPathLoaderService
    {
        ImageBatch LoadImagePaths();
    }
}