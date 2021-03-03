using System;
using System.IO;

namespace Lab_1
{
  class WorkWithFile
  {
    public void MenuBarFile()
    {
      string path = @"C:\Lab_1File";
      DirectoryInfo dirInfo = new DirectoryInfo(path);
      if (!dirInfo.Exists)
      {
        dirInfo.Create();
      }
      for (; ; )
      {
        Console.WriteLine("Выберете дейстие:\n 1.Создать файл\n 2.Записать в файл строку\n 3.Прочитать файл в консоль\n 4.Удалить файл\n 5.Перейти в основное меню");
        string Choise = Console.ReadLine();
        int i = Convert.ToInt32(Choise);
        switch (i)
        {
          case 1:
            CreateFile(path);
            break;
          case 2:
            WriteFile(path);
            break;
          case 3:
            ReadFile(path);
            break;
          case 4:
            DeleteFile(path);
            break;
          case 5:
            goto FinishFile;
          default:
            Console.WriteLine("Default case");
            break;
        }
      }
    FinishFile:;
    }

    static void CreateFile(string path)
    {
      FileNameAgain:
      Console.WriteLine("Введите имя файла:");
      string FileName = Console.ReadLine();
      FileInfo fileInfFile = new FileInfo($"{path}\\{FileName}.txt");
      if (fileInfFile.Exists)
      {
        Console.WriteLine("Файл с таким именем уже существует");
        goto FileNameAgain;
      }
      using (FileStream fstream = new FileStream($"{path}\\{FileName}.txt", FileMode.OpenOrCreate))
      {

        Console.WriteLine("Файл создан");
      }
    }

    static void WriteFile(string path) {
      Console.WriteLine("Введите название файла:");
      string FileName = Console.ReadLine();
      FileInfo fileInfFile = new FileInfo($"{path}\\{FileName}.txt");
      if (!fileInfFile.Exists)
      {
        Console.WriteLine("Файла с таким именем не существует");
      }
      else{
        Console.WriteLine("Введите строку для записи в файл:");
        string text = Console.ReadLine();
        using (StreamWriter sw = new StreamWriter($"{path}\\{FileName}.txt", false, System.Text.Encoding.Default))
        {
          sw.WriteLine(text);
        }
      }
    }
    static void ReadFile(string path) {
      Console.WriteLine("Введите название файла:");
      string FileName = Console.ReadLine();
      FileInfo fileInf = new FileInfo($"{path}\\{FileName}.txt");
      if (fileInf.Exists)
      {
        using (StreamReader sr = new StreamReader($"{path}\\{FileName}.txt", System.Text.Encoding.Default))
        {
          string line;
          while ((line = sr.ReadLine()) != null)
          {
            Console.WriteLine(line);
          }
        }
      }
      else
      {
        Console.WriteLine("Файл с данным именем не найден");
      }
    }
    static void DeleteFile(string path){
        Console.WriteLine("Введите название файла:");
        string FileName = Console.ReadLine();
        FileInfo fileInf = new FileInfo($"{path}\\{FileName}.txt");
      if (fileInf.Exists)
      {
        using (FileStream fstream = File.OpenRead($"{path}\\{FileName}.txt"))
        {
          Console.WriteLine($"Вы действительно хотите удалить файл {FileName}?:\n 1.Да\n 2.Нет\n");
          string ChekDelete = Console.ReadLine();
          int ChekDel = Convert.ToInt32(ChekDelete);
          if (ChekDel == 1)
          {
            fstream.Close();
            File.Delete($"{path}\\{FileName}.txt");
            Console.WriteLine("Файл удален");
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
