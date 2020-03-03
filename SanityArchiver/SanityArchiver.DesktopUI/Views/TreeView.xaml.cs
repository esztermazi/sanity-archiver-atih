﻿using SanityArchiver.Application.Models;
using SanityArchiver.DesktopUI.ViewModels;
using System.Windows.Navigation;
using System.Windows;
using System.Windows.Controls;

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
            CustomDriver driver = (CustomDriver)source.Header;
            foreach (var dir in driver.Items)
            {
                CustomItemController = new CustomItemController() {CustomDirectory = dir};
                CustomItemController.GetCustomDirectories(dir.Name);
            }

        }
    }
}
