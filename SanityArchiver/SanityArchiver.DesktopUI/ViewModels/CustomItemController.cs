using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using SanityArchiver.Application.Models;

namespace SanityArchiver.DesktopUI.ViewModels
{
    public class CustomItemController
    {
        public CustomDirectory CustomDirectory { get; set; }

        public void GetCustomDirectories(string path)
        {
            var items = new ObservableCollection<CustomDirectory>();

            var dirinfo = new DirectoryInfo(path);

            foreach (var directory in dirinfo.GetDirectories())
            {
                try
                {
                    var dir = new CustomDirectory
                    {
                        Name = directory.Name,
                        DateModified = directory.LastWriteTime,
                        Type = directory.GetType().ToString(),
                        Size = 0,
                    };

                    CustomDirectory.Items.Add(dir);
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
        }
    }
}


