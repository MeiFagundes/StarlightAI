using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using Starlight.MachineLearning;

namespace Starlight {
    public class ClassificationController {

        static readonly string datasetPath = Path.Combine(Environment.CurrentDirectory, "Dataset");
        static List<string> _intentList;
        List<BinaryClassificator> _binaryClassificators;

        public ClassificationController() {

            _binaryClassificators = new List<BinaryClassificator>();
            Console.WriteLine("=============== Starlight ML Cognition Build ===============\n");

            foreach (var intentName in GetIntentList()) {

                _binaryClassificators.Add(new BinaryClassificator(intentName, false));
                
            }
        }

        
        public JObject Cognize(string query) {

            Utterance utterance = new Utterance();
            utterance.Query = query;

            for (int i = 0; i < _intentList.Count; i++)
                utterance.Intents.Add(_binaryClassificators[i].Classify(query));

            return getJSON(utterance);

        }

        static List<string> GetIntentList() {

            DirectoryInfo d = new DirectoryInfo(datasetPath);
            FileInfo[] files = d.GetFiles("*.txt");
            _intentList = new List<string>();
            foreach (FileInfo file in files) {
                _intentList.Add(file.Name.Replace(".txt", string.Empty));
            }
            return _intentList;
        }

        JObject getJSON(Utterance utterance) {

            JObject json =
                new JObject(
                    new JProperty("query", utterance.Query),
                    new JProperty("intents", 
                        new JArray(
                            from intent in utterance.Intents
                            orderby intent.Score descending
                            select new JObject(
                                new JProperty("intent", intent.Name),
                                new JProperty("score", intent.Score)
                            )
                        )
                    )
                );

            return json;
        }
    }
}
