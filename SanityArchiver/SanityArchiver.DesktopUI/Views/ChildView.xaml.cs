using SanityArchiver.Application.Models;
using System.Windows.Controls;

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
    }
}
