using SanityArchiver.Application.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanityArchiver.DesktopUI.ViewModels
{
    class TreeViewProvider
    {
            public ObservableCollection<CustomItem> GetItems()
            {
                var items = new ObservableCollection<CustomItem>();

                foreach (var driver in DriveInfo.GetDrives())
                {
                    try
                    {
                    var item = new CustomDriver
                    {
                        Name = driver.Name
                        };
                        items.Add(item);
                    }
                    catch (System.UnauthorizedAccessException)
                    {
                        System.Console.WriteLine("Got Exception");
                    }
                }
                return items;
            }
        }
}
