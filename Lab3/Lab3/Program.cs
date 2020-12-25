using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3
{
    class Program {
        public static void ChangesCollector(object sender, DataChangedEventArgs args) {
            Console.WriteLine(args.ToString() + Environment.NewLine);
        }
        public static void Main() {
            try {
                V1MainCollection v1MainCollection = new V1MainCollection();
                v1MainCollection.DataChanged += ChangesCollector;

                Console.WriteLine("Add elements to a collection:" + Environment.NewLine);
                v1MainCollection.AddDefaults();

                Console.WriteLine(Environment.NewLine + "v1MainCollection after AddDefaults():" + Environment.NewLine);
                foreach (var elem in v1MainCollection) {
                    Console.WriteLine(elem.ToLongString() + Environment.NewLine);
                }
                
                Console.WriteLine(Environment.NewLine + "Changing V1Data Info property in the element with index 0:" + Environment.NewLine);
                v1MainCollection[0].Info = "Something new";

                Console.WriteLine(Environment.NewLine + "Changing element with index 1:" + Environment.NewLine);
                v1MainCollection[1] = new V1DataCollection("New Element", new DateTime());

                Console.WriteLine(Environment.NewLine + "Remove element with index 3:" + Environment.NewLine);
                V1DataOnGrid item = new V1DataOnGrid("grid1", new DateTime(2008, 5, 1, 7, 30, 52), new Grid());
                v1MainCollection.Remove(item.Info, item.Date);

                Console.WriteLine(Environment.NewLine + "v1MainCollection after all changes:" + Environment.NewLine);
                foreach(var elem in v1MainCollection) {
                    Console.WriteLine(elem.ToLongString() + Environment.NewLine);
                }

                Console.WriteLine("=====");
                v1MainCollection[1].Info = "aa";
                v1MainCollection[1].Info = "bb";

            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}
