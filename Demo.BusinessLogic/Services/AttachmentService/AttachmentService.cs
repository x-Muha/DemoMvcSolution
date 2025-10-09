using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.BusinessLogic.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        List<string> allowedExtensions = [".jpg", ".jpeg", ".png"];
        const int maxSize = 2*1_048_576; // 1MB
        public string? Upload(IFormFile file, string folderName)
        {
            //1.Check Extension
            var extension = Path.GetExtension(file.FileName);
            if (!allowedExtensions.Contains(extension)) return null;
            //2.Check Size
            if (file.Length ==0 || file.Length > maxSize) return null;
            //3.Get Located Folder Path
            //var folderPath = $"{Directory.GetCurrentDirectory()}/wwwroot/Files/{folderName}";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","Files",folderName);
            //4.Make Attachment Name Unique-- GUID
            var fileName = $"{Guid.NewGuid()}_{file.Name}.{extension}";
            //5.Get File Path
            var filePath = Path.Combine(folderPath, fileName);
            //6.Create File Stream To Copy File[Unmanaged]
            using FileStream fs = new FileStream(filePath, FileMode.Create);
            //7.Use Stream To Copy File
            file.CopyTo(fs);
            //8.Return FileName To Store In Database
            return fileName;
        }
        public bool Delete(string filePath)
        {
            if(!File.Exists(filePath)) return false;

            File.Delete(filePath);
            return true;
        }
    }
}
