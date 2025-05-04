using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Utilidades
{
    public static class VideosUtility
    {
        public static void eliminarVideo(string ruta)
        {
            if (File.Exists(ruta))
                System.IO.File.Delete(ruta);
        }

        public static string guardarVideo(string rutaLocal, string rutaAbsoluta, IFormFile file)
        {
            rutaAbsoluta = $"{rutaAbsoluta}{rutaLocal}";

            if (!Directory.Exists(rutaAbsoluta))
                Directory.CreateDirectory(rutaAbsoluta);

            FileInfo infoArchivo = new FileInfo(file.FileName);
            string guiImagen = $"{Guid.NewGuid().ToString()}{infoArchivo.Extension}";
            string rutaCompleta = $"{rutaAbsoluta}{guiImagen}";

            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"{rutaLocal}{guiImagen}";
        }
    }
}
