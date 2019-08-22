using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.ML.DataOperationsCatalog;

namespace Starlight.MachineLearning {
    interface IClassificator {

        void Classify(string query);
    }
}
