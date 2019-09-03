using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.ML.DataOperationsCatalog;

namespace Starlight.MachineLearning {
    interface IClassificator {

        Intent Classify(string query);
    }
}
