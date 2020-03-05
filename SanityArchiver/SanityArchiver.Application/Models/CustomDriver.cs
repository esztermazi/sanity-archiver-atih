using System.Collections.ObjectModel;

namespace SanityArchiver.Application.Models
{
    public class CustomDriver : CustomItem
    {
        public ObservableCollection<CustomDirectory> Items { get; set; }

        public CustomDriver()
        {
            Items = new ObservableCollection<CustomDirectory>();
        }
    }
}
