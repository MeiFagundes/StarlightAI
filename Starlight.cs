using System;

namespace Starlight {
    public class Starlight {
        public static void Main() {

            IntentClassificator cc = new IntentClassificator();

            while (true) {
                 Console.WriteLine("Enter an utterance:");
                Console.Write("> ");
                Console.WriteLine(cc.Cognize(Console.ReadLine()));
            }
        }
    }
}
