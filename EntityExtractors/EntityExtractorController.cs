using System;
using System.Linq;
using System.Reflection;

namespace Starlight.EntityExtractors {
    public class EntityExtractorController {
        public static void Fetch(Utterance u) {

            String intentName = u.TopScoringIntent.Name;

            intentName = intentName.First().ToString().ToUpper() + intentName.Substring(1);
            Type classType = Type.GetType("Starlight.EntityExtraction." + intentName);

            // The StartsWith("<>") is in there to avoid calling the '<>c__DisplayClass1_...' class from the Debugger, if this happens an 'NullReferenceException' will be thrown
            if (classType != null && classType.Name != "EntityExtractorController" && !classType.Name.StartsWith("<>")) {

                MethodInfo classMethod = classType.GetMethod("Fetch");
                classMethod.Invoke(null, new object[] { u });
            }
        }
    }
}
