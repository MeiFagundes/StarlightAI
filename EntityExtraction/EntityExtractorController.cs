using System;
using System.Linq;
using System.Reflection;

namespace Starlight.EntityExtraction {
    public class EntityExtractorController {
        public static void Fetch(Utterance u) {

            Intent intent = u.TopScoringIntent;

            intent.Name = intent.Name.First().ToString().ToUpper() + intent.Name.Substring(1);
            Type classType = Type.GetType("Starlight.EntityExtraction." + intent.Name);

            // The StartsWith("<>") is in there to avoid calling the '<>c__DisplayClass1_...' class from the Debugger, if this happens an 'NullReferenceException' will be thrown
            if (classType != null && classType.Name != "EntityExtractorController" && !classType.Name.StartsWith("<>")) {

                MethodInfo classMethod = classType.GetMethod("Fetch");
                classMethod.Invoke(null, new object[] { u });
            }
        }
    }
}
