using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
