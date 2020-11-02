using System;
using System.IO;
using System.Collections.Generic;

namespace lab1
{
    public class UserInteractionManager
    {


        // Properties

        private static UserInteractionManager instance = null;

        public static UserInteractionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserInteractionManager();
                }

                return instance;
            }
        }


        // Methods

        private UserInteractionManager()
        {
        }

        public void ShowUserMenu()
        {
            while (true)
            {
                switch (GetChoice())
                {
                    case 1:
                        GetDrivesInfo();

                        break;
                    case 2:
                        SaveTracks();

                        break;
                    case 3:
                        GetTrack();

                        break;
                    case 4:
                        GetFiles();

                        break;
                    case 5:
                        GetDirectories();

                        break;
                    case 6:
                        CopyFile();

                        break;
                    case 7:
                        CompressFile();

                        break;
                    case 8:
                        DecompressFile();

                        break;
                    case 9:
                        ArchiveFile();

                        break;
                    case 10:
                        DearchiveFile();

                        break;
                    case 11:
                        DeleteFile();

                        break;
                    default:
                        WrongValueEnteredMessage();

                        break;
                }
            }
        }

        private int GetChoice()
        {
            string answer;

            Console.WriteLine("Выберите действие" +
                "\n1)Список дисков" +
                "\n2)Запись в файл" +
                "\n3)Прочитать файл" +
                "\n4)Список файлов" +
                "\n5)Список директорий" +
                "\n6)Копировать файл" +
                "\n7)Сжать файл" +
                "\n8)Разжать файл" +
                "\n9)Архивировать файл" +
                "\n10)Разархивировать файл" +
                "\n11)Удалить файл");

            answer = Console.ReadLine();

            int.TryParse((
                (string.IsNullOrEmpty(answer) || int.Parse(answer) <= 0) ?
                "0" : answer),
                out int choice);

            return choice;
        }

        private int DiskChoice()
        {
            int counter = 1;
            string answer;

            if (FileManager.Instance.drivers != null)
            {
                foreach(DriveInfo drive in FileManager.Instance.drivers)
                {
                    Console.WriteLine($"{counter}) {drive.Name}");

                    counter++;
                }

                answer = Console.ReadLine();

                int.TryParse((
                    (string.IsNullOrEmpty(answer) || int.Parse(answer) <= 0) ?
                    "0" : answer),
                    out int choice);

                return choice;
            }
            else
            {
                return 0;
            }
        }

        private void GetDrivesInfo()
        {
            FileManager.Instance.GetDriveInfo();
        }

        private void SaveTracks()
        {
            string answer;
            int quantity;
            string path = @"";
            string name;
            List<Track> tracks = new List<Track>();
            path += ChooseDrive().ToString();
            path = ChooseDirectory(path);
            name = CreateFileName(".txt");

            while (true)
            {
                Console.WriteLine("Введите количество треков");
                answer = Console.ReadLine();

                int.TryParse((
                      (string.IsNullOrEmpty(answer) || int.Parse(answer) <= 0) ?
                      "0" : answer),
                      out quantity);

                if (quantity > 0)
                {
                    break;
                }
                else
                {
                    WrongValueEnteredMessage();
                }
            }

            for (int i = 0; i < quantity; i++)
            {
                Track track = CreateTrack();
                tracks.Add(track);
            }

            FileManager.Instance.SaveTracks(tracks, path.ToString() + name.ToString());
        }

        private string CreateFileName(string extension)
        {
            string name;

            while (true)
            {
                Console.WriteLine("Введиие название файла");
                name = Console.ReadLine();

                if (!string.IsNullOrEmpty(name))
                {
                    break;
                }
            }

            return "/" + name.ToString() + extension.ToString();
        }

        private Track CreateTrack()
        {
            string answer;

            Track track = new Track();

            Console.WriteLine("\nEnter track name");
            track.Name = Console.ReadLine();

            Console.WriteLine("\nEnter track Author");
            track.Author = Console.ReadLine();

            Console.WriteLine("\nEnter track url");
            track.Link = Console.ReadLine();

            Console.WriteLine("\nEnter track duration");
            answer = Console.ReadLine();

            int.TryParse((
                  (string.IsNullOrEmpty(answer) || int.Parse(answer) <= 0) ?
                  "0" : answer),
                  out int duration);
            track.Duration = duration;

            return track; 
        }

        private string ChooseDrive()
        {
            Console.WriteLine("Выберете диск:");

            return FileManager.Instance.drivers[DiskChoice() - 1].Name;
        }

        private string ChooseDirectory(string path)
        {
            string directoryPath = path;

            while (true)
            {
                switch (ContinueSearchChoice())
                {
                    case 1:
                        break;
                    case 2:
                        return directoryPath;
                }

                Console.WriteLine(directoryPath);
                Console.WriteLine("Выберете директорию");
                string[] directories = FileManager.Instance.GetDirectories(directoryPath);

                if (directories.Length != 0)
                {
                    directoryPath = DirectoryChoice(directories);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Директории отсутствуют");
                    Console.ResetColor();

                    return directoryPath;
                }
            }
        }

        private string DirectoryChoice(string[] directories)
        {
            int counter = 1;
            string answer;
            int choice;

            foreach (string directory in directories)
            {
                Console.WriteLine($"{counter}) {directory}");

                counter++;
            }


            while (true)
            {
                answer = Console.ReadLine();

                int.TryParse((
                     (string.IsNullOrEmpty(answer) || int.Parse(answer) <= 0) ?
                     "0" : answer),
                     out choice);

                if (choice >= 0 && choice <= directories.Length)
                {
                    break;
                }
                else
                {
                    WrongValueEnteredMessage();

                    continue;
                }
            }

            return directories[choice - 1];
        }

        private int ContinueSearchChoice()
        {
            string answer;

            while (true)
            {
                Console.WriteLine("1)Искать дальше\n2)Остаться");
                answer = Console.ReadLine();

                int.TryParse((
                    (string.IsNullOrEmpty(answer) || int.Parse(answer) <= 0) ?
                    "0" : answer),
                    out int choice);

                switch (choice)
                {
                    case 1:
                        return 1;
                    case 2:
                        return 2;
                    default:
                        WrongValueEnteredMessage();

                        continue;
                }
            }
        }

        private void GetTrack()
        {
            string path = @"" + GetDirectories().ToString();
            string[] files = FileManager.Instance.GetFiles(path);

            if (files.Length != 0)
            {
                Console.WriteLine("Выберете файл для копирования");

                FileManager.Instance.GetTrack(FileChoice(files));
            }
            else
            {
                FilesNotFoundMessage();
            }
        }

        private (string[] files, string path) GetFiles()
        {
            string path = @"" + GetDirectories().ToString();
            string[] files = FileManager.Instance.GetFiles(path);

            Console.WriteLine(new string('-', 30));
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(path);
            Console.ForegroundColor = ConsoleColor.Yellow;

            if (files.Length != 0)
            {
                foreach (string file in files)
                {
                    Console.WriteLine($"|---{file}");
                }
            }
            else
            {
                FilesNotFoundMessage();
            }

            Console.ResetColor();
            Console.WriteLine();

            return (files, path);
        }

        private string GetDirectories()
        {
            string path = @"";

            path += ChooseDrive().ToString();
            path = ChooseDirectory(path);

            return path;
        }

        private void DeleteFile()
        {
            string path = @"" + GetDirectories().ToString();
            string[] files = FileManager.Instance.GetFiles(path);

            if (files.Length != 0)
            {
                Console.WriteLine("Выберете файл для удаления");

                FileManager.Instance.DeleteFile(FileChoice(files));
            }
            else
            {
                FilesNotFoundMessage();
            }
        }

        private string FileChoice(string[] files)
        {
            int counter = 1;
            string answer;
            int choice;

            foreach (string file in files)
            {
                Console.WriteLine($"{counter}) {file}");

                counter++;
            }


            while (true)
            {
                answer = Console.ReadLine();

                int.TryParse((
                     (string.IsNullOrEmpty(answer) || int.Parse(answer) <= 0) ?
                     "0" : answer),
                     out choice);

                if (choice >= 0 && choice <= files.Length)
                {
                    break;
                }
                else
                {
                    WrongValueEnteredMessage();

                    continue;
                }
            }

            return files[choice - 1];
        }

        private void CopyFile()
        {
            string path = @"" + GetDirectories().ToString();
            string[] files = FileManager.Instance.GetFiles(path);

            if (files.Length != 0)
            {

                Console.WriteLine("Выберете файл для копирования");

                FileManager.Instance.CopyFile(FileChoice(files));
            }
            else
            {
                FilesNotFoundMessage();
            }
        }

        private void CompressFile()
        {
            string path = @"" + GetDirectories().ToString();
            string[] files = FileManager.Instance.GetFiles(path);

            if (files.Length != 0)
            {
                FileManager.Instance.CompressFile(FileChoice(files));
            }
            else
            {
                FilesNotFoundMessage();
            }
        }

        private void DecompressFile()
        {
            string path = @"" + GetDirectories().ToString();
            string[] files = FileManager.Instance.GetFiles(path);

            if (files.Length != 0)
            {
                FileManager.Instance.DecompressFile(FileChoice(files));
            }
            else
            {
                FilesNotFoundMessage();
            }
        }

        private void ArchiveFile()
        {
            string path = @"" + GetDirectories().ToString();
            string[] files = FileManager.Instance.GetFiles(path);

            if (files.Length != 0)
            {
                FileManager.Instance.ArchiveFile(FileChoice(files));
            }
            else
            {
                FilesNotFoundMessage();
            }
        }

        private void DearchiveFile()
        {
            string path = @"" + GetDirectories().ToString();
            string[] files = FileManager.Instance.GetFiles(path);

            if (files.Length != 0)
            {
                FileManager.Instance.DearchiveFile(FileChoice(files));
            }
            else
            {
                FilesNotFoundMessage();
            }
        }


        private void CreateDirectory()
        {
            string path = @"" + GetDirectories().ToString();

            FileManager.Instance.CreateDirectory(path);
        }

        private void CreateFile()
        {
            string path = @"" + GetDirectories().ToString();

            FileManager.Instance.CreateFile(path);
        }

        private void FilesNotFoundMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Файлы не найдены");
            Console.ResetColor();
        }

        private void WrongValueEnteredMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Неверный ввод. Попробуйте снова");
            Console.ResetColor();
        }
    }
}
