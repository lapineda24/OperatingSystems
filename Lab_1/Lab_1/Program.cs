using System;
using System.Threading.Tasks;

namespace Lab_1
{
  class Program
  {
    public static Information inf;
    public static WorkWithFile wf;
    public static WorkWithJson wj;
    public static WorkWithXml wx;
    public static WorkWithZip wz;
    static async Task Main(string[] args)
    {
      for (; ; )
      {
        Console.WriteLine("Выберете дейстие:\n 1.Вывести информацию в консоль о логических дисках, именах, метке тома, размере типе файловой системы.\n 2.Работа с файлами\n 3.Работа с форматом JSON\n 4.Работа с форматом XML\n 5.Работа с форатом zip\n 6.Выход");
        string Choise = Console.ReadLine();
        int i = Convert.ToInt32(Choise);
        switch (i)
        {
          case 1:
            inf = new Information();
            inf.Info();
            break;
          case 2:
            wf = new WorkWithFile();
            wf.MenuBarFile();
            break;
          case 3:
            wj = new WorkWithJson();
            await wj.MenuBarJson();
            break;
          case 4:
            wx = new WorkWithXml();
            wx.MenuBarXml();
            break;
          case 5:
            wz = new WorkWithZip();
            wz.MenuBarZip();
            break;
          case 6:
            goto Finish;
          default:
            Console.WriteLine("Default case");
            break;
        }
      }
    Finish:;
    }
    
  }
}