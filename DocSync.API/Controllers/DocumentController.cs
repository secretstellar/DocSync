using DocSync.API.DTOs;
using DocSync.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocSync.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }


        [HttpPost("upload"),Authorize]
        public async Task<IActionResult> Upload([FromForm] DocumentDto documentDto)
        {
            var response = await _documentService.UploadDocument(documentDto);
            return Ok(response);
        }

        [HttpPost("uploadcsv")]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            var response = await _documentService.UploadCsv(file);
            return Ok(response);
        }
    }
}
