using System;
using System.ComponentModel;

namespace FieldLibrary
{
    [Serializable]
    public abstract class V1Data : INotifyPropertyChanged {
        private string info;
        private DateTime date;
        public V1Data(string info_, DateTime date_) {
            info = info_;
            date = date_;
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property_name) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property_name));
        }

        public string Info {
            get { return info; }
            set {
                if (info != value) {
                    info = value;
                    OnPropertyChanged("V1Data Info property is changed to: " + value.ToString());
                }
            }
        }
        public DateTime Date {
            get { return date; }
            set {
                if (date != value) {
                    date = value;
                    OnPropertyChanged("V1Data Date property is changed to: " + value.ToString());
                }
            }
        }
        public abstract float[] NearZero(float eps);
        public abstract string ToLongString();
        public override string ToString() {
            return Info.ToString() + " " + Date.ToString();
        }
        public virtual string ToLongString(string format) {
            return Info.ToString() + " " + Date.ToString(format);
        }
    }
}
