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
using System.Windows.Shapes;

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

        private void Compress_KeyDown(object sender, RoutedEventArgs e)
        {
            //mW.ctrChildView
        }

        private void clickBtn(object sender, RoutedEventArgs e)
        {

        }
    }
}
