using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.BusinessLogic.Services.AttachmentService
{
    public interface IAttachmentService
    {
        // Upload
        public string Upload(IFormFile file, string folderName);
        // Delete
        public bool Delete(string filePath);
    }
}
