using Microsoft.AspNetCore.Http;
using System.Drawing;
using Xabe.FFmpeg;


namespace BlogCore.Utilidades
{
    public static class VideosUtility
    {
        public static void eliminarVideo(string ruta)
        {
            if (File.Exists(ruta))
                System.IO.File.Delete(ruta);
        }

        public static async Task<Dictionary<string, string>> guardarVideo(string rutaScreenshot, string rutaLocal, string rutaRoot, IFormFile file)
        {
            Dictionary<string, string> rutas = new Dictionary<string, string>();

            string rutaAbsoluta = $"{rutaRoot}{rutaLocal}";
            string rutaAbsolutaScreenshot = $"{rutaAbsoluta}{rutaScreenshot}";

            if (!Directory.Exists(rutaAbsoluta))
                Directory.CreateDirectory(rutaAbsoluta);

            if(!Directory.Exists(rutaAbsolutaScreenshot))
                Directory.CreateDirectory(rutaAbsolutaScreenshot);

            //guardar el video en la ruta 
            FileInfo infoArchivo = new FileInfo(file.FileName);
            string guiImagen = $"{Guid.NewGuid().ToString()}{infoArchivo.Extension}";
            string rutaCompleta = $"{rutaAbsoluta}{guiImagen}";

            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            //si existe el video guardado entonces tomamos un screenshot.
            if (File.Exists(rutaCompleta))
            {
                string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Herramientas");
                FFmpeg.SetExecutablesPath(ruta);

                string nombreScreenshot = $"{Guid.NewGuid()}.jpg";
                
                var resultado = await FFmpeg.Conversions.New()
                    .AddParameter($"-ss {5} -i \"{rutaCompleta}\" -frames:v 1 \"{$"{rutaAbsolutaScreenshot}{nombreScreenshot}"}\"", ParameterPosition.PreInput)
                    .Start();

                //dibujamos justamente algo en la imagen ya agregada en el sistema de archivos
                string nombreImagenDibujada = dibujarEnImagen($"{rutaAbsolutaScreenshot}", rutaRoot, $"{rutaAbsolutaScreenshot}{nombreScreenshot}");

                rutas.Add("rutaScreenshot", $"{rutaLocal}{rutaScreenshot}{nombreImagenDibujada}");
            }

            rutas.Add("rutaVideo", $"{rutaLocal}{guiImagen}");

            return rutas;
        }

        public static string dibujarEnImagen(string rutaAbsolutaScreenshot, string rutaRoot, string rutaImagenScreenshot)
        {
            Bitmap bitmap = new Bitmap(Image.FromFile(rutaImagenScreenshot), width: 800, height: 800);
            Graphics grafico = Graphics.FromImage(bitmap);
           
            string rutaIcono = $"{rutaRoot}\\pictures\\personalizacion\\icon_play.png";
            
            if ( File.Exists(rutaIcono) )
            {
                Image imagen = Image.FromFile(rutaIcono);
                grafico.DrawImage(imagen, new Point() { X = (imagen.Width*30)/100, Y = (imagen.Height*25)/100 });
            }

            string nombreScreenshot = $"{Guid.NewGuid()}.jpg";
            string imagenNueva = $"{rutaAbsolutaScreenshot}{nombreScreenshot}";
            bitmap.Save(imagenNueva);

            File.Delete(rutaImagenScreenshot);

            return nombreScreenshot;
        }
    }
}
