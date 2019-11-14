using System;
using System.Xml.Linq;

namespace Starlight {
    public class Starlight {
        public static void Main() {
            
            IntentClassificator cc = new IntentClassificator(null, true);

            while (true) {
                 Console.WriteLine("Enter an utterance:");
                Console.Write("> ");
                Console.WriteLine(cc.Cognize(Console.ReadLine(), true));
            }
        }
    }
}
