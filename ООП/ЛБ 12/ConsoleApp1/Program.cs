using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;

class YMLLog
{
    private string logfile;

    public YMLLog(string logfile = "ymllogfile.txt")
    {
        this.logfile = logfile;
    }

    public void WriteLog(string action, string details)
    {
        using (StreamWriter writer = new StreamWriter(logfile, true))
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            writer.WriteLine($"{timestamp} - {action}: {details}");
        }
    }

    public IEnumerable<string> ReadLog()
    {
        if (File.Exists(logfile))
        {
            return File.ReadAllLines(logfile);
        }
        return Enumerable.Empty<string>();
    }


    public IEnumerable<string> SearchLog(string query)
    {
        return ReadLog().Where(log => log.Contains(query));
    }

    public int CountLogEntries()
    {
        return ReadLog().Count();
    }

    public void DeleteOldEntries()
    {
        var currentHour = DateTime.Now.Hour;
        var currentDay = DateTime.Now.Date;
        var newLogEntries = ReadLog().Where(log =>
        {
            var logTime = DateTime.Parse(log.Split(' ')[0] + " " + log.Split(' ')[1]);
            return logTime.Date == currentDay && logTime.Hour == currentHour;
        });

        File.WriteAllLines(logfile, newLogEntries);
    }

    public IEnumerable<string> SearchDate(DateTime date)
    {
        return ReadLog().Where(log =>
       {
        var logTime = DateTime.Parse(log.Split(' ')[0] + " " + log.Split(' ')[1]);
        return logTime.Date == date.Date;
        });
    }
}

class YMLDiskInfo
{
    public List<DiskInfo> GetDiskInfo()
    {
        List<DiskInfo> disks = new List<DiskInfo>();
        foreach (DriveInfo drive in DriveInfo.GetDrives())
        {
            if (drive.IsReady)
            {
                disks.Add(new DiskInfo
                {
                    Name = drive.Name,
                    Total = drive.TotalSize,
                    Free = drive.AvailableFreeSpace,
                    Used = drive.TotalSize - drive.AvailableFreeSpace,
                    Label = drive.VolumeLabel
                });
            }
        }
        return disks;
    }
}

class DiskInfo
{
    public string Name { get; set; }
    public long Total { get; set; }
    public long Free { get; set; }
    public long Used { get; set; }
    public string Label { get; set; }

    public override string ToString()
    {
        return $"Имя: {Name}, Размер: {Total}, свободно: {Free}, исполльзутся: {Used}, тип: {Label}";
    }
}

class YMLFileInfo
{
    private string filepath;

    public YMLFileInfo(string filepath)
    {
        this.filepath = filepath;
    }

    public FileInfo GetFileInfo()
    {
        if (File.Exists(filepath))
        {
            return new FileInfo(filepath);
        }
        throw new FileNotFoundException("не найден файл", filepath);
    }
}

class YMLDirInfo
{
    private string dirpath;

    public YMLDirInfo(string dirpath)
    {
        this.dirpath = dirpath;
    }

    public DirectoryInfo GetDirInfo()
    {
        if (Directory.Exists(dirpath))
        {
            return new DirectoryInfo(dirpath);
        }
        throw new DirectoryNotFoundException("не найдн директория");
    }
}

class YMLFileManager
{
    private YMLLog log;

    public YMLFileManager(YMLLog log)
    {
        this.log = log;
    }

    public void CreateDirectory(string dirName)
    {
        Directory.CreateDirectory(dirName);
        log.WriteLog("Создаем директорию", dirName);
    }

    public void CreateFile(string fileName)
    {
        File.WriteAllText(fileName, "это новый файл.");
        log.WriteLog("создаем файл", fileName);
    }

    public void CopyFile(string source, string destination)
    {
        File.Copy(source, destination);
        log.WriteLog("Копируем файл", $"От {source} в {destination}");
    }

    public void RenameFile(string oldName, string newName)
    {
        File.Move(oldName, newName);
        log.WriteLog("переменовываем файл", $"как было {oldName} и стало {newName}");
    }

    public void DeleteFile(string fileName)
    {
        File.Delete(fileName);
        log.WriteLog("удаляем файл", fileName);
    }

    public void SaveDirInfoToFile(string dirName, string outputFile)
    {
        var dirInfo = new YMLDirInfo(dirName).GetDirInfo();
        var info = $"Количество файлов: {dirInfo.GetFiles().Length}, Время создания: {dirInfo.CreationTime}, колич директорий: {dirInfo.GetDirectories().Length}";
        File.WriteAllText(outputFile, info);
        log.WriteLog("сохранить информацию о директории", outputFile);
    }

    public void CreateFilePath(string directory, string fileName)
    {
        string fullPath = Path.Combine(directory, fileName);
        File.WriteAllText(fullPath, "это новый файл.");
        log.WriteLog("создаем файл", fullPath);
    }
}

class Program
{
    static void Main()
    {
        YMLLog log = new YMLLog();
        YMLDiskInfo diskInfo = new YMLDiskInfo();

        Console.WriteLine("информация диска:");
        foreach (var disk in diskInfo.GetDiskInfo())
        {
            Console.WriteLine(disk);
        }

        var fileManager = new YMLFileManager(log);
        fileManager.CreateDirectory("YMLInspect");
        fileManager.CreateFile("YMLInspect/sample.txt");
        fileManager.CopyFile("YMLInspect/sample.txt", "YMLInspect/sample_copy.txt");
        fileManager.RenameFile("YMLInspect/sample_copy.txt", "YMLInspect/sample_renamed.txt");
        fileManager.DeleteFile("YMLInspect/sample.txt");
        fileManager.SaveDirInfoToFile("YMLInspect", "xxxdirinfo.txt");

        fileManager.CreateFilePath("YMLInspect", "sample_path.txt");

        Console.WriteLine("вывод журнала:");
        foreach (var logEntry in log.ReadLog())
        {
            Console.WriteLine(logEntry);
        }

        Console.WriteLine($"Количество записей в журнале: {log.CountLogEntries()}");

        log.DeleteOldEntries();

        DateTime seaerchday = DateTime.Now.Date;
        Console.WriteLine($"ПОИСК ПО ДАТЕ{seaerchday.ToShortDateString()}:");
        foreach (var logEntry in log.SearchDate(seaerchday)) { Console.WriteLine(logEntry); }

        Console.WriteLine("Журнал после удаления старых записей:");
        foreach (var logEntry in log.ReadLog())
        {
            Console.WriteLine(logEntry);
        }
    }
}
