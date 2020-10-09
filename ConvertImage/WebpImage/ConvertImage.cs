using System.Drawing;
using System.IO;
using ImageProcessor.Imaging.Formats;
using ImageProcessor;

namespace WebpImage.Convert
{
    public class ConvertImage
    {
        public string ConvertWebp(string ruta, float maxHeight, float maxWidth)
        {
            Image image = Image.FromFile(ruta);
            var height = (float)(image.Height);
            var width = (float)(image.Width);

            float prop = 0;
            //Se calculan los valores proporcionales al maximo ancho y alto requerido
            if (height > width)
            {
                prop = CalcularProporcionH(height, width);
                height = maxHeight;
                width = maxHeight / prop;
            }
            else if (width > height)
            {
                prop = CalcularProporcionW(height, width);
                width = maxWidth;
                height = maxWidth / prop;
            }
            else if (height == width)
            {
                height = maxHeight;
                width = maxWidth;
            }
            image.Dispose();


            //Leo la imagen en arreglo matricial de una ruta especifica
            byte[] imageBytes = File.ReadAllBytes(ruta);
            // Format is automatically detected though can be changed.
            //ISupportedImageFormat format = new PngFormat { Quality = 70 };
            ISupportedImageFormat format = new JpegFormat { Quality = 70 };

            Size size = new Size((int)width, (int)height);
            using (MemoryStream inStream = new MemoryStream(imageBytes))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    // Inicializa ImageFactory sobrecargando EXIF metadata.
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                    {
                        // Carga, redimensiona, formatea y lo pone en la salida
                        imageFactory.Load(inStream)
                                    //.Resize(size)
                                    .Format(format)
                                    .Save(outStream);
                        //Guarda la imagen en formato webp
                        //imageFactory.Save(@"E:\Img\PruebaRefactory.webp");
                        var baseImage = (byte[])(new ImageConverter()).ConvertTo(imageFactory.Image, typeof(byte[]));
                        string base64encodedImage = System.Convert.ToBase64String(baseImage);
                        imageFactory.Dispose();
                        return base64encodedImage;
                    }
                }
            }
        }
        public float CalcularProporcionH(float hh, float wh)
        {
            float propH = 0;
            propH = (float)(hh / wh);
                      
            return  propH;
        }
        public float CalcularProporcionW(float hh, float wh)
        {
            float propW = 0;
            propW = (float)(wh / hh);


            return propW;
        }
    }
}
