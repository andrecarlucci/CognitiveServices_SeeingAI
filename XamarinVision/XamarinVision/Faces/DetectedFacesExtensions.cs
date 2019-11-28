using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XamarinVision.Faces
{
    public static class DetectedFacesExtensions
    {
        public static string ToStringMessage(this IList<DetectedFace> detectedFaces)
        {
            if (!detectedFaces.Any())
            {
                return "";
            }
            var sb = new StringBuilder();
            for (var i = 0; i < detectedFaces.Count; i++)
            {
                var p = detectedFaces[i];
                if(p.FaceAttributes != null)
                {
                    sb.AppendLine($"Person {i+1}. ");   
                }
                var att = p.FaceAttributes;
                if(att.Gender != null)
                {
                    sb.Append($"Seems like {att.Gender}, ");
                }
                if(att.Emotion != null)
                {
                    sb.Append($"displayed emotion: {att.Emotion.GetMostProbable()}, ");
                }
                if (att.Age!= null)
                {
                    sb.Append($"age around {att.Age}, ");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}