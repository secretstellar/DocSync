using AutoMapper;
using CsvHelper;
using DocSync.API.DTOs;
using DocSync.API.Infrastructure;
using DocSync.API.Models;
using DocSync.API.Repositories.Interfaces;
using DocSync.API.Services.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace DocSync.API.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly MessageQueueConfiguration _configuration;
        private readonly MessageQueueSettings _msgQueueSettings;
        private readonly IMapper _mapper;
        private readonly DocumentSettings _documentSettings;
        private readonly IDocumentRepository _repository;


        public DocumentService(MessageQueueConfiguration configuration
            , IOptions<MessageQueueSettings> msgQueueSettings
            , IMapper mapper
            , IOptions<DocumentSettings> documentSettings
            , IDocumentRepository repository)
        {
            _configuration = configuration;
            _msgQueueSettings = msgQueueSettings.Value;
            _mapper = mapper;
            _documentSettings = documentSettings.Value;
            _repository = repository;
        }


        public async Task<ResponseDto> UploadDocument(DocumentDto documentDto)
        {
            if (documentDto == null)
                throw new ArgumentNullException("Document object must not be empty.");

            if (documentDto.Document == null || documentDto.Document.Length == 0)
                throw new ArgumentNullException("Document content cannot be empty.");

            if (documentDto.DocumentId < 1)
                throw new ArgumentNullException("Invalid document Id.");

            using (var memoryStream = new MemoryStream())
            {
                await documentDto.Document.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();

                var document = new DocumentModel
                {
                    DocumentId = documentDto.DocumentId,
                    Document = fileBytes
                };

                using (var connection = _configuration.CreateConnection())
                using (var channel = _configuration.CreateChannel(connection))
                {
                    var jsonMessage = JsonSerializer.Serialize(document);
                    var body = Encoding.UTF8.GetBytes(jsonMessage);

                    channel.BasicPublish(exchange: "", routingKey: _msgQueueSettings.QueueName, basicProperties: null, body: body);
                }
                return new ResponseDto { IsSuccess = true, Message = "Document uploaded and message sent to RabbitMQ." };
            }
        }

        public async Task<ResponseDto> UploadCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentNullException("No file uploaded.");

            using (var stream = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecordsAsync<PeopleCsvDto>();
                await BatchInsertDataAsync(records);
            }
            return new ResponseDto { IsSuccess = true };
        }

        private async Task BatchInsertDataAsync(IAsyncEnumerable<PeopleCsvDto> recordsDto)
        {
            var batchSize = _documentSettings.BatchSize;
            var records = _mapper.Map<IAsyncEnumerable<PeopleCsv>>(recordsDto);

            var batch = new List<PeopleCsv>();
            await foreach (var record in records)
            {
                batch.Add(record);

                if (batch.Count >= batchSize)
                {
                    await _repository.BulkCreateAsync(batch);
                    batch.Clear();
                }
            }
            if (batch.Count > 0)
                await _repository.BulkCreateAsync(batch);
        }
    }
}

