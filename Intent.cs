using System;

namespace Starlight {
    public class Intent {

        public String Name { get; set; }
        public float Score { get; set; }

        public Intent() { }

        public Intent(String Name, float Score) {
            this.Name = Name;
            this.Score = Score;
        }
    }
}
