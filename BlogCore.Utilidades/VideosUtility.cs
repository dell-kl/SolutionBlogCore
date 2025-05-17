using Microsoft.AspNetCore.Http;
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

        public static async Task<Dictionary<string, string>> guardarVideo(string rutaScreenshot, string rutaLocal, string rutaAbsoluta, IFormFile file)
        {
            Dictionary<string, string> rutas = new Dictionary<string, string>();

            rutaAbsoluta = $"{rutaAbsoluta}{rutaLocal}";
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

                rutas.Add("rutaScreenshot", $"{rutaLocal}{rutaScreenshot}{nombreScreenshot}");
            }

            rutas.Add("rutaVideo", $"{rutaLocal}{guiImagen}");

            return rutas;
        }
    }
}
