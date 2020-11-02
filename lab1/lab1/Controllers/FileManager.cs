using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;

namespace lab1
{
    public class FileManager
    {


        // Properties

        private static FileManager instance = null;
        private List<Track> tracksList = new List<Track>();
        public DriveInfo[] drivers = DriveInfo.GetDrives();

        public static FileManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FileManager();
                }
                
                return instance;
            }
        }


        // Methods

        private FileManager()
        {
        }

        public void GetTrack(string path)
        {
            try
            {
                using(FileStream fileStream = File.OpenRead(path))
                {
                    byte[] byteArray;
                    //fileStream.Seek();
                }
            }
            catch (FileNotFoundException e)
            {
                FileNotFoundMessage(e);
            }
        }

        public void SaveTracks(List<Track> tracks, string path)
        {
            int counter = 1;
            string textDocument = "";

            foreach(Track track in tracks)
            {
                textDocument += $"{counter}) Name: {track.Name} Author: {track.Author} Duration: {track.Duration} Link: {track.Link}\n";
                counter++;
            }

            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    byte[] byteArray = System.Text.Encoding.Default.GetBytes(textDocument);
                    fileStream.Write(byteArray, 0, byteArray.Length);
                    Console.WriteLine($"Файл успешно записан в {path}");
                }
            }
            catch (PathTooLongException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Слишком длинный путь");
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
            catch (UnauthorizedAccessException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Отказано в доступе");
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
        }

        public void GetDriveInfo()
        {
            Console.WriteLine("Мой компьютер:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string('-', 30));

            if (drivers != null) {
                foreach (DriveInfo drive in drivers)
                {
                    Console.WriteLine("|" + new string('-', 3) + new string($"Диск: '{drive.Name}'"));
                    Console.WriteLine("|" + new string('-', 3) + $"Тип: {drive.DriveType}");

                    if (drive.IsReady)
                    {
                        Console.WriteLine("|" + new string('-', 3) + $"Всего места: {drive.TotalSize}");
                        Console.WriteLine("|" + new string('-', 3) + $"Свободного места: {drive.AvailableFreeSpace}\n");
                    }
                    else
                    {
                        Console.WriteLine("|" + new string('-', 3) + $"Диск {drive.Name} не готов к работе\n");
                    }
                }
            } else
            {
                Console.WriteLine("Диски не найдены");
            }

            Console.ResetColor();
        }

        public string[] GetDirectories(string path)
        {
            try
            {
                return Directory.GetDirectories(path);
            }
            catch (DriveNotFoundException e)
            {
                Console.WriteLine("Неверный путь");
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public string[] GetFiles(string path)
        {
            try
            {
                return Directory.GetFiles(path);
            }
            catch (DriveNotFoundException e)
            {
                Console.WriteLine("Неверный путь");
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public void CreateDirectory(string path)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                directoryInfo.CreateSubdirectory(path);
            }
            catch (DriveNotFoundException e)
            {
                DiskNotFoundMessage(e);
            }
            catch (DirectoryNotFoundException e)
            {
                DirectoryeNotFoundMessage(e);
            }
        }

        public void CreateFile(string path)
        {
            try
            {
                File.Create(path);
                Console.WriteLine("Файл успешно создан");
            }
            catch (DriveNotFoundException e)
            {
                DiskNotFoundMessage(e);
            }
            catch (DirectoryNotFoundException e)
            {
                DirectoryeNotFoundMessage(e);
            }
        }

        public void CopyFile(string path)
        {
            try
            {

            }
            catch (FileNotFoundException e)
            {
                FileNotFoundMessage(e);
            }
        }

        public void DeleteFile(string path)
        {
            try
            {
                try
                {
                    File.Delete(path);
                    Console.WriteLine("Файл успешно удален");
                }
                catch (IOException e)
                {
                    Console.WriteLine("Закройте файл перед удалением");
                    Console.WriteLine(e.Message);
                }
            }
            catch (FileNotFoundException e)
            {
                FileNotFoundMessage(e);
            }
        }

        public void CompressFile(string pathToOpen, string pathToSave)
        {
            try
            {
                 using(FileStream sourceStream = new FileStream(pathToOpen, FileMode.OpenOrCreate))
                {
                    using(FileStream targetStream = File.Create(pathToSave))
                    {
                        using(GZipStream decompressStream = new GZipStream(sourceStream, CompressionMode.Compress))
                        {
                            decompressStream.CopyTo(targetStream);
                        }
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                FileNotFoundMessage(e);
            }
        }

        public void DecompressFile(string pathToOpen, string pathToSave)
        {
            try
            {
                using(FileStream sourceStream = new FileStream(pathToOpen, FileMode.OpenOrCreate))
                {
                    using(FileStream targetStream = File.Create(pathToSave))
                    {
                        using(GZipStream decompressStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                        {
                            decompressStream.CopyTo(targetStream);
                        }
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                FileNotFoundMessage(e);
            }
        }

        public void ArchiveFile(string path)
        {
            try
            {

            }
            catch (FileNotFoundException e)
            {
                FileNotFoundMessage(e);
            }
        }

        public void DearchiveFile(string path)
        {
            try
            {

            }
            catch (FileNotFoundException e)
            {
                FileNotFoundMessage(e);
            }
        }

        public void RenameFile(string path)
        {
            try
            {

            }
            catch (FileNotFoundException e)
            {
                FileNotFoundMessage(e);
            }
        }

        private void FileNotFoundMessage(FileNotFoundException e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Файл не найден");
            Console.WriteLine(e.Message);
            Console.ResetColor();
        }

        private void DirectoryeNotFoundMessage(DirectoryNotFoundException e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Директория не найдена");
            Console.WriteLine(e.Message);
            Console.ResetColor();
        }

        private void DiskNotFoundMessage(DriveNotFoundException e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Диск не найден");
            Console.WriteLine(e.Message);
            Console.ResetColor();
        }
    }
}
