using AutoMapper;
using DocSync.API.DTOs;
using DocSync.API.Models;
using DocSync.API.Repositories.Interfaces;
using DocSync.API.Services.Interfaces;

namespace DocSync.API.Services
{
    public class DocumentInfoService : IDocumentInfoService
    {
        private readonly IDocumentInfoRepository _repository;
        private readonly IMapper _mapper;

        public DocumentInfoService(IDocumentInfoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseDto> GetAllDocumentInfoAsync()
        {
            var docInfoList = await _repository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<DocumentInfoDto>>(docInfoList);
            return new ResponseDto
            {
                Data = result,
                IsSuccess = true,
                Message = "Document Info list retrieved successfully"
            };
        }

        public async Task<ResponseDto> GetDocumentInfoByIdAsync(int id)
        {
            if (id < 1)
                throw new ArgumentNullException("id is invalid");

            var docInfo = await _repository.GetByIdAsync(id);
            if (docInfo == null)
                return new ResponseDto { IsSuccess = false, Message = "Document Info not found" };

            var documentInfoDto = _mapper.Map<DocumentInfoDto>(docInfo);
            return new ResponseDto
            {
                Data = documentInfoDto,
                IsSuccess = true,
                Message = "Document Info retrieved successfully"
            };
        }

        public async Task<ResponseDto> AddDocumentInfoAsync(DocumentInfoDto docInfoDto)
        {
            if (docInfoDto == null)
                throw new ArgumentNullException("Input object must not be empty");

            if (String.IsNullOrEmpty(docInfoDto?.DocumentName))
                throw new ArgumentNullException("Document name is required");

            if (docInfoDto?.StatusId < 1)
                throw new ArgumentNullException("Status Id invalid");

            if (String.IsNullOrEmpty(docInfoDto?.CreatedBy))
                throw new ArgumentNullException("Created by is required");


            var docInfo = _mapper.Map<DocumentInfo>(docInfoDto);
            docInfo.CreatedDate = DateTime.UtcNow;

            var result = await _repository.CreateAsync(docInfo);
            return new ResponseDto
            {
                Data = result,
                IsSuccess = result > 0,
                Message = result > 0 ? "Document Info added successfully" : "Error adding Document Info"
            };
        }

        public async Task<ResponseDto> UpdateDocumentInfoAsync(DocumentInfoDto docInfoDto)
        {
            if (docInfoDto == null)
                throw new ArgumentNullException("Input object must not be empty");

            if (String.IsNullOrEmpty(docInfoDto?.UpdatedBy))
                throw new ArgumentNullException("Updated by is required");

            var docInfo = _mapper.Map<DocumentInfo>(docInfoDto);
            docInfo.UpdatedDate = DateTime.UtcNow;

            var result = await _repository.UpdateAsync(docInfo);
            return new ResponseDto
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Document Info updated successfully" : "Error updating Document Info"
            };
        }

        public async Task<ResponseDto> DeleteDocumentInfoAsync(int id)
        {
            if (id < 1)
                throw new ArgumentNullException("id is invalid");

            var result = await _repository.DeleteAsync(id);
            return new ResponseDto
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Document Info deleted successfully" : "Error deleting Document Info"
            };
        }
    }
}
