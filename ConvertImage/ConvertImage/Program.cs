using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConvertImage
{
    class Program1
    {
        static void Main(string[] args)
        {

            ProcesarImagen procImge = new ProcesarImagen();
            var image= procImge.Leer();
            var bitmap=procImge.ConvertirGuardar(image);            
            procImge.Redimensionar(bitmap);
            procImge.Convertir();
            procImge.ExtraerPropiedades(image);
            //var imageConverted=procImge.Convertir(image);

        }
    }
}
