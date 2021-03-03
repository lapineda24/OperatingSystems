using System;
using System.IO;
using System.IO.Compression;

namespace Lab_1
{
  class WorkWithZip
  {
    public void MenuBarZip()
    {
      for (; ; )
      {
        Console.WriteLine("Выберете дейстие(все файлы формата txt берутся из каталога Lab_1File):\n 1.Создать архив в форматер zip\n 2.Добавить файл в архив\n 3.Разархивировать файл и вывести данные о нем\n 4.Удалить файл и архив\n 5.Перейти в основное меню");
        string Choise = Console.ReadLine();
        int i = Convert.ToInt32(Choise);
        switch (i)
        {
          case 1:
            CreateZip();
            break;
          case 2:
            AddToTheZip();
            break;
          case 3:
            UnZip();
            break;
          case 4:
            DeleteZip();
            break;
          case 5:
            goto FinishZip;
          default:
            Console.WriteLine("Default case");
            break;
        }
      }
    FinishZip:;
    }
    static void CreateZip()
    {
    TryAgain:
      Console.WriteLine("Введите имя нового архива");
      string ZipName = Console.ReadLine();
      string ZipPath = $".//{ZipName}.zip";
      string FilePath = @"C:\Lab_1FileZip";
      FileInfo fileInf = new FileInfo(ZipPath);
      if (fileInf.Exists)
      {
        Console.WriteLine("Архив с таким именем уже существует");
        goto TryAgain;
      }
      else
      {
        DirectoryInfo dirInfo = new DirectoryInfo(FilePath);
        if (!dirInfo.Exists)
        {
          dirInfo.Create();
        }
        ZipFile.CreateFromDirectory(FilePath, ZipPath);
        dirInfo.Delete();
        Console.WriteLine("Архив создан");
      }
    }
    static void AddToTheZip()
    {
    FileNameAgain:
      Console.WriteLine("Введите имя файла");
      string FileName = Console.ReadLine();
      string FilePath = $"C://Lab_1File//{FileName}.txt";
      FileInfo fileInfFile = new FileInfo(FilePath);
      if (!fileInfFile.Exists)
      {
        Console.WriteLine("Файл с таким именем не существует");
        goto FileNameAgain;
      }
    ZipNameAgain:
      Console.WriteLine("Введите имя архива");
      string ZipName = Console.ReadLine();
      string ZipPath = $".//{ZipName}.zip";
      FileInfo fileInfZip = new FileInfo(ZipPath);
      if (!fileInfZip.Exists)
      {
        Console.WriteLine("Архив с таким именем не найден");
        goto ZipNameAgain;
      }
      using (ZipArchive archive = ZipFile.Open(ZipPath, ZipArchiveMode.Update))
      {
        archive.CreateEntryFromFile($"C://Lab_1File//{FileName}.txt", $"{FileName}.txt");
      }
    }
    static void UnZip()
    {
    ZipNameAgain1:
      Console.WriteLine("Введите имя архива");
      string ZipName = Console.ReadLine();
      string ZipPath = $".//{ZipName}.zip";
      FileInfo fileInfZip = new FileInfo(ZipPath);
      if (!fileInfZip.Exists)
      {
        Console.WriteLine("Архив с таким именем не найден");
        goto ZipNameAgain1;
      }
      string FilePath = @"C:\Lab_1FileZip";
      DirectoryInfo dirInfo = new DirectoryInfo(FilePath);
      if (!dirInfo.Exists)
      {
        dirInfo.Create();
      }
      FileInfo fileInf = new FileInfo(ZipPath);
      if (fileInf.Exists)
      {
        using (ZipArchive archive = ZipFile.OpenRead(ZipPath))
        {
          foreach (ZipArchiveEntry entry in archive.Entries)
          {
            try
            {
              entry.ExtractToFile(Path.Combine(FilePath, entry.FullName));
            }
            catch (IOException e) when ((e.HResult & 0x0000FFFF) == 80)
            {
              Console.WriteLine($"Файл с именем {entry.FullName} уже существует");
            }
          }
        }
        var Files = Directory.GetFiles(FilePath);
        foreach (var value in Files)
        {
          Console.WriteLine(value);
        }
      }
      else
      {
        Console.WriteLine("Файл с данным именем не найден");
      }
    }
    static void DeleteZip()
    {
      Console.WriteLine("Введите имя файла");
      string ZipName = Console.ReadLine();
      string ZipPath = $".//{ZipName}.zip";
      FileInfo fileInf = new FileInfo(ZipPath);
      if (fileInf.Exists)
      {
        Console.WriteLine($"Вы действительно хотите удалить файл?\n 1.Да\n 2.Нет\n");
        string ChekDelete = Console.ReadLine();
        int ChekDel = Convert.ToInt32(ChekDelete);
        if (ChekDel == 1)
        {
          using (FileStream fs = new FileStream(ZipPath, FileMode.OpenOrCreate))
          {
            fs.Close();
            File.Delete(ZipPath);
            Console.WriteLine("The file was deleted");
          }
        }
      }
      else
      {
        Console.WriteLine("Файл с данным именем не найден");
      }
    }
  }
}
