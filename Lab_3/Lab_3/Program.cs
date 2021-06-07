using System;
using System.Threading;
using System.Collections.Generic;

namespace Lab_3
{
  class Program
  {
    public static Thread Manufacturer1;
    public static Thread Manufacturer2;
    public static Thread Manufacturer3;
    public static Thread Consumer1;
    public static Thread Consumer2;
    public static Queue<int> number;
    public static Random rnd;
    public static bool CheckBottom;

    static void Main(string[] args)
    {
      CheckBottom = true;
      number = new Queue<int>();
      rnd = new Random();
      Manufacturer1 = new Thread(AddElements);
      Manufacturer2 = new Thread(AddElements);
      Manufacturer3 = new Thread(AddElements);
      Manufacturer1.Start();
      Manufacturer2.Start();
      Manufacturer3.Start();
      Consumer1 = new Thread(TakeElements);
      Consumer2 = new Thread(TakeElements);
      Consumer1.Start();
      Consumer2.Start();
      Thread CheckFinish = new Thread(WaitFinish);
      CheckFinish.Start();
    }

    public static void AddElements()
    {
      while(CheckBottom)
      {
          CheckManufacturer();
      }
      if((!Manufacturer1.IsAlive && !Manufacturer2.IsAlive) ||
        (!Manufacturer1.IsAlive && !Manufacturer3.IsAlive) ||
        (!Manufacturer3.IsAlive && !Manufacturer2.IsAlive))
        Console.WriteLine("Производители закончили работу");
    }

    public static void TakeElements()
    {
      while(CheckBottom == true || number.Count > 0 )
      {
        CheckConsumer();
      }
      if(!Consumer1.IsAlive || !Consumer2.IsAlive) Console.WriteLine("Потребители закончили работу");
    }

    public static void CheckManufacturer()
    {
      if (number.Count > 101)
      {
        Console.WriteLine("Производители засыпают. Количество элементов в очереди: " + Convert.ToString(number.Count));
        Thread.Sleep(10);
      }
      else
      {
        number.Enqueue(rnd.Next(1, 100));
        if (number.Count > 80 && number.Count < 84)
        {
          Console.WriteLine("Производители просыпаются. Количество элементов в очереди: " + Convert.ToString(number.Count));

        }
      }
    }

    public static void CheckConsumer()
    {

      if (number.Count == 0)
      {
        Console.WriteLine("Потребители засыпают. Количество элементов в очереди: " + Convert.ToString(number.Count));
        Thread.Sleep(100);
      }
      else
      {
        try
          {
            number.Dequeue();
          }
          catch (InvalidOperationException) { }
        if (number.Count > 0 && number.Count < 3)
        {
          Console.WriteLine("Потребители просыпаются. Количество элементов в очереди: " + Convert.ToString(number.Count));
          
        }
      }
    }

    public static void WaitFinish()
    {
      while (true)
      {
        ConsoleKeyInfo cki = new ConsoleKeyInfo();
        if (Console.KeyAvailable == true)
        {
          cki = Console.ReadKey(true);
          if (cki.Key == ConsoleKey.Q)
          {
            CheckBottom = false;
            break;
          }
        }
      }
    }
  }
}
