

using Starlight.MachineLearning;
using System;
using System.IO;

namespace Starlight {
    public class Starlight {
        public static void Main() {

            ClassificationController cc = new ClassificationController();

            while (true) {
                Console.WriteLine("Enter a phrase:");
                cc.Cognize(Console.ReadLine());
            }
        }
    }
}
