using SanityArchiver.Application.Models;
using SanityArchiver.DesktopUI.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System;

namespace SanityArchiver.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for TreeView.xaml
    /// </summary>
    public partial class TreeView : UserControl

    {

        public DriverController DriverController { get; set; }
        public CustomItemController CustomItemController { get; set; }



        public TreeView()
        {

            InitializeComponent();
            DriverController = new DriverController();
            foreach (var drive in DriverController.Drivers)
            {
                trvMenu.Items.Add(drive);
            }

        }
        private void OnExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem source = e.OriginalSource as TreeViewItem;

            try
            {
                CustomDriver sourceItem = (CustomDriver)source.Header;
                foreach (var dir in sourceItem.Items)
                {
                    dir.ShortName = dir.Name.Remove(0, dir.Name.LastIndexOf("\\")+1);
                    CustomItemController = new CustomItemController() { CustomDirectory = dir };
                    CustomItemController.GetCustomDirectories(dir.Name);
                }
            }
            catch (InvalidCastException)
            {
                CustomDirectory sourceItem = (CustomDirectory)source.Header;
                foreach (var dir in sourceItem.Items)
                {
                    CustomItemController = new CustomItemController() { CustomDirectory = (CustomDirectory)dir };
                    CustomItemController.GetCustomDirectories(dir.Name);
                }
            }
        }

        private void OnItemSelected(object sender, RoutedEventArgs e)
        {
            TreeViewItem source = e.OriginalSource as TreeViewItem;
            MainWindow mainWondow = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWondow.ctrChildView.MyDataGrid.Items.Clear();
            try
            {
                CustomDriver sourceItem = (CustomDriver)source.Header;
                foreach (var item in sourceItem.Items)
                {
                    mainWondow.ctrChildView.MyDataGrid.Items.Add(item);
                }
            }
            catch (InvalidCastException)
            {
                CustomDirectory sourceItem = (CustomDirectory)source.Header;
                CustomItemController = new CustomItemController() { CustomDirectory = sourceItem };
                CustomItemController.GetCustomFiles(sourceItem.Name);
                foreach (var item in sourceItem.Items)
                {
                    mainWondow.ctrChildView.MyDataGrid.Items.Add(item);
                }
                sourceItem.Items.Clear();
                CustomItemController.GetCustomDirectories(sourceItem.Name);
            }
        }
    }
}
