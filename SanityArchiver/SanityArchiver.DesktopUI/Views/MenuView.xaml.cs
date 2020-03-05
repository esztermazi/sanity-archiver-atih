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
    }
}
