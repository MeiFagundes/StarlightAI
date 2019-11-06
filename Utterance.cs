using System;
using System.Collections.Generic;
using System.Text;

namespace Starlight {
    public class Utterance {

        public string Query { get; set; }
        public List<Intent> Intents { get; set; }
        public Entity Entity { get; set; }

        public Utterance() {
            Intents = new List<Intent>();
            Entity = new Entity();
        }
    }
}
