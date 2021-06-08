using System;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;



namespace Lab_4
{
  class Program
  {
    public static Thread Thread1;
    public static Thread Thread2;
    public static Thread Thread3;
    public static Stopwatch stopWatch;
    public static bool CheckBottom;
    static object locker = new object();
    static char[] alfabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
    static void Main(string[] args)
    {
      CheckBottom = true;
      stopWatch = new Stopwatch();
      stopWatch.Start();
      Thread1 = new Thread(FindWords);
      Thread2 = new Thread(FindPrimeNumbers);
      Thread3 = new Thread(FindSum);
      Thread Check = new Thread(Wait);
      Check.Start();
      Thread1.Start();
      Thread2.Start();
      Thread3.Start();
      
    }

    public static void FindWords()
    {
        string str;
        lock (locker)
        {
          for (int letter1 = 0; letter1 < 26; letter1++)
          { 
              for (int letter2 = 0; letter2 < 26; letter2++)
              {
                for (int letter3 = 0; letter3 < 26; letter3++)
                {
                
                     if (stopWatch.ElapsedMilliseconds < 5000) // проверка времени
              {
                        if (CheckBottom) // проверка была ли нажата Q
                {
                          str = String.Concat(alfabet[letter1], alfabet[letter2], alfabet[letter3]);
                          Console.WriteLine(str);
                          str = "";
                        }
                        else { Thread.Sleep(100); }
                     }
                     else
                     {
                        Console.WriteLine("Приостановка работы потока по поиску слов. Время, затраченное на работу: " + Convert.ToString(stopWatch.ElapsedMilliseconds));
                          stopWatch.Restart(); // обнуление счетчика для другого потока
                          Monitor.Pulse(locker); // блокировка текущего потока
                          Monitor.Wait(locker); //режим  ожидание, пока другой поток не закончит работу или не перейдет в режим ожидания
              }
                }
              }
          }
          Console.WriteLine("Конец поиска слов из 3 букв");
          Monitor.Pulse(locker);
        }
    }

    public static void FindPrimeNumbers()
    {

      lock (locker) 
      {
        bool check = true;
        for (int i = 1; i < 9000; i++)
        {

          for (int j = 1; j < i - 1; j++)
          {
            if (stopWatch.ElapsedMilliseconds < 5000) // проверка времени
            {
              if (CheckBottom) // проверка была ли нажата Q
              {
                if (i % j == 0 && j != 1) check = false;
                
              }
              else { Thread.Sleep(1000); }
            }
            else 
            {
                Console.WriteLine("Приостановка работы потока по поиску простых чисел. Время, затраченное на работу: " + Convert.ToString(stopWatch.ElapsedMilliseconds));
                stopWatch.Restart(); // обнуление счетчика для другого потока
                Monitor.Pulse(locker); // блокировка текущего потока
                Monitor.Wait(locker); //режим  ожидание, пока другой поток не закончит работу или не перейдет в режим ожидания
              
            }    
          }
          if (check) Console.WriteLine(Convert.ToString(i));
          check = true;
        }
        Console.WriteLine("Конец поиска простых чисел");
        Monitor.Pulse(locker);
      }
    }

    public static void FindSum()
    {
      lock (locker)
      {
        int sum = 0;
        if (stopWatch.ElapsedMilliseconds < 5000) // проверка времени
        {
          
          for (int i = 0; i < 1000; i++)
          {
            if (stopWatch.ElapsedMilliseconds < 5000) 
            {
              if (CheckBottom) // проверка была ли нажата Q
              {
                sum += (i * 3) * (i + 1) - (i * i);
                Console.WriteLine(Convert.ToString(sum));
              }
              else { Thread.Sleep(1000); }
            }
            else
            {
              Console.WriteLine("Приостановка работы потока по поиску суммы ряда. Время, затраченное на работу: " + Convert.ToString(stopWatch.ElapsedMilliseconds));
              stopWatch.Restart(); // обнуление счетчика для другого потока
              Monitor.Pulse(locker); // блокировка текущего потока
              Monitor.Wait(locker); //режим  ожидание, пока другой поток не закончит работу или не перейдет в режим ожидания
            }
          }
        }
        Console.WriteLine("Конец поиска суммы ряда");
        Monitor.Pulse(locker); 
      }
    }

    public static void Wait()
    {
      while(Thread1.IsAlive || Thread2.IsAlive || Thread3.IsAlive) // работает пока работают три основных потока 
      {
        ConsoleKeyInfo cki = new ConsoleKeyInfo();
        if (Console.KeyAvailable == true)
        {
          cki = Console.ReadKey(true);
          if (cki.Key == ConsoleKey.Q)
          {
            if (CheckBottom == true) { CheckBottom = false; }
            else { CheckBottom = true; }
          }
        }
      }
    }
  }
}
