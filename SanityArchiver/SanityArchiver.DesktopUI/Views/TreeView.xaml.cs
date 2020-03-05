using SanityArchiver.Application.Models;
using SanityArchiver.DesktopUI.ViewModels;
using System;
using System.Collections.ObjectModel;
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
                CustomDirectory sourceItem = (CustomDirectory)source.Header;
                foreach (CustomDirectory dir in sourceItem.Items)
                {
                    CustomItemController = new CustomItemController() { CustomDirectory = (CustomDirectory)dir };
                    CustomItemController.GetCustomDirectories(dir.Name);
                }
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
                CustomDirectory sourceItem = (CustomDirectory)source.Header;
                CustomItemController = new CustomItemController() { CustomDirectory = sourceItem };
                CustomItemController.GetCustomFiles(sourceItem.Name);
                mainWindow.ctrChildView.Custom.Items.Clear();
                foreach (var item in sourceItem.Items)
                {
                    mainWindow.ctrChildView.Custom.Items.Add(item);
                }
            }
        }
    }
}
