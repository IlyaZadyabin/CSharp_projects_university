using System;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime dateTime = new DateTime(2020, 10, 20, 12, 43, 10);
            Grid grid = new Grid(0, 1.1f, 3);
            V1DataOnGrid v1DataOnGrid = new V1DataOnGrid("first", dateTime, grid);
            Console.WriteLine(v1DataOnGrid.ToLongString());

            V1DataCollection v1DataCollection = new V1DataCollection("tmp", dateTime);
            v1DataCollection = v1DataOnGrid;
            Console.WriteLine(v1DataCollection.ToLongString());

            V1MainCollection v1MainCollection = new V1MainCollection();
            v1MainCollection.AddDefaults();
            Console.WriteLine(v1MainCollection.ToString());

            foreach (var item in v1MainCollection)
            {
                float[] arr = item.NearZero(0.13f);
                Console.WriteLine(item.ToString());
            }
        }
    }
}
