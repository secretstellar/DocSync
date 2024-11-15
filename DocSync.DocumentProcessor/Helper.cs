using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace DocSync.DocumentProcessor
{
    public class Helper
    {
        public IFormFile ConvertByteArrayToIFormFile(byte[] byteArray)
        {
            var stream = new MemoryStream(byteArray);
            var formFile = new FormFile(stream, 0, byteArray.Length, "file", "csvfile")
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/csv"
            };
            return formFile;
        }
                     
    }
}
