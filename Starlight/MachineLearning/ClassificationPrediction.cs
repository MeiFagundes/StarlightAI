using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Starlight.MachineLearning {
    class ClassificationPrediction : ClassificationData {

        [ColumnName("PredictedLabel")]

        public bool Prediction { get; set; }
        public float Probability { get; set; }
        public float Score { get; set; }
    }
}
