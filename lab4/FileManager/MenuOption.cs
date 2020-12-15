using ConfigurationManager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryMonitorService
{
    class MenuOption
    {
        public FileSystemWatcher fileSystemWatcher;
        public PathConfig pathConfig { get; set; }
        public LogConfig logConfig { get; set; }
        public ArchiveConfig archieveConfig { get; set; }
        public CipherConfig cipherConfig { get; set; }

        private string fileText;

        private void SetConfigs()
        {
            try
            {
                var appPath = AppDomain.CurrentDomain.BaseDirectory;
                var manager = new ConfigManager();

                pathConfig = manager.GetConfig<PathConfig>(appPath + "config.xml");
                logConfig = manager.GetConfig<LogConfig>(appPath + "appSettings.json");
                archieveConfig = manager.GetConfig<ArchiveConfig>(appPath + "config.xml");
                cipherConfig = manager.GetConfig<CipherConfig>(appPath + "appSettings.json");

                ValidatePaths(pathConfig);
                ValidateLogs(logConfig);
            }
            catch (Exception ex)
            {
                using (var writer = new StreamWriter(@"E:\TargetDirectory\log.txt", true))
                {
                    writer.WriteLine(ex.Message);
                }
            }
        }

        public void Start()
        {
            SetConfigs();
            Watcher();
        }

        public void Watcher()
        {
            fileSystemWatcher = new FileSystemWatcher(pathConfig.Source)
            {
                EnableRaisingEvents = true,
                IncludeSubdirectories = true
            };

            fileSystemWatcher.Created += Changes;
            fileSystemWatcher.Deleted += Changes;
            fileSystemWatcher.Renamed += Rename;
            fileSystemWatcher.Changed += Changes;
        }

        private void Rename(object sender, RenamedEventArgs e)
        {
            Encryption(e.FullPath);
            ArchivateFile(e.FullPath);

            var mess = $"{e.ChangeType} - {e.FullPath} - {DateTime.Now}{Environment.NewLine}";
            File.AppendAllText(logConfig.Log, mess);
        }

        private void Changes(object sender, FileSystemEventArgs e)
        {
            var mess = $"{e.ChangeType} - {e.FullPath} - {DateTime.Now}{Environment.NewLine}";
            File.AppendAllText(logConfig.Log, mess);
        }

        private void ArchivateFile(string path)
        {
            try
            {
                var compressFile = Path.ChangeExtension(path, archieveConfig.Extension); // E:\SourceDirectory\text.rar

                archieveConfig.Compress(path, compressFile);
                File.Delete(path);

                var targetPath = DateDirectory() + Path.GetFileName(compressFile);
                File.Copy(compressFile, targetPath);
                archieveConfig.Decompress(targetPath, Path.ChangeExtension(targetPath, ".txt"));
                Decryption(Path.ChangeExtension(targetPath, ".txt"));
                File.Delete(targetPath);
            }
            catch (Exception ex)
            {
                using (var writer = new StreamWriter(logConfig.Log, true))
                {
                    writer.WriteLine(ex.Message);
                }
            }
        }

        private string DateDirectory()
        {
            DateTime date = DateTime.Now;
            string filepath = pathConfig.Target + "\\archive " + date.ToString("D");

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            return filepath + "\\";
        }

        private void Encryption(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                fileText = reader.ReadToEnd();
            }

            using (var writer = new StreamWriter(filePath))
            {
                fileText = cipherConfig.Encrypt(fileText, cipherConfig.Key);
                writer.WriteLine(fileText);
            }
        }

        private void Decryption(string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                fileText = cipherConfig.Decrypt(fileText, cipherConfig.Key);
                writer.WriteLine(fileText);
            }
        }

        private void ValidatePaths(PathConfig path)
        {
            var writer = new StreamWriter(logConfig.Log, true);
            var results = new List<ValidationResult>();
            var context = new ValidationContext(path);

            if (!Validator.TryValidateObject(path, context, results, true))
            {
                foreach (var error in results)
                {
                    writer.WriteLine(error.ErrorMessage);

                }
            }

            writer.Close();
        }

        private void ValidateLogs(LogConfig path)
        {
            var writer = new StreamWriter(@"E:\TargetDirectory\log.txt", true);
            var results = new List<ValidationResult>();
            var context = new ValidationContext(path);

            if (!Validator.TryValidateObject(path, context, results, true))
            {
                foreach (var error in results)
                {
                    writer.WriteLine(error.ErrorMessage);
                }
            }

            writer.Close();
        }
    }
}