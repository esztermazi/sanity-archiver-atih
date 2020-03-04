﻿using SanityArchiver.Application.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanityArchiver.DesktopUI.ViewModels
{
    public class DriverController
    {

        public ObservableCollection<CustomDriver> Drivers {get; set; }

        public DriverController()
        {
            GetItems();
        }

        public void GetItems()
            {
            Drivers = new ObservableCollection<CustomDriver>();
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
                        Drivers.Add(item);
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
