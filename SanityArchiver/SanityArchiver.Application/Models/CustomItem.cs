using Hangfire.Annotations;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SanityArchiver.Application.Models
{
    public class CustomItem : INotifyPropertyChanged
    {
        private string _name;
        private string _shortName;
        private DateTime _dateModified;
        private string _type;
        private string _size;
        private string _path;

        public event PropertyChangedEventHandler PropertyChanged;


        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string ShortName
        {
            get => _shortName;
            set
            {
                _shortName = value;
                OnPropertyChanged();
            }
        }


        public DateTime DateCreated
        {
            get => _dateModified;
            set
            {
                _dateModified = value;
                OnPropertyChanged();
            }
        }

        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

        public string Size
        {
            get => _size;
            set
            {
                _size = value;
                OnPropertyChanged();
            }
        }
        public string Path
        {
            get => _path;
            set
            {
                _path = value;
                OnPropertyChanged();
            }
        }


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
