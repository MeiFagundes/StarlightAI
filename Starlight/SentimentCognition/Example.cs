using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;
using static Microsoft.ML.DataOperationsCatalog;

namespace Starlight.SentimentCognition
{
    /// <summary> ML Sentiment implementation example
    /// <author> Mei Fagundes
    class Example
    {

        static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "example.txt");
        static readonly string _modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "testModel.zip");

        public static void MLMain()
        {
            MLContext mlContext = new MLContext();
            TrainTestData splitDataView = LoadData(mlContext);
            ITransformer model = BuildAndTrainModel(mlContext, splitDataView.TrainSet);
            Evaluate(mlContext, model, splitDataView.TestSet);
            UseModelWithSingleItem(mlContext, model);
        }

        /*
         * Extracts and transforms the data
         * Trains the model
         * Predicts sentiment based on test data
         * Returns the model
         */
        public static ITransformer BuildAndTrainModel(MLContext mlContext, IDataView splitTrainSet)
        {
            // converts the text column into a numeric key type column used by the machine learning algorithm and adds it as a new dataset column:
            var estimator = mlContext.Transforms.Text.FeaturizeText(outputColumnName: "Features", inputColumnName: nameof(MLData.SentimentText))
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
        public static void Evaluate(MLContext mlContext, ITransformer model, IDataView splitTestSet)
        {

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
        private static void UseModelWithSingleItem(MLContext mlContext, ITransformer model)
        {

            PredictionEngine<MLData, MLPrediction> predictionFunction = mlContext.Model.CreatePredictionEngine<MLData, MLPrediction>(model);

            MLData sampleStatement = new MLData
            {
                SentimentText = "This was a very bad steak"
            };

            var resultprediction = predictionFunction.Predict(sampleStatement);

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
        public static TrainTestData LoadData(MLContext mlContext)
        {
            IDataView dataView = mlContext.Data.LoadFromTextFile<MLData>(_dataPath, hasHeader: false);
            // testFraction -> Percentage of phrases compared | Default: 10%
            TrainTestData splitDataView = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.9);
            return splitDataView;
        }
    }

    public class MLData
    {
        [LoadColumn(0)]
        public string SentimentText;

        [LoadColumn(1), ColumnName("Label")]
        public bool Sentiment;
    }

    public class MLPrediction : MLData
    {

        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }

        public float Probability { get; set; }

        public float Score { get; set; }
    }
}
