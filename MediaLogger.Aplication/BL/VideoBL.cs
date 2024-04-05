using MediaLogger.Domain.DTOs;
using MediaLogger.Domain.Entities.Business;
using MediaLogger.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Aplication.BL
{
    public class VideoBL : IVideoBL
    {
        public async Task<byte[]> DownloadVideo(string IdTrx)
        {
            string BaseFolder = $@"C:\VideosPaypad\";
            string[] videoFiles = Directory.GetFiles(BaseFolder, $"*{IdTrx}*");
            if (videoFiles.Length == 0) throw new Exception("No se encontraron coincidencias");
            return await File.ReadAllBytesAsync(videoFiles[0]);
            
        }

        public async Task UploadVideo(IFormFile videoFile)
        {
            string fileName = await FileName(videoFile);
            await UpLoadFile(videoFile, fileName);
        }

        private async Task<string> FileName(IFormFile videoFile)
        {
            string Folder = $@"C:\VideosPaypad\";

            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);

            string fileName = Path.Combine(Folder, videoFile.FileName);

            return await Task.FromResult(fileName);
        }
    
        private async Task UpLoadFile(IFormFile videoFile, string fileName) 
        {
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                await  videoFile.CopyToAsync(stream);
            }
        }

      
    }
}
