using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocSync.DocumentProcessor
{
    public class DocumentModel
    {
        public int DocumentId { get; set; }
        public byte[] Document { get; set; }
    }
}
