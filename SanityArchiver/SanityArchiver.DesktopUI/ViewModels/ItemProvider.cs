using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using SanityArchiver.Application.Models;

namespace SanityArchiver.DesktopUI.ViewModels
{
    public class ItemProvider
    {
        public ObservableCollection<CustomItem> GetItems(string path)
        {
            var items = new ObservableCollection<CustomItem>();

            var dirInfo = new DirectoryInfo(path);

            foreach (var directory in dirInfo.GetDirectories())
            {
                try
                {
                    var item = new CustomDirectory
                    {
                        Name = directory.Name,
                        DateModified = directory.LastWriteTime,
                        Type = directory.GetType().ToString(),
                        Size = 0,
                    };
                    items.Add(item);
                }
                catch (System.UnauthorizedAccessException)
                {
                    System.Console.WriteLine("Got Exception");
                }

                foreach (var file in dirInfo.GetFiles())
                {
                    try
                    {
                        var item = new CustomFile
                        {
                            Name = file.Name,
                            DateModified = file.LastWriteTime,
                            Type = file.GetType().ToString(),
                            Size = 0,
                        };

                        items.Add(item);
                    }
                    catch (System.UnauthorizedAccessException)
                    {
                        System.Console.WriteLine("Got Exception");
                    }
                }
            }
            return items;
        }
    }
}


