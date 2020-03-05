using SanityArchiver.Application.Models;
using System.IO;
using System.Linq;
using System.Windows;
using Path = System.IO.Path;

namespace SanityArchiver.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for RenameModal.xaml
    /// </summary>
    public partial class RenameModal : Window
    {
        public RenameModal()
        {
            InitializeComponent();
        }

        private void SaveRename(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textField.Text))
            {
                try
                {
                    MainWindow mainWindow = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                    CustomFile selectedFile = mainWindow.ctrChildView.MyDataGrid.SelectedItem as CustomFile;
                    CustomItemWithCollection selectedDirectory = mainWindow.ctrTreeView.trvMenu.SelectedItem as CustomItemWithCollection;
                    bool isExists = false;
                    foreach (var item in selectedDirectory.Items)
                    {
                        if (textField.Text.Equals(Path.GetFileNameWithoutExtension(item.Name))) 
                        {
                            isExists = true;
                            MessageBox.Show("File name already exist", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    if (!isExists) {
                        File.Move(selectedFile.Name, Path.GetDirectoryName(selectedFile.Name) + @"\" + textField.Text + Path.GetExtension(selectedFile.Name));
                        selectedFile.ShortName = textField.Text + Path.GetExtension(selectedFile.Name);
                        selectedFile.Name = Path.GetDirectoryName(selectedFile.Name) + @"\" + textField.Text + Path.GetExtension(selectedFile.Name);
                    }
                    Close();
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show(ex.Message, "File does not exist in current context", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CancelRename(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
