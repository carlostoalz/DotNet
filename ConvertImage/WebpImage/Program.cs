using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebpImage.Convert;
using ImageProcessor.Imaging.Formats;
using ImageProcessor;
using System.IO;

namespace WebpImage
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "E:/Img/Prueba.jpg";
            if (File.Exists(url))
            {
                ConvertImage convertImage = new ConvertImage();
                string image = convertImage.ConvertWebp(url, 1024, 1024);

                string baseImage = $"data: image/jpeg; base64, {image}";
                convertImage = null;
            }

        }



    }
}
