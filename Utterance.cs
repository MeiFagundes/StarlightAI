using System;
using System.Collections.Generic;
using System.Text;

namespace Starlight {
    public class Utterance {

        private string _query;
        public String Query {
            get {
                return _query;
            }
            set {
                _query = value.ToLower();
            }
        }
        public List<Intent> Intents { get; set; }
        public Intent TopScoringIntent {
            get {
                return GetTopScoringIntent();
            }
        }
        public Entity Entity { get; set; }

        public Utterance() {
            Intents = new List<Intent>();
            Entity = new Entity();
        }

        private Intent GetTopScoringIntent() {

            Intent topScoringIntent = new Intent();
            float maxValue = 0;

            foreach (Intent intent in Intents) {
                if (intent.Score > maxValue) {
                    topScoringIntent = intent;
                    maxValue = intent.Score;
                }
            }

            return topScoringIntent;
        }
    }
}
