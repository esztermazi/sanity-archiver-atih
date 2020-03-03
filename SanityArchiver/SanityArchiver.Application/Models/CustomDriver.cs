using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
