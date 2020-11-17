using System.Numerics;

namespace Lab2
{
    public struct DataItem {
        public DataItem(float t_, Vector3 vec_) {
            t = t_;
            vec = vec_;
        }
        public float t { get; set; }
        public Vector3 vec { get; set; }

        public override string ToString() {
            return t.ToString() + ": " + vec.ToString();
        }
        public string ToString(string format){
            return "Time: " + t.ToString(format) + " " + vec.ToString(format) + " Length: " + vec.Length().ToString(format);
        }
    }
}
