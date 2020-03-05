using SanityArchiver.Application.Models;
using System;
using System.Collections.ObjectModel;
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
        public string selected;
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
            if (MyDataGrid.SelectedItem == null)
            {
                MyMenuItem.Header = "New Text File";
                CopyMenuItem.Visibility = Visibility.Collapsed;
                DeleteMenuItem.Visibility = Visibility.Collapsed;
            }
            else
            {
                MyMenuItem.Header = "Rename";
                CopyMenuItem.Header = "Copy";
                CopyMenuItem.Visibility = Visibility.Visible;
                DeleteMenuItem.Header = "Delete";
                DeleteMenuItem.Visibility = Visibility.Visible;
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
                    CustomFile item = new CustomFile { Name = newFilePath, ShortName = @"New file.txt", DateCreated = DateTime.Now, Type = Path.GetExtension(newFilePath), Size = new FileInfo(newFilePath).Length / 512 + " KB" };
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

        private void Copy_item(object sender, RoutedEventArgs e)
        {
            CustomFile selectedFile = (CustomFile)MyDataGrid.SelectedItem;
            selected = selectedFile.Name;
        }

        private void Delete_item(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            CustomItem currentSelection = (CustomItem)MyDataGrid.SelectedItem;
            if (File.Exists(currentSelection.Name))
                {
                    try
                    {
                        File.Delete(currentSelection.Name);
                        Custom.Items.Remove(currentSelection);
                        //mainWindow.ctrTreeView.trvMenu.SelectedItem. Remove(currentSelection);
                    }
                    catch (IOException) { }
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
