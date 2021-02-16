using System;
using System.Numerics;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.Serialization;

namespace FieldLibrary
{
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
            Random random = new System.Random();
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
            string str = this.ToString() + Environment.NewLine;

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
            info.AddValue("arr", arr);
            info.AddValue("grid", grid);
            info.AddValue("info", Info);
            info.AddValue("date", Date);
        }
        public V1DataOnGrid(SerializationInfo info, StreamingContext streamingContext) :
            base(info.GetString("info"), info.GetDateTime("date"))
        {
            grid = (Grid)info.GetValue("grid", typeof(Grid));
            arr = (Vector3[])info.GetValue("arr", typeof(Vector3[]));
        }

        public Grid grid { get; set; }
        public Vector3[] arr { get; set; }
    }
}
