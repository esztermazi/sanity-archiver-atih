using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
