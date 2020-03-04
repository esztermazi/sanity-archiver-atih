using SanityArchiver.Application.Models;
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
        public CustomDirectory CustomDirectory { get; set; }

        public ChildView()
        {
            CustomDirectory = new CustomDirectory();
            InitializeComponent();
        }

        private void Set_ContextMenuContent(object sender, RoutedEventArgs e)
        {
            if (MyDataGrid.SelectedItem == null)MyMenuItem.Header = "New Text File";
            else
            {
                MyMenuItem.Header = "Rename";
            }
        }

        private void Excetute_ContextMenuCommand(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.OriginalSource as MenuItem;
            if (menuItem.Header.Equals("New Text File")) AddFile();
            else {
                RenameFile();
            }
        }

        private void AddFile()
        {
            MainWindow mainWindow = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            CustomDirectory customDirectory = mainWindow.ctrTreeView.trvMenu.SelectedItem as CustomDirectory;
            string newFilePath = $"{customDirectory.Name}" + @"\New file.txt";
            File.CreateText(newFilePath);
            CustomItem item = new CustomItem { ShortName = @"New file.txt", DateModified = DateTime.Now, Type = Path.GetExtension(newFilePath) };
            MyDataGrid.Items.Add(item);
        }

        private void RenameFile()
        {
            MyDataGrid.SelectedItem = new TextBlock();
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
