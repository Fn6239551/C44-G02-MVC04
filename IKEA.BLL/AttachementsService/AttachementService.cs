using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.AttachementsService
{
    public class AttachementService : IAttachementService
    {
        private const long MaxSize = 5 * 1024 * 1024; // 5 MB مثلاً
        private const string FolderName = "Uploads";

        private readonly List<string> AllowedExtensions = new() { ".jpg", ".jpeg", ".png" };
        public bool Delete(string filePath)
        {
            if(!File.Exists(filePath)) return false;
            else
            {
                File.Delete(filePath);
            }
            return true;
        }
        public string Upload(IFormFile file)
        {
            return Upload(file, FolderName); // يخزن في الفولدر الافتراضي "Uploads"
        }
        public string Upload(IFormFile file,string folderName)
        {
          var Extention=Path.GetExtension(file.FileName);
            if (!AllowedExtensions.Contains(Extention)) return null;
            if (file.Length == 0 || file.Length > MaxSize) return null;
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath=Path.Combine(FolderPath, fileName);
            using FileStream FS = new FileStream(filePath, FileMode.Create);
            file.CopyTo(FS);
            return fileName;

        }
    }
}
