using SanityArchiver.Application.Models;
using System.IO;

namespace SanityArchiver.DesktopUI.ViewModels
{
    public class CustomItemController
    {
        public CustomDirectory CustomDirectory { get; set; }

        public void GetCustomDirectories(string path)
        {
            var dirinfo = new DirectoryInfo(path);

            try
            {
                foreach (var directory in dirinfo.GetDirectories())
                {
                    var dir = new CustomDirectory
                    {
                        Name = directory.FullName,
                        ShortName = directory.FullName.Remove(0, directory.FullName.LastIndexOf('\\') + 1),
                        DateCreated = directory.CreationTime,
                        Type = "File folder",
                        Size = "",
                    };

                    CustomDirectory.Items.Add(dir);
                }

            }
            catch (System.UnauthorizedAccessException)
            {
                System.Console.WriteLine("Got Exception");
            }
        }

        public void GetCustomFiles(string path)
        {
            var dirinfo = new DirectoryInfo(path);

            try
            {
                foreach (var customFile in dirinfo.GetFiles())
                {
                    var file = new CustomFile
                    {
                        Name = customFile.FullName,
                        ShortName = customFile.FullName.Remove(0, customFile.FullName.LastIndexOf("\\") + 1),
                        DateCreated = customFile.LastWriteTime,
                        Type = Path.GetExtension(customFile.FullName),
                        Size = customFile.Length / 512 + " KB",
                    };

                    CustomDirectory.Items.Add(file);
                }

            }
            catch (System.UnauthorizedAccessException)
            {
                System.Console.WriteLine("Got Exception");
            }
        }
    }
}


