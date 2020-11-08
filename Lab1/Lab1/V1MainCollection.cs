using System;
using System.Collections.Generic;
using System.Collections;

namespace Lab1
{
    public class V1MainCollection : IEnumerable<V1Data> {
        public int Count {
            get {
                return list.Count;
            }
        }
        public void Add(V1Data item) {
            list.Add(item);
        }
        public bool Remove(string id, DateTime dateTime) {
            int cnt_before_removal = list.Count;
            list.RemoveAll(item => ((item.info == id) && (item.date == dateTime)));
            return cnt_before_removal != list.Count;
        }
        public void AddDefaults() {
            DateTime time = new DateTime(2008, 5, 1, 8, 30, 52);
            Grid grid = new Grid(0, 1, 4);
            V1DataCollection col1 = new V1DataCollection("col1", time);
            V1DataCollection col2 = new V1DataCollection("col2", time);
            V1DataOnGrid grid1 = new V1DataOnGrid("grid1", time, grid);
            V1DataOnGrid grid2 = new V1DataOnGrid("grid2", time, grid);

            col1.InitRandom(3, 1, 5, 1, 15);
            col2.InitRandom(4, 0, 3, 11, 14);
            grid1.InitRandom(3, 7);
            grid2.InitRandom(1, 20);

            list.Add(col1);
            list.Add(col2);
            list.Add(grid1);
            list.Add(grid2);
        }
        public override string ToString() {
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < list.Count; i++) {
                sb.AppendLine(list[i].ToString());
            }
            return sb.ToString();
        }
        public IEnumerator<V1Data> GetEnumerator() {
            return this.list.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

        private List<V1Data> list = new List<V1Data>();
    }
}
