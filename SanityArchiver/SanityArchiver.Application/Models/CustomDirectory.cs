using System.Collections.ObjectModel;

namespace SanityArchiver.Application.Models
{
    public class CustomDirectory : CustomItem
    {
        public ObservableCollection<CustomItem> Items { get; set; }

        public CustomDirectory()
        {
            Items = new ObservableCollection<CustomItem>();
        }
    }
}
