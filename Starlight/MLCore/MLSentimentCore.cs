using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;
using static Microsoft.ML.DataOperationsCatalog;

namespace Starlight.MLCore {
    class MLSentimentCore {


        public static void Cognize(string query, string sentimentName) {

            string dataPath = Path.Combine(Environment.CurrentDirectory, "Dataset", sentimentName + ".txt");

            MLContext _mlContext = new MLContext();
            MLSentimentData statement = new MLSentimentData {
                SentimentText = query
            };

            TrainTestData splitDataView = LoadData(_mlContext, dataPath);
            ITransformer model = BuildAndTrainModel(_mlContext, splitDataView.TrainSet);
            Evaluate(_mlContext, model, splitDataView.TestSet);
            UseModelWithSingleItem(statement, _mlContext, model);
        }

        /*
         * Extracts and transforms the data
         * Trains the model
         * Predicts sentiment based on test data
         * Returns the model
         */
        private static ITransformer BuildAndTrainModel(MLContext mlContext, IDataView splitTrainSet) {
            // converts the text column into a numeric key type column used by the machine learning algorithm and adds it as a new dataset column:
            var estimator = mlContext.Transforms.Text.FeaturizeText(outputColumnName: "Features", inputColumnName: nameof(MLSentimentData.SentimentText))
                .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Label", featureColumnName: "Features"));

            Console.WriteLine("==== Create and Train the Model ====");
            var model = estimator.Fit(splitTrainSet);
            Console.WriteLine("==== End of training ====");
            Console.WriteLine();

            return model;
        }

        /* Loads the test dataset
         * Creates the BinaryClassification evaluator
         * Evaluates the model and creates metrics
         * Displays the metrics
         */
        private static void Evaluate(MLContext mlContext, ITransformer model, IDataView splitTestSet) {

            Console.WriteLine("==== Evaluating Model accuracy with Test data ====");
            IDataView predictions = model.Transform(splitTestSet);
            CalibratedBinaryClassificationMetrics metrics = mlContext.BinaryClassification.Evaluate(predictions, "Label");

            Console.WriteLine();
            Console.WriteLine("Model quality metrics evaluation");
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Accuracy: {metrics.Accuracy:P2}");
            Console.WriteLine($"Auc: {metrics.AreaUnderRocCurve:P2}");
            Console.WriteLine($"F1Score: {metrics.F1Score:P2}");
            Console.WriteLine("==== End of model evaluation ====");
        }

        /* Creates a single comment of test data.
         * Predicts sentiment based on test data.
         * Combines test data and predictions for reporting.
         * Displays the predicted results.
         */
        private static void UseModelWithSingleItem(MLSentimentData statement, MLContext mlContext, ITransformer model) {

            PredictionEngine<MLSentimentData, MLSentimentPrediction> predictionFunction = mlContext.Model.CreatePredictionEngine<MLSentimentData, MLSentimentPrediction>(model);

            

            var resultprediction = predictionFunction.Predict(statement);

            Console.WriteLine();
            Console.WriteLine("==== Prediction Test of model with a single sample and test dataset ====");

            Console.WriteLine();
            Console.WriteLine($"Sentiment: {resultprediction.SentimentText} | Prediction: {(Convert.ToBoolean(resultprediction.Prediction) ? "Positive" : "Negative")} | Probability: {resultprediction.Probability} ");

            Console.WriteLine("==== End of Predictions ====");
            Console.WriteLine();
        }


        /* Loads the data.
        * Splits the loaded dataset into train and test datasets.
        * Returns the split train and test datasets.*/
        private static TrainTestData LoadData(MLContext mlContext, string dataPath) {
            IDataView dataView = mlContext.Data.LoadFromTextFile<MLSentimentData>(dataPath, hasHeader: false);
            // testFraction -> Percentage of phrases compared | Default: 10%
            TrainTestData splitDataView = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.9);
            return splitDataView;
        }
    }
}
