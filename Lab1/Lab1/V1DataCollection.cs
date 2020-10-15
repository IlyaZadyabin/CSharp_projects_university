using System;
using System.Collections.Generic;
using System.Numerics;

namespace Lab1
{
    public class V1DataCollection : V1Data
    {
        public V1DataCollection(string str_, DateTime date_) : base(str_, date_) { }
        public void InitRandom(int nItems, float tmin, float tmax, float minValue, float maxValue)
        {
            Random random = new System.Random();
            for (int i = 0; i < nItems; i++)
            {
                DataItem tmp = new DataItem();
                tmp.t = (float)random.NextDouble() * (tmax - tmin);
                tmp.vec = new Vector3(
                                        (float)random.NextDouble() * (maxValue - minValue),
                                        (float)random.NextDouble() * (maxValue - minValue),
                                        (float)random.NextDouble() * (maxValue - minValue)
                                    );
                list.Add(tmp);
            }
        }
        public override float[] NearZero(float eps)
        {
            float[] nodes = new float[list.Count];
            int length = 0;
            for (int i = 0; i < list.Count; i++)
            {
                float tmp = (float)Math.Sqrt(list[i].vec.X * list[i].vec.X
                                                + list[i].vec.Y * list[i].vec.Y
                                                + list[i].vec.Z * list[i].vec.Z);
                if (tmp < eps)
                {
                    nodes[length++] = list[i].t;
                }
            }
            float[] ans = new float[length];

            Array.Copy(nodes, ans, length);
            return ans;
        }

        public override string ToString()
        {
            return "V1DataCollection" + " " + base.ToString() + " " + list.Count.ToString();
        }
        public override string ToLongString()
        {
            string str = this.ToString();
            for (int i = 0; i < list.Count; i++)
            {
                str += list[i].ToString();
            }
            return str;
        }
        List<DataItem> list { get; set; } = new List<DataItem>();
    }
}
