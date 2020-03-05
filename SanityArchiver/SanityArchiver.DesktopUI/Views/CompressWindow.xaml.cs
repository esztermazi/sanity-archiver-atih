using SanityArchiver.Application.Models;
using System;
using System.Collections.Generic;
using System.IO.Compression;
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
    /// Interaction logic for CompressWindow.xaml
    /// </summary>
    public partial class CompressWindow : Window
    {
        private MainWindow mW = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

        public CompressWindow()
        {
            InitializeComponent();
        }
        protected override void OnClosed(EventArgs e)
        {
            mW.ctrMenuView.Status = false;
            base.OnClosed(e);
        }

        private void Compress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                CompressionAndDisplay();
                mW.ctrMenuView.Status = false;
                Close();
            }
        }

        private void CompressionAndDisplay()
        {
            try
            {
                CustomFile selectedFile = (CustomFile)mW.ctrChildView.Custom.Items.Single(item => item.ShortName.Equals(textField.Text) && item.Type != "File folder");
                string newCompressionedFile = CompressSelectedFile(selectedFile);
                mW.ctrChildView.Custom.Items.Add(new CustomFile
                {
                    Name = newCompressionedFile,
                    ShortName = Path.GetFileNameWithoutExtension(newCompressionedFile) + ".zip",
                    Size = new FileInfo(newCompressionedFile).Length / 512 + " KB",
                    Type = Path.GetExtension(newCompressionedFile),
                    DateCreated = new FileInfo(newCompressionedFile).CreationTime,
                });
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("File not found", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void clickBtn(object sender, RoutedEventArgs e)
        {
            CompressionAndDisplay();
            mW.ctrMenuView.Status = false;
            Close();
        }

        private string CompressSelectedFile(CustomFile selectedFile)
        {
            StreamReader sr = new StreamReader(selectedFile.Name);
            string selectedFileContent = sr.ReadToEnd();
            sr.Close();

            string newCompressionedFilePath = $@"{selectedFile.Name.Replace(selectedFile.ShortName, "")}{GetTimestamp(DateTime.Now)}.zip";

            using (FileStream zipToCreate = new FileStream(newCompressionedFilePath, FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(zipToCreate, ZipArchiveMode.Create))
                {
                    ZipArchiveEntry newEntry = archive.CreateEntry(selectedFile.ShortName);
                    using (StreamWriter writer = new StreamWriter(newEntry.Open()))
                    {
                        writer.WriteLine(selectedFileContent);
                    }
                }
            }

            return newCompressionedFilePath;
        }

        private string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
    }
}
