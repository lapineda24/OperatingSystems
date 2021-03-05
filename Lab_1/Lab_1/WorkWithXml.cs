using System;
using System.IO;
using System.Xml;

namespace Lab_1
{
  class WorkWithXml
  {
    class User
    {
      public string Name { get; set; }
      public int Age { get; set; }
      public string Company { get; set; }
    }
    public void MenuBarXml()
    {
      string path = @"C:\Users\lapin\source\repos\Lab_1\Lab_1\users.xml";
      for (; ; )
      {
        Console.WriteLine("Выберете дейстие:\n 1.Записать в файл пользователя\n 2.Прочитать файл в консоль\n 3.Удалить файл\n 4.Перейти в основное меню");
        string Choise = Console.ReadLine();
        int i = Convert.ToInt32(Choise);
        switch (i)
        {
          case 1:
            InputUser(path);
            break;
          case 2:
            ReadXml(path);
            break;
          case 3:
            DeleteXml(path);
            break;
          case 4:
            goto FinishXml;
          default:
            Console.WriteLine("Default case");
            break;
        }
      }
    FinishXml:;
    }
    static void InputUser(string path)
    {
      FileInfo fileInfFile = new FileInfo(path);
      if (!fileInfFile.Exists)
      {
        User user1 = new User();
        Console.WriteLine("Введите имя ");
        user1.Name = Console.ReadLine();
        Console.WriteLine("Введите возраст ");
        user1.Age = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Введите название компании ");
        user1.Company = Console.ReadLine();
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(path);
        XmlElement xRoot = xDoc.DocumentElement;
        XmlElement userElem = xDoc.CreateElement("user");
        XmlAttribute nameAttr = xDoc.CreateAttribute("name");
        XmlElement companyElem = xDoc.CreateElement("company");
        XmlElement ageElem = xDoc.CreateElement("age");
        XmlText nameText = xDoc.CreateTextNode(user1.Name);
        XmlText companyText = xDoc.CreateTextNode(user1.Company);
        XmlText ageText = xDoc.CreateTextNode(user1.Age.ToString());
        nameAttr.AppendChild(nameText);
        companyElem.AppendChild(companyText);
        ageElem.AppendChild(ageText);
        userElem.Attributes.Append(nameAttr);
        userElem.AppendChild(companyElem);
        userElem.AppendChild(ageElem);
        xRoot.AppendChild(userElem);
        xDoc.Save(path);
        Console.WriteLine("Запись сохранена");
      }
      else
      {
        Console.WriteLine("Файл с данным именем не найден");
      }
    }
    static void ReadXml(string path)
    {
      FileInfo fileInfFile = new FileInfo(path);
      if (!fileInfFile.Exists)
      {
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(path);
        XmlElement xRoot = xDoc.DocumentElement;
        foreach (XmlNode xnode in xRoot)
        {
          if (xnode.Attributes.Count > 0)
          {
            XmlNode attr = xnode.Attributes.GetNamedItem("name");
            if (attr != null)
              Console.WriteLine(attr.Value);
          }
          foreach (XmlNode childnode in xnode.ChildNodes)
          {
            if (childnode.Name == "company")
            {
              Console.WriteLine($"Компания: {childnode.InnerText}");
            }
            if (childnode.Name == "age")
            {
              Console.WriteLine($"Возраст: {childnode.InnerText}");
            }
          }
          Console.WriteLine();
        }
        Console.Read();
      }
      else
      {
        Console.WriteLine("Файл с данным именем не найден");
      }
    }
    static void DeleteXml(string path)
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
