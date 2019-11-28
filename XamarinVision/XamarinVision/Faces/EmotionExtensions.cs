using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System.Collections;

namespace XamarinVision.Faces
{
    public static class EmotionExtensions
    {
        public static string GetMostProbable(this Emotion emotion)
        {
            var max = 0d;
            var name = "";
            var props = emotion.GetType().GetProperties();
            foreach (var p in props)
            {
                var value = (double)p.GetValue(emotion, null);
                if (value > max)
                {
                    max = value;
                    name = p.GetMethod.Name.Substring(4);
                }
            }
            return name;
        }
    }
}
