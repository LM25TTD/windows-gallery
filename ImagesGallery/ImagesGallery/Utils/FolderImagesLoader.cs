﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.ObjectModel;
using ImagesGallery.Model;

namespace ImagesGallery.Utils
{
    class FolderImagesLoader : IImagesPathLoader
    {
        private Regex rgxPictureFiles = new Regex(@"bmp|png|jpg|gif");

        public ImageBatch LoadImagePaths()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                Boolean pathIsValid = !string.IsNullOrWhiteSpace(fbd.SelectedPath);

                if (result == DialogResult.OK && pathIsValid)
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                    List<string> pictureFiles = new List<string>();

                    foreach (string file in files)
                    {
                        if (rgxPictureFiles.Match(file).Success)
                        {
                            pictureFiles.Add(file);
                        }
                    }
                    return new ImageBatch(fbd.SelectedPath, pictureFiles);
                }
            }

            return null;
        }
    }
}