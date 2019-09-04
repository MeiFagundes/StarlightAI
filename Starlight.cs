using System;

namespace Starlight {
    public class Starlight {
        public static void Main() {

            ClassificationController cc = new ClassificationController();

            while (true) {
                Console.WriteLine("Enter an utterance:");
                Console.Write("> ");
                Console.WriteLine(cc.Cognize(Console.ReadLine()));
            }
        }
    }
}
