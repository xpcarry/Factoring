using Factoring.Infrastructure.Extensions;
using Factoring.XMLProcessorService;
using Factoring.XMLProcessorService.Configuration;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.Configure<XmlProcessingConfig>(builder.Configuration.GetSection("XmlProcessing"));
builder.Services.AddSingleton<XmlProcessingConfig>();
builder.Services.AddHostedService<XmlWorker>();
builder.Services.AddDatabase(builder.Configuration);

var host = builder.Build();
host.Run();
