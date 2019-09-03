using System;
using System.Collections.Generic;
using System.Text;

namespace Starlight {
    class Utterance {

        public string Query { get; set; }
        public List<Intent> Intents {
            get{ Intents.Sort((x, y) => x.Score.CompareTo(y.Score));
                return Intents;
            }
            set { Intents = value; } }
    }
}
