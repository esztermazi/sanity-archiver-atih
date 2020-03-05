using SanityArchiver.Application.Models;
using SanityArchiver.DesktopUI.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SanityArchiver.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for TreeView.xaml
    /// </summary>
    public partial class TreeView : UserControl
    {

        public DriveController DriveController { get; set; }
        public CustomItemController CustomItemController { get; set; }


        public TreeView()
        {
            DriveController = new DriveController();
            DataContext = DriveController;
            InitializeComponent();
        }

        private void OnExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem source = e.OriginalSource as TreeViewItem;

            try
            {
                CustomDrive sourceItem = (CustomDrive)source.Header;
                foreach (CustomDirectory dir in sourceItem.Items)
                {
                    dir.ShortName = dir.Name.Remove(0, dir.Name.LastIndexOf("\\") + 1);
                    CustomItemController = new CustomItemController() { CustomDirectory = dir };
                    CustomItemController.GetCustomDirectories(dir.Name);
                }
            }
            catch (InvalidCastException)
            {
                try
                {
                    CustomDirectory sourceItem = (CustomDirectory)source.Header;
                    foreach (var dir in sourceItem.Items)
                    {
                        if (dir.Type.Equals("File folder"))
                        {
                            CustomItemController = new CustomItemController() { CustomDirectory = (CustomDirectory)dir };
                            CustomItemController.GetCustomDirectories(dir.Name);
                        }
                    }
                }
                catch (InvalidCastException) { }
            }
        }

        private void OnItemSelected(object sender, RoutedEventArgs e)
        {
            TreeViewItem source = e.OriginalSource as TreeViewItem;
            MainWindow mainWindow = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            try
            {
                CustomDrive sourceItem = (CustomDrive)source.Header;
                mainWindow.ctrChildView.Custom.Items.Clear();
                foreach (var item in sourceItem.Items)
                {
                    mainWindow.ctrChildView.Custom.Items.Add(item);
                }
                

            }
            catch (InvalidCastException)
            {
                try
                {
                    CustomDirectory sourceItem = (CustomDirectory)source.Header;
                    CustomItemController = new CustomItemController() { CustomDirectory = sourceItem };
                    CustomItemController.GetCustomFiles(sourceItem.Name);
                    mainWindow.ctrChildView.Custom.Items.Clear();
                    foreach (var item in sourceItem.Items)
                    {
                        mainWindow.ctrChildView.Custom.Items.Add(item);
                    }
                }
                catch (InvalidCastException) { }
            }
        }

        private void trvMenu_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TreeViewMenuItem.Header = "Paste";
        }

        private void PasteIntoDir(object sender, RoutedEventArgs e)
        {
            CustomDirectory selectedDirectory = (CustomDirectory)trvMenu.SelectedItem;
            MainWindow mainWindow = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            string selectedFile = mainWindow.ctrChildView.selected;
            string selectedFileName = Path.GetFileName(selectedFile);
            string newPath = Path.Combine(selectedDirectory.Name, selectedFileName);
            File.Copy(selectedFile, newPath);
            mainWindow.ctrChildView.Custom.Items.Add(new CustomFile() 
            { 
                Name = selectedFile,
                ShortName = selectedFileName,
                Size = new FileInfo(selectedFile).Length / 512 + " KB",
                Type = Path.GetExtension(selectedFile),
                DateCreated = new FileInfo(selectedFile).CreationTime,
            });
        }

        private void CopySubFolders(string sourceDirName, string destDirName)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }
            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destDirName, subdir.Name);
                CopySubFolders(subdir.FullName, temppath);
            }
        }
    }
}
