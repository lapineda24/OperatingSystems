using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab_1
{
  class Book
  {
    public string Title { get; set; }
    public string Author { get; set; }
    public int Price { get; set; }
  }
  class WorkWithJson
  {
    public async Task MenuBarJson()
    {
      string path = @"C:\Users\lapin\source\repos\Lab_1\Lab_1\user.json";
      for (; ; )
      {
        Console.WriteLine("Выберете дейстие:\n 1.Записать в файл строку\n 2.Прочитать файл в консоль\n 3.Удалить файл\n 4.Перейти в основное меню");
        string Choise = Console.ReadLine();
        int i = Convert.ToInt32(Choise);
        switch (i)
        {
          case 1:
            await СonservationJson(path);
            break;
          case 2:
            await ReadJson(path);
            break;
          case 3:
            DeleteJson(path);
            break;
          case 4:
            goto FinishJson;
          default:
            Console.WriteLine("Default case");
            break;
        }
      }
      FinishJson:;
    }
    static async Task СonservationJson(string path)
    {
      FileInfo fileInfFile = new FileInfo(path);
      if (!fileInfFile.Exists)
      {
        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
        {
          Book tom = new Book() { Title = "Тень ветра", Author = "Карлос Руис Сафон", Price = 75 };
          await JsonSerializer.SerializeAsync<Book>(fs, tom);
          Console.WriteLine("Запись сохранена в файле");
        }
      }
      else
      {
        Console.WriteLine("Файл с данным именем не найден");
      }
    }
    static async Task ReadJson(string path)
    {
      FileInfo fileInfFile = new FileInfo(path);
      if (!fileInfFile.Exists)
      {
        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
        {
          Book restoredPerson = await JsonSerializer.DeserializeAsync<Book>(fs);
          Console.WriteLine($"Название: {restoredPerson.Title}  Автор: {restoredPerson.Author} Цена: {restoredPerson.Price} ");
        }
      }
      else
      {
        Console.WriteLine("Файл с данным именем не найден");
      }
    }
    static void DeleteJson(string path)
    {
      FileInfo fileInf = new FileInfo(path);
      if (fileInf.Exists)
      {
        Console.WriteLine($"Вы действительно хотите удалить файл?\n 1.Да\n 2.Нет\n");
        string ChekDelete = Console.ReadLine();
        int ChekDel = Convert.ToInt32(ChekDelete);
        if (ChekDel == 1)
        {
          using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
          {
            fs.Close();
            File.Delete(path);
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
