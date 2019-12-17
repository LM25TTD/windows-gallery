using ImagesGallery.Model;
using ImagesGallery.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using unirest_net.http;

namespace ImagesGallery.Providers
{
    class FacesDetectorImageProcessor : IImageProcessor
    {
        private string imageSource;

        public string Label
        {
            get
            {
                return "Faces Detector";
            }
        }

        private List<object> _metadata;
        public List<object> Metadata
        {
            get
            {
                return _metadata;
            }
            private set
            {
                _metadata = value;
            }
        }

        private async Task<HttpResponse<FacesApiResponse>> PostToRapidApi()
        {
            // Do the network job in background thread
            return await Task.Run(() =>
            {
                try
                {
                    var fileBytes = File.ReadAllBytes(imageSource);

                    //FIXME: this is a sample call, for production, must be well structured
                    //TODO: at the moment of development, this API was not working. Check if
                    // the responses do the job properly in the future.
                    HttpResponse<FacesApiResponse> response =
                           Unirest.post("https://apicloud-facerect.p.rapidapi.com/process-file.json")
                        .header("X-RapidAPI-Host", "apicloud-facerect.p.rapidapi.com")
                        .header("X-RapidAPI-Key", "1785465fb7msh15e4ca24f5f41e7p151dc0jsn44e34a8fa1e4")
                        .header("Content-Type", "application/x-www-form-urlencoded")
                        .field("image", fileBytes)
                        .asJson<FacesApiResponse>();

                    return response;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            });
        }

        private async Task<BitmapImage> DetectFaces()
        {
            // bmp is the original BitmapImage
            var bmp = new BitmapImage(new Uri(imageSource));
            var target = new RenderTargetBitmap(bmp.PixelWidth, bmp.PixelHeight, bmp.DpiX, bmp.DpiY, PixelFormats.Pbgra32);
            var visual = new DrawingVisual();

            HttpResponse<FacesApiResponse> apiResponse = await PostToRapidApi();

            using (var r = visual.RenderOpen())
            {
                if (apiResponse != null
                && apiResponse.Body != null
                && apiResponse.Body.faces != null
                && apiResponse.Body.faces.Count > 0)
                {
                    // Fills the processor metadata
                    Metadata = new List<object>
                    {
                        apiResponse.Body
                    };

                    foreach (Face face in apiResponse.Body.faces)
                    {
                        r.DrawImage(bmp, new Rect(face.x, face.y, face.width, face.height));
                    }
                }
                else
                {
                    r.DrawLine(new Pen(Brushes.Red, 10.0), new Point(0, 0), new Point(bmp.Width, bmp.Height));
                }
            }

            target.Render(visual);

            BitmapSource bitmapSource = target;

            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            MemoryStream memoryStream = new MemoryStream();

            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            encoder.Save(memoryStream);

            memoryStream.Position = 0;

            BitmapImage bImg = new BitmapImage();
            bImg.BeginInit();
            bImg.StreamSource = new MemoryStream(memoryStream.ToArray());
            bImg.EndInit();
            bImg.Freeze();

            return bImg;
        }

        public async Task<BitmapImage> ProcessImage()
        {
            BitmapImage image = await DetectFaces();

            return image;
        }

        public void SetImageSource(string source)
        {
            this.imageSource = source;
        }
    }
}
