using System;
using System.Collections.Generic;
using System.Collections;
using System.Numerics;
using System.Globalization;

namespace Lab2
{
    public class V1DataCollection : V1Data, IEnumerable<DataItem> {
        public V1DataCollection(string str_, DateTime date_) : base(str_, date_) {}

        // info
        // date         ru-RU type
        // time x,y,z   en-US type
        // time x,y,z   en-US type
        // ...
        public V1DataCollection(string filename) : base("", new DateTime()) {
            try {
                CultureInfo cultureInfo_ruRU = new CultureInfo("ru-RU");
                CultureInfo cultureInfo_enUS = new CultureInfo("en-US");

                string[] lines = System.IO.File.ReadAllLines(filename);
                info = lines[0];
                date = Convert.ToDateTime(lines[1], cultureInfo_ruRU);

                for (int i = 2; i < lines.Length; i++) {
                    string[] tokens = lines[i].Split(' ');
                    string[] coord = tokens[1].Split(',');

                    list.Add(new DataItem
                    {
                        t = Convert.ToSingle(tokens[0], cultureInfo_enUS),
                        vec = new Vector3( Convert.ToSingle(coord[0], cultureInfo_enUS),
                                           Convert.ToSingle(coord[1], cultureInfo_enUS),
                                           Convert.ToSingle(coord[2], cultureInfo_enUS))
                    });
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
        public void InitRandom(int nItems, float tmin, float tmax, float minValue, float maxValue) {
            Random random = new System.Random();
            for (int i = 0; i < nItems; i++) {
                DataItem tmp = new DataItem();
                tmp.t = tmin + (float)random.NextDouble() * (tmax - tmin);
                tmp.vec = new Vector3(
                                        minValue + (float)random.NextDouble() * (maxValue - minValue),
                                        minValue + (float)random.NextDouble() * (maxValue - minValue),
                                        minValue + (float)random.NextDouble() * (maxValue - minValue)
                                    );
                list.Add(tmp);
            }
        }
        public override float[] NearZero(float eps) {
            float[] nodes = new float[list.Count];
            int length = 0;
            for (int i = 0; i < list.Count; i++) {
                float tmp = list[i].vec.Length();
                if (tmp < eps) {
                    nodes[length++] = list[i].t;
                }
            }
            float[] ans = new float[length];

            Array.Copy(nodes, ans, length);
            return ans;
        }

        public override string ToString() {
            return "V1DataCollection" + " " + base.ToString() + " " + list.Count.ToString();
        }
        public override string ToLongString() {
            string str = ToString() + Environment.NewLine;
            for (int i = 0; i < list.Count; i++) {
                str += list[i].ToString() + Environment.NewLine;
            }
            return str;
        }
        public override string ToLongString(string format) {
            string str = "V1DataCollection" + " " + base.ToLongString(format)
                    + " " + list.Count.ToString(format) + Environment.NewLine;
            for (int i = 0; i < list.Count; i++) {
                str += list[i].ToString(format) + Environment.NewLine;
            }
            return str;
        }
        public IEnumerator<DataItem> GetEnumerator() {
            return list.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
        public List<DataItem> list { get; set; } = new List<DataItem>();
    }
}
