using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.ML.DataOperationsCatalog;

namespace Starlight.MLCore {
    interface IClassificator {

        Intent Classify(string query);
    }
}
