using System;
using System.Runtime;
using System.Text;
using System.Security.Cryptography;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;


namespace Lab_2
{
  class Program
  {
    static public List<string> words;
    static string chek;
    static public int n; 
    static public List<Thread> ThreadsForFind;
    static public List<Thread> ThreadsForArray;
    static char[] alfabet = { 'a','b', 'c' , 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x' , 'y' , 'z'};
    static void Main(string[] args)
    {
      Console.WriteLine("Введите количество потоков до 26 включительно");
      n = Convert.ToInt32(Console.ReadLine());
      Console.WriteLine("1.Выбрать заданное хеш-Значение\n 2.Считать с консоли");
      int choise = Convert.ToInt32(Console.ReadLine());
      switch (choise)
      {
        case 1:
          Console.WriteLine("1.1115dd800feaacefdf481f1f9070374a2a81e27880f187396db67958b207cbad\n" +
                            "2.3a7bd3e2360a3d29eea436fcfb7e44c735d117c42d1c1835420b6b9942dd4f1b\n" +
                            "3.74e1bb62f8dabb8125a58852b63bdf6eaef667cb56ac7f7cdba6d7305c50a22f");
          int SecondChoise = Convert.ToInt32(Console.ReadLine());
          switch (SecondChoise)
          {
            case 1:
              chek = "1115dd800feaacefdf481f1f9070374a2a81e27880f187396db67958b207cbad";
              break;
            case 2:
              chek = "3a7bd3e2360a3d29eea436fcfb7e44c735d117c42d1c1835420b6b9942dd4f1b";
              break;
            case 3:
              chek = "74e1bb62f8dabb8125a58852b63bdf6eaef667cb56ac7f7cdba6d7305c50a22f";
              break;
            default:
              Console.WriteLine("Default case");
              break;
          }
          StartProgram();
          break;
        case 2:
          chek = Console.ReadLine();
          StartProgram();
          break;
        default:
          Console.WriteLine("Default case");
          break;
      }
    }

    public static void StartProgram()
    {
      words = new List<string>();
      ThreadsForFind = new List<Thread>();
      ThreadsForArray = new List<Thread>();
      for (int i = 0; i < 26; i++)
      {
        ThreadsForArray.Add(new Thread(new ParameterizedThreadStart(Create)));
        ThreadsForArray[i].Start(i);
      }
      for (int i = 0; i < n; i++)
      {
        ThreadsForFind.Add(new Thread(new ParameterizedThreadStart(Find)));
        ThreadsForFind[i].Start(i);
      }

    }

    public static void Find(object i)
    {
      Stopwatch stopWatch = new Stopwatch();
      stopWatch.Start();
      string str;
      for (int j = 0; j < 26 * 26 * 26 * 26 * 26 / n; j++)
      {
        try
        {
          str = words[(int)i + n * j];
          Encoding u8 = Encoding.UTF8;
          int k = CountsAndBytes(str, u8);
          if (k == 1) Console.WriteLine(str + " Потраченное время: " + stopWatch.ElapsedMilliseconds);
        }
        catch (ArgumentOutOfRangeException) { goto Finish; }
      }
    Finish:;
    }
    public static void Create(object i)
    {
      
      string str = "";
      Encoding u8 = Encoding.UTF8;
        for (int letter2 = 0; letter2 < 26; letter2++)
        {
          for (int letter3 = 0; letter3 < 26; letter3++)
          {
            for (int letter4 = 0; letter4 < 26; letter4++)
            {
              for (int letter5 = 0; letter5 < 26; letter5++)
              {
                str = String.Concat(alfabet[(int)i], alfabet[letter2], alfabet[letter3], alfabet[letter4], alfabet[letter5]);
                words.Add(str);
                str = "";
              }
            }
        }
      }
    }
    public static int CountsAndBytes(string str, Encoding enc)
    {
      int k = 0;
      if (str != null) { byte[] bytes = enc.GetBytes(str);
        using (SHA256 mySHA = SHA256.Create())
        {
          byte[] hashValue = mySHA.ComputeHash(bytes);
          k = ByteArray(hashValue);
        } 
      }
      return k;
    }

    public static int ByteArray(byte[] array)
    {
      int k = 0;
      string chek1 = BitConverter.ToString(array);
      chek1 = chek1.Replace("-", "");
      string chek2 = chek1.ToLower();
      if (chek2 == chek) k = 1;
      return k;
    }
  }
}
