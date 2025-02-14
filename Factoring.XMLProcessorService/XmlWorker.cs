using Factoring.Infrastructure;
using Factoring.XMLProcessorService.Configuration;
using Microsoft.Extensions.Options;

namespace Factoring.XMLProcessorService
{
    public class XmlWorker : BackgroundService
    {
        private readonly ILogger<XmlWorker> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IOptions<XmlProcessingConfig> _xmlProcessingConfig;
        private readonly IServiceProvider _serviceProvider;

        public XmlWorker(
            ILogger<XmlWorker> logger,
            ILoggerFactory loggerFactory,
            IOptions<XmlProcessingConfig> xmlProcessingConfig,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _loggerFactory = loggerFactory;
            _xmlProcessingConfig = xmlProcessingConfig;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<FactoringDbContext>();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var inputPath = _xmlProcessingConfig.Value.InputPath;
                    var files = Directory.GetFiles(inputPath, "*.xml");

                    var xmlProcessor = new XmlProcessor(_loggerFactory, context);

                    await Parallel.ForEachAsync(files, stoppingToken, async (file, cancellationToken) =>
                    {
                        _logger.LogInformation($"Processing {file}");
                        await xmlProcessor.ProcessXmlFile(file);
                        ArchiveProcessedFile(file);
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing XML files");
                }

                await Task.Delay(5000, stoppingToken);
            }
        }

        private void ArchiveProcessedFile(string originalPath)
        {
            var archivePath = _xmlProcessingConfig.Value.ArchivePath;
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(originalPath);
            var extension = Path.GetExtension(originalPath);

            var newFileName = $"{fileNameWithoutExtension}_{timestamp}{extension}";
            var newFilePath = Path.Combine(archivePath, newFileName);

            File.Move(originalPath, newFilePath);
        }
    }
}
