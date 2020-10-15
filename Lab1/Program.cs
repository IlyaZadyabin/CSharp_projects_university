using System;

namespace Lab1
{
    class Program {
        static void Main(string[] args) {
            DateTime dateTime = new DateTime(2020, 10, 20, 12, 43, 10);
            Grid grid = new Grid(0, 1.1f, 3);
            V1DataOnGrid v1DataOnGrid = new V1DataOnGrid("first", dateTime, grid);
            v1DataOnGrid.InitRandom(3,10);
            Console.WriteLine(v1DataOnGrid.ToLongString());

            Console.WriteLine("--------------------------\n");

            V1DataCollection v1DataCollection = v1DataOnGrid;
            Console.WriteLine(v1DataCollection.ToLongString());

            Console.WriteLine("--------------------------\n");

            V1MainCollection v1MainCollection = new V1MainCollection();
            v1MainCollection.AddDefaults();
            Console.WriteLine(v1MainCollection.ToString());

            Console.WriteLine("--------------------------\n");

            foreach (var item in v1MainCollection) {
                float[] arr = item.NearZero(10);
                if (arr.Length != 0) {
                    Console.WriteLine("<" + string.Join(", ", arr) + ">");
                }
            }
        }
    }
}
