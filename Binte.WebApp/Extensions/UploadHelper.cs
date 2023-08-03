using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Binte.WebApp.Extensions
{
    public class UploadHelper
    {
        public static string Upload(IFormFile file,string folder)
        {            
            if (!Directory.Exists($"wwwroot/Uploads/{folder}/"))
            {
                Directory.CreateDirectory($"wwwroot/Uploads/{folder}/");
            }
            var path = $"Uploads/{folder}/" + Guid.NewGuid().ToString("N") + file.FileName;
            var fs = System.IO.File.OpenWrite("wwwroot/" + path);
            file.CopyTo(fs);
            fs.Close();
            return path;
        }
    }
}
