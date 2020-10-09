using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using GroupDocs.Metadata.Formats.Image;
using Imazen.WebP;
using System.IO;

namespace ConvertImage
{
    public class ProcesarImagen
    {
        public Image Leer()
        {
            Image image = Image.FromFile("C:/Despliegue/Imagenes/eagle.png");
            return image;
        }
        public void Convertir()
        {
            var encoder = new SimpleEncoder();
            var fileNameIn = "C:/Despliegue/Imagenes/eagle1.png";
            var fileNameOut = "C:/Despliegue/Imagenes/eagle.webp";
            File.Delete(fileNameOut);
            Bitmap mBitmap;
            FileStream outStream = new FileStream(fileNameOut, FileMode.Create);
            using (Stream BitmapStream = System.IO.File.Open(fileNameIn, System.IO.FileMode.Open))
            {
                Image img = Image.FromStream(BitmapStream);

                mBitmap = new Bitmap(img);        
                //encoder.Encode(mBitmap, outStream, 100, false);
            }

            //FileInfo finfo = new FileInfo(fileNameOut);
            //Assert.True(finfo.Exists);
        }
        public void ExtraerPropiedades(Image _imgProp)
        {
            
        }
        public Bitmap ConvertirGuardar(Image imgSave)
        {
            imgSave.Save("C:/Despliegue/Imagenes/eagle_png_jpg.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            imgSave.Save("C:/Despliegue/Imagenes/eagle_png_tiff.tiff", System.Drawing.Imaging.ImageFormat.Tiff);
            imgSave.Save("C:/Despliegue/Imagenes/eagle_png_bmp.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            imgSave.Save("C:/Despliegue/Imagenes/eagle_png_png.png", System.Drawing.Imaging.ImageFormat.Png);
            var h = imgSave.VerticalResolution;
            var w = imgSave.HorizontalResolution;
            var fdH = imgSave.Height;
            var fdW = imgSave.Width;

            if (fdH>1024 || fdW>1024)
            {
                Bitmap bitmap = new Bitmap(imgSave, 1024, 600);
                return bitmap;
            }
            else
            {
                Bitmap bitmap = new Bitmap(imgSave);
                return bitmap;
            }
            
        }
        public void Redimensionar(Bitmap bitmap)
        {
            
            //bitmap.SetResolution(1024, 1024);
            bitmap.Save("C:/Despliegue/Imagenes/eagle_redi.png", System.Drawing.Imaging.ImageFormat.Png);
            bitmap.Save("C:/Despliegue/Imagenes/eagle_redi.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            
        }
    }

}

