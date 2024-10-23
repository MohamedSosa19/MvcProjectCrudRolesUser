using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.PL.Helper
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            //1.Get FolderPath
            string folderPath=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\files", folderName);

            //2.Get FileName And make it Uniqe
            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            //3.Get FilePath
            string filePath=Path.Combine(folderPath,fileName);

            //4.Save in Stream 
            var fs=new FileStream(filePath,FileMode.Create);

            file.CopyTo(fs);

            return fileName;
        }
        
        public static void DeleteFile(string fileName,string folderName)
        {

            if(fileName is not null && folderName is not null)
            {
                string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName, fileName);
                if (File.Exists(filepath))
                    File.Delete(filepath);
            }
            
        }
    }
}
