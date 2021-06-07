using System;
using System.Collections.Generic;
using System.IO;

namespace Lab_5
{
  class Program
  {
    public static int MemorySize;
    public static bool CheckSegment;

    
    public struct segment
    {
      public int AddressInMemory;
      public string ProgramName;
      public string name;
      public int SizeOfMemory;
    }

    static List<segment> Segments;
    static void Main(string[] args)
    {
      Segments = new List<segment>();
      CheckSegment = true;
      MemorySize = 65536;
      int Choise = 0;
      for (; ; ) {
        Console.WriteLine("1.Ввести данные из файла\n2.Вывести данные\n3.Удалить задачу из памяти\n4.Выход");
        TryAgainInput:
        try
        {
          Choise = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException) 
        {
          Console.WriteLine("Вы ввели символ. Попробуйте снова");
          goto TryAgainInput;
        }
        switch (Choise)
        {
          case 1:
            ReadFile();
            break;
          case 2:
            PrintInformation();
            break;
          case 3:
            DeleteSegment();
            break;
          case 4:
            goto Finish;
          default:
            Console.Write("Неверно введенный номер команды");
            break;

        }
      }
    Finish:;
    }

    public static void ReadFile() //создание таблицы сегментов
    {
      int CheckString = 0;
      string programName = "";
      string Name = "";
      string Size = "";
      FileInfo fileInf = new FileInfo(@"D:\Information.txt");
      if (fileInf.Exists)
      {
        using (StreamReader sr = new StreamReader(@"D:\Information.txt", System.Text.Encoding.Default))
        {
          string line;
          while ((line = sr.ReadLine()) != null)
          {
            for(int i = 0; i < line.Length; i++)
            {
              if (line[i] == ' ') CheckString ++;
              if (CheckString == 0) programName += line[i];
              if (CheckString == 1) Name += line[i];
              if (CheckString == 2) Size += line[i];
            }
            MemorySize -= Convert.ToInt32(Size);
            Check(Name);
            if (MemorySize >= 0 && CheckSegment)
            {
              AddSegments(programName, Name, Convert.ToInt32(Size));
            }
            else
            {
              if(MemorySize <= 0)
              { 
                Console.WriteLine("Память заполнена. Больше задач выполнятся не может");
                goto End; 
              }
            }
            programName = "";
            Name = "";
            Size = "";
            CheckString = 0;
            CheckSegment = true;
          }
        }
      }
      else
      {
        Console.WriteLine("Файл с данным именем не найден");
      }
      End:;
    }
    
    public static void Check(string Name)// проверка одинаковых сегментов из разных процессов
    {
      for(int i = 0; i < Segments.Count; i++)
      {
        if (Segments[i].name == Name) CheckSegment = false; 
      }
    }

    public static void AddSegments(string programName, string Name, int Size)
    {
      bool Create = false;
      for(int i = 0; i <  Segments.Count; i++)
      {
        if(Segments[i].ProgramName == null && Segments[i - 1].ProgramName != programName)
        {
          var Copy = Segments[i];
          Copy.ProgramName = programName;
          Copy.name = Name;
          Copy.SizeOfMemory = Size;
          Copy.AddressInMemory = i;
          Segments[i] = Copy;
          Create = true;
          break;
        }     
      }
      if(Segments.Count == 0)
      {
        Segments.Add(new segment { ProgramName = programName, name = Name, SizeOfMemory = Convert.ToInt32(Size) });
        Create = true;
      }
      if(Create == false)
      {
        if(Segments[Segments.Count - 1].ProgramName == programName) Segments.Add(new segment { ProgramName = null, name = null, SizeOfMemory = 0 });
        Segments.Add(new segment { ProgramName = programName, name = Name, SizeOfMemory = Convert.ToInt32(Size), AddressInMemory = Segments.Count });
      }
    }

    public static void PrintInformation()
    {
      for (int i = 0; i < Segments.Count; i++)
      {
        if (Segments[i].ProgramName == null) Console.WriteLine("Пустой сегмент");
        else Console.WriteLine("Имя программы: " + Segments[i].ProgramName + " Имя задачи: " + Segments[i].name + " Выделяемая память: " + Convert.ToString(Segments[i].SizeOfMemory) + "байт ");
      }
    }

    public static void DeleteSegment()
    {
      bool DeleteSeg = false;
      if (Segments.Count != 0)
      {
        int k = 0;
        Console.WriteLine("Введите адрес сегмента задачи, которую следует вывести из памяти");
      TryAgain:
        try
        {
          k = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException)
        {
          Console.WriteLine("Вы ввели неккоректный адрес");
          goto TryAgain;
        }
        for (int i = 0; i < Segments.Count; i++)
        {
          if (Segments[i].AddressInMemory == k)
          {
            MemorySize += Segments[i].SizeOfMemory;
            Segments.RemoveAt(i);
            DeleteSeg = true;
            Console.WriteLine("Задача удалена из памяти");
            break;
          }
        }
        if (DeleteSeg == false) Console.WriteLine("Сегмент с данным адресом не найден");
      }
      else Console.WriteLine("Память пуста");
    }
    
  }
}
