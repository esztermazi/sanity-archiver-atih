﻿using SanityArchiver.Application.Models;
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
                        DateModified = directory.LastWriteTime,
                        Type = directory.GetType().ToString(),
                        Size = 0,
                    };

                    CustomDirectory.Items.Add((CustomItem)dir);
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
                        DateModified = customFile.LastWriteTime,
                        Type = customFile.GetType().ToString(),
                        Size = 0,
                    };

                    CustomDirectory.Items.Add((CustomItem)file);
                }

            }
            catch (System.UnauthorizedAccessException)
            {
                System.Console.WriteLine("Got Exception");
            }
        }
    }
}


