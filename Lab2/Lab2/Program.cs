using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2
{
    class Program {
        public static void Main()
        {
            try {
                V1DataCollection v1DataCollection = new V1DataCollection(@"..\..\..\..\data.txt");
                Console.WriteLine("V1DataCollection from data.txt:" + Environment.NewLine + Environment.NewLine
                    + v1DataCollection.ToLongString("F") + Environment.NewLine);

                V1MainCollection v1MainCollection = new V1MainCollection();
                v1MainCollection.AddDefaults();

                Console.WriteLine("V1MainCollection with default values:" + Environment.NewLine + Environment.NewLine
                    + v1MainCollection.ToLongString("F"));

                Console.WriteLine("Max amount of field measurements:" + Environment.NewLine
                    + v1MainCollection.GetMaxAmount.ToString("D"));

                Console.WriteLine(Environment.NewLine + "Sorted DataItem elements:" + Environment.NewLine);
                var iter = v1MainCollection.GetSorted;
                foreach (var dataItem in iter) {
                    Console.WriteLine(dataItem.ToString("F"));
                }

                Console.WriteLine(Environment.NewLine + "All unique 't' in V1MainCollection:" + Environment.NewLine);
                foreach (var t in v1MainCollection.UniqueTime) {
                    Console.WriteLine(t.ToString("F"));
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}
