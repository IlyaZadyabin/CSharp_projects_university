using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Lab2
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
            V1DataCollection col3 = new V1DataCollection("col3", time); //V1DataCollection with empty List<DataItem>
            V1DataOnGrid grid1 = new V1DataOnGrid("grid1", time, grid);
            V1DataOnGrid grid2 = new V1DataOnGrid("grid2", time, grid);
            V1DataOnGrid grid3 = new V1DataOnGrid("grid3", time, new Grid(1.0f, 1.5f, 0)); //V1DataOnGrid with zero nodes

            col1.InitRandom(3, 1, 5, 1, 15);
            col2.InitRandom(4, 0, 3, 11, 14);
            grid1.InitRandom(3, 7);
            grid2.InitRandom(1, 20);

            list.Add(col1);
            list.Add(col2);
            list.Add(col3);
            list.Add(grid1);
            list.Add(grid2);
            list.Add(grid3);
        }
        public override string ToString() {
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < list.Count; i++) {
                sb.AppendLine(list[i].ToString());
            }
            return sb.ToString();
        }
        public string ToLongString(string format) {
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < list.Count; i++) {
                sb.AppendLine(list[i].ToLongString(format));
            }
            return sb.ToString();
        }
        public IEnumerator<V1Data> GetEnumerator() {
            return this.list.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

        public int GetMaxAmount {
            get {
                int amount1 = ( from v1DataCollection in list.OfType<V1DataCollection>()
                                from v1DataItem in v1DataCollection
                                select v1DataItem
                              ).Count();

                int amount2 = ( from v1DataOnGrid in list.OfType<V1DataOnGrid>()
                                select v1DataOnGrid.grid.amount_of_nodes
                              ).Sum();

                return amount1 + amount2;
            }
        }
        public IEnumerable<DataItem> GetSorted {
            get {
                var query1 = from v1DataCollection in list.OfType<V1DataCollection>()
                             from v1DataItem in v1DataCollection
                             select v1DataItem;

                var query2 = from v1DataOnGrid in list.OfType<V1DataOnGrid>() //Implicit conversion in V1DataOnGrid GetEnuminator()
                             from v1DataItem in v1DataOnGrid
                             select v1DataItem;

                var query3 = query1.Union(query2);

                return from dataItem in query3
                       orderby dataItem.vec.Length() descending
                       select dataItem;
            }
        }

        public IEnumerable<float> UniqueTime {
            get {
                var query1 = GetSorted;
                var query2 = from dataItem in query1
                             select dataItem.t;

                return query2.Distinct();
            }
        }
        private List<V1Data> list = new List<V1Data>();
    }
}
