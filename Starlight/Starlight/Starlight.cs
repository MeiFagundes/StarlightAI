

using Starlight.MachineLearning;
using System;
using System.IO;

namespace Starlight {
    public class Starlight {
        public static void Main() {

            Console.WriteLine("Enter a phrase:");
            ClassificationController.Cognize(Console.ReadLine());
        }
    }
}
