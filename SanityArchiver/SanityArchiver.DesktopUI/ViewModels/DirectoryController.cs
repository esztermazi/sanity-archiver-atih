using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using SanityArchiver.Application.Models;

namespace SanityArchiver.DesktopUI.ViewModels
{
    public class DirectoryController
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

                //foreach (var file in dirinfo.getfiles())
                //{
                //    try
                //    {
                //        var item = new customfile
                //        {
                //            name = file.name,
                //            datemodified = file.lastwritetime,
                //            type = file.gettype().tostring(),
                //            size = 0,
                //        };

                //        items.add(item);
                //    }
                //    catch (system.unauthorizedaccessexception)
                //    {
                //        system.console.writeline("got exception");
                //    }
                //}
            }
            return items;
        }
    }
}


