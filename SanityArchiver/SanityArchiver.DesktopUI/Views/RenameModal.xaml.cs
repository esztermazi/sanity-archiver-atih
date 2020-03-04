using SanityArchiver.Application.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
                    File.Move(selectedFile.Name, Path.GetDirectoryName(selectedFile.Name) + @"\"+ textField.Text + Path.GetExtension(selectedFile.Name));
                    selectedFile.ShortName = textField.Text + Path.GetExtension(selectedFile.Name);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "File does not exist in current context", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CancelRename(object sender, RoutedEventArgs e)
        {

        }
    }
}
