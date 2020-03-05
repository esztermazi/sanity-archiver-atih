using SanityArchiver.Application.Models;
using System.Collections.ObjectModel;
using System.IO;

namespace SanityArchiver.DesktopUI.ViewModels
{
    public class DriveController
    {

        public ObservableCollection<CustomDriver> Items { get; set; }

        public DriveController()
        {
            Items = new ObservableCollection<CustomDriver>();
            GetItems();
        }

        public void GetItems()
        {
            Items.Clear();
            foreach (var driver in DriveInfo.GetDrives())
            {
                try
                {
                    var item = new CustomDriver
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

        private void GetDirectories(CustomDriver item)
        {
            try
            {
                foreach (var dir in Directory.GetDirectories(item.Name))
                {
                    item.Items.Add(new CustomDirectory { Name = dir });
                }

            }
            catch (IOException)
            {
                System.Console.WriteLine("Got Exception");
            }
        }
    }
}
