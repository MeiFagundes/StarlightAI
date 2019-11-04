using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Starlight.MachineLearning {
    public class ModelPersistenceIO {

        const String _MODEL_PATH = "\\Model\\";

        public static void SaveModel(MLContext mlContext, String modelName, ITransformer model, DataViewSchema dataViewSchema) {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + _MODEL_PATH);
            mlContext.Model.Save(model, dataViewSchema, Directory.GetCurrentDirectory() + _MODEL_PATH + modelName + ".zip");
        }

        public static ITransformer LoadModel(MLContext mlContext, String modelName, DataViewSchema dataViewSchema) {
            
            string modelPath = Directory.GetCurrentDirectory() + _MODEL_PATH + modelName + ".zip";

            if (File.Exists(modelPath))
                return mlContext.Model.Load(modelPath, out dataViewSchema);
            else
                return null;
        }
    }
}
