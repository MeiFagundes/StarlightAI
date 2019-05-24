using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Starlight.MLCore {
    class MLSentimentPrediction : MLSentimentData {

        [ColumnName("PredictedLabel")]

        public bool Prediction { get; set; }
        public float Probability { get; set; }
        public float Score { get; set; }
    }
}
