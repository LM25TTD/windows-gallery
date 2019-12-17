using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesGallery.Model
{
    public class Eye
    {
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Nose
    {
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Mouth
    {
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Features
    {
        public List<Eye> eyes { get; set; }
        public Nose nose { get; set; }
        public Mouth mouth { get; set; }
    }

    public class Face
    {
        public string orientation { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Features features { get; set; }
    }

    public class Image
    {
        public int width { get; set; }
        public int height { get; set; }
    }

    public class FacesApiResponse
    {
        public List<Face> faces { get; set; }
        public Image image { get; set; }
    }
}
