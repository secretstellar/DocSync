// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client.Events;
using DocSync.DocumentProcessor;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http.Headers;

Console.WriteLine("*********** DocSync Document Processor **********");

Console.WriteLine("Please enter user name");
var user = Console.ReadLine();

Console.WriteLine("\n");

Console.WriteLine("DocSync Document Processing Started...");

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "admin",
    Password = "admin123$",
    VirtualHost = "/"
};

var conn = factory.CreateConnection();
using var channel = conn.CreateModel();
channel.QueueDeclare(queue: "docsync", durable: true, exclusive: false, autoDelete: false, arguments: null);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += async (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();

    var document = JsonSerializer.Deserialize<DocumentModel>(body);

    // POST API for bulk uploading the Csv records will be called here
    // "https://localhost:7287/api/Document/uploadcsv";


    // PUT API for updating the document info status to completed or error will be called here
    // https://localhost:7287/api/DocumentInfo

};
channel.BasicConsume("docsync", true, consumer);

Console.ReadKey();



