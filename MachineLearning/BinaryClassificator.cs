using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;
using static Microsoft.ML.DataOperationsCatalog;

namespace Starlight.MachineLearning {
    class BinaryClassificator /*: IClassificator*/ {

        MLContext _mlContext;
        ITransformer _model;
        readonly string _datasetName;
        IDataView _dataView;

        public BinaryClassificator(string datasetName, bool hasHeader, bool debug = false) {

            if (debug)
            Console.WriteLine("------------- Building " + datasetName + " Dataset Object ------------");
            _mlContext = new MLContext();
            _datasetName = datasetName;
            string datasetPath = Path.Combine(Environment.CurrentDirectory, "Dataset", _datasetName + ".txt");
            TrainTestData splitDataView = LoadData(datasetPath, hasHeader);

            _model = ModelPersistenceIO.LoadModel(_mlContext, datasetName, _dataView.Schema);

            // Disabling cache for now
            if (_model == null/*true*/) {
                
                _model = BuildAndTrainModel(splitDataView.TrainSet, debug);
                ModelPersistenceIO.SaveModel(_mlContext, datasetName, _model, _dataView.Schema);
            }
            else {
                if (debug)
                    Console.WriteLine("Model Cache found! Opening...");
            }

            Evaluate(splitDataView.TestSet, debug);
            if (debug)
                Console.WriteLine("----------------------------------------------------\n");
        }

        public BinaryClassificator(string datasetName) : this(datasetName, false) {
            
        }

        public Intent Classify(string query, bool debug = false) {

            
            ClassificationData statement = new ClassificationData {
                Content = query
            };
            return PredictSingleItem(statement, debug);
        }

        ITransformer BuildAndTrainModel(IDataView splitTrainSet, bool debug = false) {

            // converts the text column into a numeric key type column used by the machine learning algorithm and adds it as a new dataset column:
            var estimator = _mlContext.Transforms.Text.FeaturizeText(outputColumnName: "Features", inputColumnName: nameof(ClassificationData.Content))
                .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Label", featureColumnName: "Features"));

            // Training model
            if (debug)
                Console.WriteLine("Building and training " + _datasetName + " model...");
            return estimator.Fit(splitTrainSet);
        }

        void Evaluate(IDataView splitTestSet, bool debug = false) {

            if (debug)
                Console.WriteLine("Evaluating Model accuracy with Dataset");
            IDataView predictions = _model.Transform(splitTestSet);
            CalibratedBinaryClassificationMetrics metrics = _mlContext.BinaryClassification.Evaluate(predictions, "Label");

            if (debug) {
                Console.WriteLine($"Accuracy: {metrics.Accuracy:P2}");
                Console.WriteLine($"Auc: {metrics.AreaUnderRocCurve:P2}");
                Console.WriteLine($"F1Score: {metrics.F1Score:P2}");
            }
        }

        TrainTestData LoadData(String datasetPath, bool hasHeader) {

            _dataView = _mlContext.Data.LoadFromTextFile<ClassificationData>(datasetPath, hasHeader: hasHeader);
            // testFraction -> Percentage of phrases compared | Default: 10%
            TrainTestData splitDataView = _mlContext.Data.TrainTestSplit(_dataView, testFraction: 0.9);
            return splitDataView;
        }

        Intent PredictSingleItem(ClassificationData statement, bool debug = false) {

            PredictionEngine<ClassificationData, ClassificationPrediction> predictionFunction =
                _mlContext.Model.CreatePredictionEngine<ClassificationData, ClassificationPrediction>(_model);
            

            var resultprediction = predictionFunction.Predict(statement);

            Intent intent = new Intent();
            intent.Name = _datasetName;
            intent.Score = resultprediction.Probability;

            if (debug) {
                Console.WriteLine("-------- Prediction of " + _datasetName + " model --------");
                Console.WriteLine("Query: " + resultprediction.Content
                    + " | Prediction (" + _datasetName + "): " + Convert.ToBoolean(resultprediction.Prediction)
                    + " | Probability: " + resultprediction.Probability);

                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine();
            }

            return intent;
        }
    }
}
