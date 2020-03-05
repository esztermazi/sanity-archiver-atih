using SanityArchiver.Application.Models;
using System.Collections.ObjectModel;
using System.IO;

namespace SanityArchiver.DesktopUI.ViewModels
{
    public class DriveController
    {

        public ObservableCollection<CustomDrive> Items { get; set; }

        public DriveController()
        {
            Items = new ObservableCollection<CustomDrive>();
            GetItems();
        }

        public void GetItems()
        {
            Items.Clear();
            foreach (var driver in DriveInfo.GetDrives())
            {
                try
                {
                    var item = new CustomDrive
                    {
                        Name = driver.Name,
                        ShortName = driver.Name,
                    };
                    GetDirectories(item);
                    Items.Add(item);
                }
                catch (System.UnauthorizedAccessException)
                {
                    System.Console.WriteLine("Got Exception");
                }
            }
        }

        private void GetDirectories(CustomDrive item)
        {
            try
            {
                foreach (var dir in Directory.GetDirectories(item.Name))
                {
                    var directoryInfo = new DirectoryInfo(dir);
                    item.Items.Add(new CustomDirectory { 
                        Name = dir,
                        DateCreated = directoryInfo.CreationTime,
                        Type = "File folder",
                    });
                }

            }
            catch (IOException)
            {
                System.Console.WriteLine("Got Exception");
            }
        }
    }
}
