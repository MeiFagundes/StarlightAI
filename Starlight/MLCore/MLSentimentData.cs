using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Starlight.MLCore {
    class MLSentimentData {

        [LoadColumn(0), ColumnName("Label")]
        public bool Sentiment;

        [LoadColumn(1)]
        public string SentimentText;
    }
}
