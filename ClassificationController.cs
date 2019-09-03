using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using Starlight.MachineLearning;

namespace Starlight {
    public class ClassificationController {

        static readonly string datasetPath = Path.Combine(Environment.CurrentDirectory, "Dataset");
        static List<string> _intentList;
        List<BinaryClassificator> _binaryClassificators;
        Utterance _utterance;

        public ClassificationController() {

            _binaryClassificators = new List<BinaryClassificator>();
            Console.WriteLine("=============== Starlight ML Cognition Build ===============\n");

            foreach (var intentName in GetIntentList()) {

                _binaryClassificators.Add(new BinaryClassificator(intentName, false));
                
            }
        }

        
        public void Cognize(string query) {

            Console.WriteLine("\n=============== Starlight ML Cognition ===============\n");
            _utterance = new Utterance();
            _utterance.Query = query;

            for (int i = 0; i < _intentList.Count; i++)
                _utterance.Intents.Add(_binaryClassificators[i].Classify(query));

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

        public String GetOutput() {

            JObject rss =
                new JObject(
                    new JProperty("query", _utterance.Query)
                    );

            return rss.ToString();
        }
    }
}
