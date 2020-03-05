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
        public string type;
        public CustomItemWithCollection Custom { get; set; } = new CustomItemWithCollection();

        public RenameModal RenameModal { get; set; }

        public ChildView()
        {
            RenameModal = new RenameModal();
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
                CustomDirectory customDirectory = mainWindow.ctrTreeView.trvMenu.SelectedItem as CustomDirectory;
                bool isExist = false;
                foreach (var file in customDirectory.Items)
                {
                    if (Path.GetFileNameWithoutExtension(file.Name).Equals("New file")) isExist = true;
                }

                if (!isExist)
                {
                    string newFilePath = $"{customDirectory.Name}" + @"\New file.txt";
                    File.CreateText(newFilePath);
                    CustomItem item = new CustomItem { ShortName = @"New file.txt", DateCreated = DateTime.Now, Type = Path.GetExtension(newFilePath), Size = "0" };
                    MyDataGrid.Items.Add(item);
                    customDirectory.Items.Add(item);
                }
                else
                {
                    MessageBox.Show("File name already exist", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                isExist = false;

            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Can not add files to drivers", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RenameFile()
        {
            RenameModal.Show();
        }

        public void Copy_item(object sender, RoutedEventArgs e)
        {
            CustomItem currentSelection = (CustomItem)MyDataGrid.SelectedItem;
            if (!currentSelection.Type.Equals("File folder"))
            {
                CustomFile selectedFile = (CustomFile)MyDataGrid.SelectedItem;
                selected = selectedFile.Name;
                type = "file";
            }
            else
            {
                CustomDirectory selectedDirectory = (CustomDirectory)MyDataGrid.SelectedItem;
                selected = selectedDirectory.Name;
                type = "folder";
            }
            
        }

        private void Delete_item(object sender, RoutedEventArgs e)
        {
            CustomItem currentSelection = (CustomItem)MyDataGrid.SelectedItem;
            if (File.Exists(currentSelection.Name))
            {
                try
                {
                    File.Delete(currentSelection.Name);
                }
                catch (IOException)
                {
                }
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
