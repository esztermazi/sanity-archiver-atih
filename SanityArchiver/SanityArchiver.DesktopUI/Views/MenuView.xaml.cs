using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SanityArchiver.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        public bool Status { get; set; } = false;

        public MenuView()
        {
            InitializeComponent();
        }

        public void Compress(object sender, RoutedEventArgs e)
        {
            if (!Status)
            {
                CompressWindow cW = new CompressWindow();
                cW.Show();
                Status = true;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mw.Close();
        }
    }
}
