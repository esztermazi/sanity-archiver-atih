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
		private ObservableCollection<CustomDirectory> _directories;

		public ObservableCollection<CustomDirectory> Items
		{
			get { return _directories; }
			set { _directories = value; }
		}

		public CustomDriver()
		{
			Items = new ObservableCollection<CustomDirectory>();
		}
	}
}
