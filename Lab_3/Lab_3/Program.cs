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
    }

    public static void TakeElements()
    {
      while(CheckBottom == true || number.Count > 0 )
      {
        CheckConsumer();
      }
    }

    public static void CheckManufacturer()
    {
      if(number.Count > 101)
      {
        Console.WriteLine("Производители засыпают. Количество элементов в очереди: " + Convert.ToString(number.Count));
        Thread.Sleep(10);
      }
      if ( number.Count < 80 )
      {
        Console.WriteLine("Производители просыпаются. Количество элементов в очереди: " + Convert.ToString(number.Count));
        number.Enqueue(rnd.Next(1, 100));
      }
    }

    public static void CheckConsumer()
    {

      if(number.Count == 0)
      {
        Console.WriteLine("Потребители засыпают. Количество элементов в очереди: " + Convert.ToString(number.Count));
        Thread.Sleep(10);
      }
      if( number.Count > 0 && number.Count < 3)
      {
        Console.WriteLine("Потребители просыпаются. Количество элементов в очереди: " + Convert.ToString(number.Count));
        try
        {
          number.Dequeue();
        }
        catch (InvalidOperationException) { }
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
