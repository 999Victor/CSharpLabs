using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WindowsService
{
    public class FileService: IFileService
    {


        // Properties

        private readonly ILogger<FileService> logger;
        private readonly string targetDirectory;


        // Methods

        public FileService(ILogger<FileService> _logger, IOptions<AppSettings> settings)
        {
            logger = _logger;
            targetDirectory = settings.Value.TargetDirectory;
        }

        public void Run(string sourceDirectory)
        {
            logger.LogInformation("In Service A");
            logger.LogInformation(targetDirectory);

            FileManager.Instance.CompressFile(sourceDirectory, targetDirectory);
        }
    }
}
