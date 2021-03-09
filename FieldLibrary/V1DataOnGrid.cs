using System;
using System.Numerics;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.Serialization;
using System.Text;

namespace FieldLibrary
{
    [Serializable]
    public class V1DataOnGrid : V1Data, IEnumerable<DataItem>, ISerializable
    {
        public V1DataOnGrid(string info, DateTime date, Grid grid_) : base(info, date) {
            grid = grid_;
            arr = new Vector3[grid.amount_of_nodes];
        }

        public static implicit operator V1DataCollection(V1DataOnGrid obj) {
            V1DataCollection new_obj = new V1DataCollection(obj.Info, obj.Date);
            for (int i = 0; i < obj.grid.amount_of_nodes; i++) {
                DataItem dataItem = new DataItem(obj.grid.t0 + obj.grid.time_step * i, obj.arr[i]);
                new_obj.list.Add(dataItem);
            }
            return new_obj;
        }

        public void InitRandom(float minValue, float maxValue) {
            Random random = new Random();
            float range = maxValue - minValue;
            for (int i = 0; i < grid.amount_of_nodes; i++)
            {
                arr[i] = new Vector3(
                                        minValue + (float)random.NextDouble() * range,
                                        minValue + (float)random.NextDouble() * range,
                                        minValue + (float)random.NextDouble() * range
                                    );
            }
        }
        public override float[] NearZero(float eps) {
            float[] nodes = new float[grid.amount_of_nodes];
            int length = 0;
            for (int i = 0; i < grid.amount_of_nodes; i++) {
                float tmp = arr[i].Length();
                if (tmp < eps) {
                    nodes[length++] = grid.t0 + grid.time_step * i;
                }
            }
            float[] ans = new float[length];

            Array.Copy(nodes, ans, length);
            return ans;
        }

        public override string ToString() {
            return "V1DataOnGrid" + " " + base.ToString() + " " + grid.ToString();
        }

        public override string ToLongString() {
            string str = ToString() + Environment.NewLine;

            for (int i = 0; i < grid.amount_of_nodes; i++) {
                str += "Time: " + (grid.t0 + grid.time_step * i).ToString() + " " + arr[i].ToString() + Environment.NewLine;
            }
            return str;
        }
        public override string ToLongString(string format) {
            string str = "V1DataOnGrid" + " " + base.ToLongString(format)
                    + " " + grid.ToString(format) + Environment.NewLine;

            for (int i = 0; i < grid.amount_of_nodes; i++) {
                str += "Time: " + (grid.t0 + grid.time_step * i).ToString(format) + " "
                        + arr[i].ToString(format) + " Length: "
                        + arr[i].Length().ToString(format) + Environment.NewLine;
            }
            return str;
        }
        public IEnumerator<DataItem> GetEnumerator() {
            V1DataCollection v1DataCollection = this;
            return v1DataCollection.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("arr", SerializeVector3Array(arr), typeof(string));
            info.AddValue("grid", grid, typeof(Grid));
            info.AddValue("info", Info);
            info.AddValue("date", Date);
        }
        public V1DataOnGrid(SerializationInfo info, StreamingContext streamingContext) :
            base(info.GetString("info"), info.GetDateTime("date"))
        {
            grid = (Grid)info.GetValue("grid", typeof(Grid));
            arr = DeserializeVector3Array(info.GetString("arr"));
        }

        public static string SerializeVector3Array(Vector3[] aVectors)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Vector3 v in aVectors)
            {
                sb.Append(v.X).Append(" ").Append(v.Y).Append(" ").Append(v.Z).Append("|");
            }
            if (sb.Length > 0) // remove last "|"
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
        public static Vector3[] DeserializeVector3Array(string aData)
        {
            if (aData.Length == 0)
                return new Vector3[0];

            string[] vectors = aData.Split('|');
            Vector3[] result = new Vector3[vectors.Length];
            
            for (int i = 0; i < vectors.Length; i++)
            {
                string[] values = vectors[i].Split(' ');
                result[i] = new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
            }
            return result;
        }

        public Grid grid { get; set; }
        public Vector3[] arr { get; set; }
    }
}
