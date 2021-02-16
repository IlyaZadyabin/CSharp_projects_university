using System;
using System.Numerics;
using System.Runtime.Serialization;

namespace FieldLibrary
{
    [Serializable]
    public struct DataItem : ISerializable
    {
        public DataItem(float t_, Vector3 vec_) {
            t = t_;
            vec = vec_;
        }
        public float t { get; set; }
        public Vector3 vec { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("vec_X", vec.X);
            info.AddValue("vec_Y", vec.Y);
            info.AddValue("vec_Z", vec.Z);
            info.AddValue("t", t);
        }
        public DataItem(SerializationInfo info, StreamingContext streamingContext)
        {
            vec = new Vector3( info.GetSingle("vec_X"),
                               info.GetSingle("vec_Y"),
                               info.GetSingle("vec_Z")
                             ); 
            t = info.GetSingle("t");
        }

        public override string ToString() {
            return t.ToString() + ": " + vec.ToString();
        }
        public string ToString(string format){
            return "Time: " + t.ToString(format) + " " + vec.ToString(format) + " Length: " + vec.Length().ToString(format);
        }
    }
}
