﻿using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static Microsoft.ML.DataOperationsCatalog;

namespace Starlight.MachineLearning {
    class BinaryClassificator : IClassificator {

        MLContext _mlContext;
        ITransformer _model;
        readonly string _datasetName;

        public BinaryClassificator(string datasetName, bool hasHeader) {

            _mlContext = new MLContext();
            _datasetName = datasetName;
            string datasetPath = Path.Combine(Environment.CurrentDirectory, "Dataset", _datasetName + ".txt");
            TrainTestData splitDataView = LoadData(datasetPath, hasHeader);
            _model = BuildAndTrainModel(splitDataView.TrainSet);
            Evaluate(splitDataView.TestSet);
        }

        public BinaryClassificator(string datasetName) : this(datasetName, false) {
            
        }

        public void Classify(string query) {

            

            ClassificationData statement = new ClassificationData {
                Content = query
            };
            PredictSingleItem(statement);
        }

        ITransformer BuildAndTrainModel(IDataView splitTrainSet) {

            // converts the text column into a numeric key type column used by the machine learning algorithm and adds it as a new dataset column:
            var estimator = _mlContext.Transforms.Text.FeaturizeText(outputColumnName: "Features", inputColumnName: nameof(ClassificationData.Content))
                .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Label", featureColumnName: "Features"));

            // Training model
            Console.WriteLine("Building and training " + _datasetName + " model...");
            Console.WriteLine();
            return estimator.Fit(splitTrainSet);
        }

        void Evaluate(IDataView splitTestSet) {

            Console.WriteLine("==== Evaluating Model accuracy with Dataset ====");
            IDataView predictions = _model.Transform(splitTestSet);
            CalibratedBinaryClassificationMetrics metrics = _mlContext.BinaryClassification.Evaluate(predictions, "Label");

            Console.WriteLine($"Accuracy: {metrics.Accuracy:P2}");
            Console.WriteLine($"Auc: {metrics.AreaUnderRocCurve:P2}");
            Console.WriteLine($"F1Score: {metrics.F1Score:P2}");
            Console.WriteLine("==== End of model evaluation ====");
            Console.WriteLine();
        }

        TrainTestData LoadData(String datasetPath, bool hasHeader) {

            IDataView dataView = _mlContext.Data.LoadFromTextFile<ClassificationData>(datasetPath, hasHeader: hasHeader);
            // testFraction -> Percentage of phrases compared | Default: 10%
            TrainTestData splitDataView = _mlContext.Data.TrainTestSplit(dataView, testFraction: 0.9);
            return splitDataView;
        }

        void PredictSingleItem(ClassificationData statement) {

            PredictionEngine<ClassificationData, ClassificationPrediction> predictionFunction =
                _mlContext.Model.CreatePredictionEngine<ClassificationData, ClassificationPrediction>(_model);

            var resultprediction = predictionFunction.Predict(statement);

            Console.WriteLine();
            Console.WriteLine("==== Prediction of " + _datasetName + " model ====");
            Console.WriteLine("Query: " + resultprediction.Content
                + " | Prediction (" + _datasetName + "): " + Convert.ToBoolean(resultprediction.Prediction) 
                + " | Probability: " + resultprediction.Probability);

            Console.WriteLine("==== End of Predictions ====");
            Console.WriteLine();
        }
    }
}