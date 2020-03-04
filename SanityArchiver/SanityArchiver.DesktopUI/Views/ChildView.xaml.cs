using SanityArchiver.Application.Models;
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

        private void Set_ContextMenuContent(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MyDataGrid.SelectedItem == null) MyMenuItem.Header = "New";
            else
            {
                MyMenuItem.Header = "Rename";
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
