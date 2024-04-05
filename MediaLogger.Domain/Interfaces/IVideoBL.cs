using MediaLogger.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Domain.Interfaces
{
    public interface IVideoBL
    {
        public Task  UploadVideo(IFormFile videoFile);
        public Task<byte[]> DownloadVideo(string videoName);
    }
}
