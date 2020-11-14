using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace WindowsService
{
    public class Worker : BackgroundService
    {


        // Properties

        private readonly string sourceDirectory;
        private readonly ILogger<Worker> logger;
        private FileSystemWatcher folderWatcher;
        private readonly IServiceProvider services;


        // Methods

        public Worker(ILogger<Worker> _logger, IOptions<AppSettings> settings, IServiceProvider _services)
        {
            logger = _logger;
            services = _services;
            sourceDirectory = settings.Value.SourceDirectory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.CompletedTask;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Сервис запущен");

            if (!Directory.Exists(sourceDirectory))
            {
                logger.LogWarning($"указанноко пути [{sourceDirectory}] не существует.");

                return Task.CompletedTask;
            }

            logger.LogInformation($"Считывание событий из файла: {sourceDirectory}");

            folderWatcher = new FileSystemWatcher(sourceDirectory, "*.TXT")
            {
                NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite | NotifyFilters.FileName |
                                  NotifyFilters.DirectoryName
            };
            folderWatcher.Created += Input_OnChanged;
            folderWatcher.EnableRaisingEvents = true;

            return base.StartAsync(cancellationToken);
        }

        protected void Input_OnChanged(object source, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                logger.LogInformation($"Создан новый файл [{e.FullPath}]");

                using (var scope = services.CreateScope())
                {
                    var fileService = scope.ServiceProvider.GetRequiredService<IFileService>();
                    fileService.Run(sourceDirectory: e.FullPath);
                }
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Сервис остановлен");
            folderWatcher.EnableRaisingEvents = false;

            await base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            logger.LogInformation("Сервис отключен");
            folderWatcher.Dispose();

            base.Dispose();
        }
    }
}
