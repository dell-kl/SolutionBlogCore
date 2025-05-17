using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace BlogCore.Utilidades
{
    public static class VideosUtility
    {
        public static void eliminarVideo(string ruta)
        {
            if (File.Exists(ruta))
                System.IO.File.Delete(ruta);
        }

        public static string guardarVideo(string rutaScreenshot, string rutaLocal, string rutaAbsoluta, IFormFile file)
        {
            rutaAbsoluta = $"{rutaAbsoluta}{rutaLocal}";
            rutaScreenshot = $"{rutaAbsoluta}{rutaScreenshot}";

            if (!Directory.Exists(rutaAbsoluta))
                Directory.CreateDirectory(rutaAbsoluta);

            if(!Directory.Exists(rutaScreenshot))
                Directory.CreateDirectory(rutaScreenshot);

            
            FileInfo infoArchivo = new FileInfo(file.FileName);
            string guiImagen = $"{Guid.NewGuid().ToString()}{infoArchivo.Extension}";
            string rutaCompleta = $"{rutaAbsoluta}{guiImagen}";

            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            rutaScreenshot = $"{rutaScreenshot}{Guid.NewGuid()}.png";
            string ffmpeg_cmd = $"-i \"{rutaCompleta}\" \"{rutaScreenshot}\"";

            Process proceso = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = "ffmpeg",
                    Arguments = ffmpeg_cmd,
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            proceso.Start();
            proceso.WaitForExit();
            
            return $"{rutaLocal}{guiImagen}";
        }
    }
}
