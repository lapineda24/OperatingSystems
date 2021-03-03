using System;
using System.IO;

namespace Lab_1
{
  class Information
  {
      public void Info()
      {
        DriveInfo[] drives = DriveInfo.GetDrives();

        foreach (DriveInfo drive in drives)
        {
          Console.WriteLine($"Название: {drive.Name}");
          Console.WriteLine($"Тип: {drive.DriveType}");
          if (drive.IsReady)
          {
            Console.WriteLine($"Объем диска: {drive.TotalSize}");
            Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
            Console.WriteLine($"Метка: {drive.VolumeLabel}");
          }
          Console.WriteLine();
        }
      }
    }
}
