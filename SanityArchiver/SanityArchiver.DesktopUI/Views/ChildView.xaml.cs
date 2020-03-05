﻿using SanityArchiver.Application.Models;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SanityArchiver.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for ChildView.xaml
    /// </summary>
    public partial class ChildView : UserControl
    {
        public bool RenameModalStatus { get; set; } = false; 

        public CustomItemWithCollection Custom { get; set; } = new CustomItemWithCollection();

        public static RenameModal RenameModal { get; set; }



        public ChildView()
        {
            DataContext = Custom;
            InitializeComponent();
        }

        private void Set_ContextMenuContent(object sender, RoutedEventArgs e)
        {
            if (MyDataGrid.SelectedItem == null) MyMenuItem.Header = "New Text File";
            else
            {
                MyMenuItem.Header = "Rename";
            }
        }

        private void Excetute_ContextMenuCommand(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.OriginalSource as MenuItem;
            if (menuItem.Header.Equals("New Text File")) AddFile();
            else
            {
                RenameFile();
            }
        }

        private void AddFile()
        {
            try
            {
                MainWindow mainWindow = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                CustomItemWithCollection sourceItem = Custom;
                CustomDirectory customDirectory = mainWindow.ctrTreeView.trvMenu.SelectedItem as CustomDirectory;
                bool isExist = false;
                foreach (var file in Custom.Items)
                {
                    if (Path.GetFileNameWithoutExtension(file.Name).Equals("New file")) isExist = true;
                }

                if (!isExist)
                {
                    string newFilePath = $"{customDirectory.Name}" + @"\New file.txt";
                    File.CreateText(newFilePath);
                    CustomItem item = new CustomItem { ShortName = @"New file.txt", DateCreated = DateTime.Now, Type = Path.GetExtension(newFilePath), Size = "0" };
                    Custom.Items.Add(item);
                }
                else
                {
                    MessageBox.Show("File name already exist", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                isExist = false;

            }
            catch (NullReferenceException)
            {
                MessageBox.Show("File name already exist", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RenameFile()
        {
            if (!RenameModalStatus)
            {
                RenameModal = new RenameModal();
                RenameModal.Show();
                RenameModalStatus = true;
            }
        }

        private void EscapePressed(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Escape))
            {
                MyDataGrid.SelectedItems.Clear();
            }
        }
    }
}
