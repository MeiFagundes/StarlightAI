using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Starlight.MachineLearning;

namespace Starlight {
    class ClassificationController {

        static readonly string datasetPath = Path.Combine(Environment.CurrentDirectory, "Dataset");
        public static void Cognize(string query) {
            foreach (var intentName in GetIntentList()) {

                BinaryClassificator bc = 
                    new BinaryClassificator(intentName, false);
                bc.Classify(query);
            }
        }

        static List<string> GetIntentList() {

            DirectoryInfo d = new DirectoryInfo(datasetPath);
            FileInfo[] files = d.GetFiles("*.txt");
            List<string> intentList = new List<string>();
            foreach (FileInfo file in files) {
                 intentList.Add(file.Name.Replace(".txt", string.Empty));
            }
            return intentList;
        }
    }
}
