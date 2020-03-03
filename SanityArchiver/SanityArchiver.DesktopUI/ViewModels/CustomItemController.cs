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


            try
            {
                foreach (var directory in dirinfo.GetDirectories())
                {
                    var dir = new CustomDirectory
                    {
                        Name = directory.FullName,
                        DateModified = directory.LastWriteTime,
                        Type = directory.GetType().ToString(),
                        Size = 0,
                    };

                    CustomDirectory.Items.Add(dir);
                };
            }
            catch (System.UnauthorizedAccessException)
            {
                System.Console.WriteLine("Got Exception");
            }
        }
    }
}


