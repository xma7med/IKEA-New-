using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Common.Services.Attachments
{
    public class AttachmentService : IAttachmentService
    {

        private readonly List<string> _allowedExtentions = new /*List<string>*/() { ".png" , ".jpg" , ".jpeg"};
        private const int _allowedMaxSize = 2_097_152;


        public string Upload(IFormFile file, string folderName)
        {
            var extention = Path.GetExtension(file.Name); // "ahmed.jpg" ==> .jpg

            if (!_allowedExtentions.Contains(extention))
                return null;

            if (file.Length > _allowedMaxSize)
                return null;




            //var folderPath = $"{Directory.GetCurrentDirectory}\\wwwroot\\files, { folderName}" ;

            var folderPath=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\files", folderName);


            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);  

            var fileName = $"{Guid.NewGuid()}{extention}"; // Must be Unique

            var filePath = Path.Combine(folderPath, fileName);  // File Location Places 

            // Streaming : Data Per Time 

            using var fileStream = File.Create(filePath);
            //using var fileStream = File.Create(filePath);

            file.CopyTo(fileStream);
            return fileName;


        }
        public bool Delete(string filePath)
        {
            if (File.Exists(filePath))
            { 
                File.Delete(filePath);
                return true;
            }
            return false;
        }

    }
}
