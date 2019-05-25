

using Starlight.MachineLearning;
using System;
using System.IO;

namespace Starlight {
    public class Starlight {
        public static void Main() {

            BinaryClassificator bc = new BinaryClassificator("test");
            Console.WriteLine("Enter a phrase:");
            bc.Classify(Console.ReadLine());
        }
    }
}
