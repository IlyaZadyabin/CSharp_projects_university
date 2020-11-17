using System;

namespace Lab2
{
    public abstract class V1Data {

        public V1Data(string info_, DateTime date_) {
            info = info_;
            date = date_;
        }
        public abstract float[] NearZero(float eps);
        public abstract string ToLongString();
        public string info { get; set; }
        public DateTime date { get; set; }

        public override string ToString() {
            return info.ToString() + " " + date.ToString();
        }
        public virtual string ToLongString(string format) {
            return info.ToString() + " " + date.ToString(format);
        }
    }
}
