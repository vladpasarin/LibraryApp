using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using RecommendationSystem.DataModels;

namespace RecommendationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            MLContext context = new MLContext();

            (IDataView trainDataView, IDataView testDataView) = LoadData(context);

            ITransformer model = BuildAndTrainModel(context, trainDataView);

            EvaluateModel(context, testDataView, model);

            var score = GeneratePrediction(context, model);
            foreach (var prediction in score)
            {
                Console.WriteLine("Score:" + prediction.Key + " & AssetID: " + prediction.Value);
            }
            //Console.WriteLine(score);

            SaveModel(context, trainDataView.Schema, model);
        }

        public static (IDataView train, IDataView test) LoadData(MLContext context)
        {
            var trainDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "recommendation-ratings-train.csv");
            var testDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "recommendation-ratings-test.csv");

            IDataView trainDataView = context.Data.LoadFromTextFile<RatingData>(trainDataPath, hasHeader: true, separatorChar: ',');
            IDataView testDataView = context.Data.LoadFromTextFile<RatingData>(testDataPath, hasHeader: true, separatorChar: ',');

            return (trainDataView, testDataView);
        }

        public static ITransformer BuildAndTrainModel(MLContext context, IDataView trainDataView)
        {
            IEstimator<ITransformer> estimator = context.Transforms.Conversion.MapValueToKey(outputColumnName: "userIdEncoded", inputColumnName: "userId")
                .Append(context.Transforms.Conversion.MapValueToKey(outputColumnName: "assetIdEncoded", inputColumnName: "assetId"));

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = "userIdEncoded",
                MatrixRowIndexColumnName = "assetIdEncoded",
                LabelColumnName = "Label",
                NumberOfIterations = 20,
                ApproximationRank = 100
            };

            var trainerEstimator = estimator.Append(context.Recommendation().Trainers.MatrixFactorization(options));

            Console.WriteLine("=============== Training the model ===============");
            ITransformer model = trainerEstimator.Fit(trainDataView);

            return model;
        }

        public static void EvaluateModel(MLContext context, IDataView testDataView, ITransformer model)
        {
            Console.WriteLine("=============== Evaluating the model ===============");
            var prediction = model.Transform(testDataView);

            var metrics = context.Regression.Evaluate(prediction, labelColumnName: "Label", scoreColumnName: "Score");

            Console.WriteLine("Root Mean Squared Error : " + metrics.RootMeanSquaredError.ToString());
            Console.WriteLine("RSquared: " + metrics.RSquared.ToString());
        }

        public static Dictionary<float, int> GeneratePrediction(MLContext context, ITransformer model)
        {
            Console.WriteLine("=============== Making a prediction ===============");
            var predictionEngine = context.Model.CreatePredictionEngine<RatingData, RatingPrediction>(model);

            var userId = 6;

            var testInputs = new List<RatingData>
            {
                new RatingData{userId = userId, assetId = 1},
                new RatingData{userId = userId, assetId = 2},
                new RatingData{userId = userId, assetId = 3},
                new RatingData{userId = userId, assetId = 9},
            };

            var ratingPredictions = new Dictionary<float, int>();

            foreach (var testInput in testInputs)
            {
                var prediction = predictionEngine.Predict(testInput);
                ratingPredictions.Add(prediction.Score, Convert.ToInt32(testInput.assetId));
            }

            //var ratingPrediction = predictionEngine.Predict(testInput);

            //if (Math.Round(ratingPrediction.Score, 1) > 3.5)
            //{
            //    Console.WriteLine("Book with assetId " + testInput.assetId + " is recommended for user " + testInput.userId);
            //}
            //else
            //{
            //    Console.WriteLine("Book with assetId " + testInput.assetId + " is not recommended for user " + testInput.userId);
            //}

            return ratingPredictions;
        }

        public static void SaveModel(MLContext context, DataViewSchema trainData, ITransformer model)
        {
            var modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "BookRecommenderModel.zip");

            Console.WriteLine("=============== Saving the model to a file ===============");
            context.Model.Save(model, trainData, modelPath);
        }
    }
}
