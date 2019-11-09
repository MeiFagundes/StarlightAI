﻿using System;
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

        public ClassificationController(string datasetpath = null, bool debug = false) {

            _binaryClassificators = new List<BinaryClassificator>();

            if (debug)
                Console.WriteLine("=============== Starlight Build ===============\n");

            foreach (var intentName in GetIntentList())
                _binaryClassificators.Add(new BinaryClassificator(intentName, datasetpath, false, debug));
        }

        
        public JObject Cognize(string query) {

            Utterance utterance = new Utterance();
            utterance.Query = query;

            for (int i = 0; i < _intentList.Count; i++)
                utterance.Intents.Add(_binaryClassificators[i].Classify(query));

            EntityExtraction.EntityExtractorController.Fetch(utterance);

            return utterance.GetResponse();

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
    }
}
