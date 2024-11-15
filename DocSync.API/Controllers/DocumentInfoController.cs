using DocSync.API.DTOs;
using DocSync.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocSync.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentInfoController : ControllerBase
    {
        private readonly IDocumentInfoService _documentInfoService;

        public DocumentInfoController(IDocumentInfoService documentInfoService)
        {
            _documentInfoService = documentInfoService;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetAllDocumentsAsync()
        {
            var response = await _documentInfoService.GetAllDocumentInfoAsync();
            return Ok(response);
        }

        [HttpGet("{id}"), Authorize]
        public async Task<IActionResult> GetDocumentInfoByIdAsync(int id)
        {
            var response = await _documentInfoService.GetDocumentInfoByIdAsync(id);
            return Ok(response);
        }

        [HttpPost,Authorize]
        public async Task<IActionResult> AddDocumentInfoAsync([FromBody] DocumentInfoDto docInfo)
        {
            var response = await _documentInfoService.AddDocumentInfoAsync(docInfo);
            return Ok(response);
        }

        [HttpPut, Authorize]
        public async Task<IActionResult> UpdateDocument([FromBody] DocumentInfoDto docInfo)
        {
            var response = await _documentInfoService.UpdateDocumentInfoAsync(docInfo);
            return Ok(response);
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteDocumentInfoAsync(int id)
        {
            var response = await _documentInfoService.DeleteDocumentInfoAsync(id);
            return Ok(response);
        }
    }
}
