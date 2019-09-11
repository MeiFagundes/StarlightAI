using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Starlight.MachineLearning {
    public class ClassificationData {

        [LoadColumn(0), ColumnName("Label")]
        public bool Classification;

        [LoadColumn(1)]
        public string Content;
    }
}
