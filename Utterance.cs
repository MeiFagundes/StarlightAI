using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public JObject GetResponse() {

            JObject json =
                new JObject(
                    new JProperty("query", Query),
                    new JProperty("intents",
                        new JArray(
                            (from intent in Intents
                             orderby intent.Score descending
                             select new JObject(
                                 new JProperty("intent", intent.Name),
                                 new JProperty("score", intent.Score)
                             )).Take(3)
                        )
                    )
                );

            DateTime dateTime = Entity.DateTime;

            if (Entity != null) {
                json.Add(new JProperty("entities",
                    new JObject(
                        new JProperty("entity", Entity.EntityText),
                        new JProperty("type", Entity.Type),
                        new JProperty("startIndex", Entity.StartIndex),
                        new JProperty("endIndex", Entity.EndIndex),
                        new JProperty("date", dateTime == null ? null : dateTime.ToString("yyyy-MM-dd")),
                        new JProperty("time", dateTime == null ? null : dateTime.ToString("hh:mm tt"))
                        )
                    )
                );
            }

            return json;
        }
    }
}
