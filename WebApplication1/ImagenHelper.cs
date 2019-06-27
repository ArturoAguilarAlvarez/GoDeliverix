using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace WebApplication1
{
    public class ImagenHelper
    {

        public static Image RedimensionarImagen(Stream stream)
        {
            //Se crea un objeto Image, que contiene las propiedades de la imagen
            Image img = Image.FromStream(stream);

            //Tamaño máximo de la imagen(altura o anchura)
            const int max = 200;

            int h = img.Height;
            int w = img.Width;
            int newH, newW;

            if (h > w && h > max)
            {
                //Si la imagen es vertical y la altura es mayor que max,
                //se redefinen las dimensiones.
                newH = max;
                newW = (w * max) / h;
            }
            else if (w > h && w > max)
            {
                //Si la imagen es horizontal y la anchura es mayor que max,
                //se redefinen las dimensiones.
                newW = max;
                newH = (h * max) / w;
            }
            else
            {
                newH = h;
                newW = w;
            }
            if (h != newH && w != newW)
            {
                //Si las dimensiones cambiaron, se modifica la imagen
                Bitmap newImg = new Bitmap(img, newW, newH);
                Graphics g = Graphics.FromImage(newImg);
                g.InterpolationMode =
                  InterpolationMode.HighQualityBilinear;
                g.DrawImage(img, 0, 0, newImg.Width, newImg.Height);
                return newImg;
            }
            else
                return img;
        }

        public Image RedimensionarImagen(Image pImagen, int pAncho = 300, int pAlto = 300)
        {
            //creamos un bitmap con el nuevo tamaño

            Bitmap vBitmap = new Bitmap(pAncho, pAlto);

            //creamos un graphics tomando como base el nuevo Bitmap
            using (Graphics vGraphics = Graphics.FromImage((Image)vBitmap))

            {
                //especificamos el tipo de transformación, se escoge esta para no perder calidad.
                vGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                //Se dibuja la nueva imagen
                vGraphics.DrawImage(pImagen, 0, 0, pAncho, pAlto);

            }

            //retornamos la nueva imagen
            return (Image)vBitmap;

        }

    }
}