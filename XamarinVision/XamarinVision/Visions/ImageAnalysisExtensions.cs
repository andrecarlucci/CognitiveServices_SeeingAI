using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Linq;

namespace XamarinVision.Visions
{
    public static class ImageAnalysisExtensions
    {
        public static string ToStringMessage(this ImageAnalysis imageAnalysis)
        {
            return string.Join(", ", imageAnalysis.Description.Captions.Select(c => c.Text));
        }
    }
}
