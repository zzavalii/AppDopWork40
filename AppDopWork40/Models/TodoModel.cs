using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDopWork40.Models
{
    public class TodoModel : INotifyPropertyChanged
    {
        private DateTime _creationDate = DateTime.Now;
        private bool _isDone;
        private string _text;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler TextChanged; // Add this event

        public DateTime CreationDate
        {
            get { return _creationDate; }
            set
            {
                if (_creationDate == value)
                    return;
                _creationDate = value;
                OnPropertyChanged("CreationDate");
            }
        }

        public bool IsDone
        {
            get { return _isDone; }
            set
            {
                if (_isDone == value)
                    return;
                _isDone = value;
                OnPropertyChanged("IsDone");
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                if (_text == value)
                    return;
                _text = value;
                OnPropertyChanged("Text");
                // Raise the TextChanged event
                TextChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
