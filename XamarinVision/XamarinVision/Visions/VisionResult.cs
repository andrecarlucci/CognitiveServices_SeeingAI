using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Linq;

namespace XamarinVision.Visions
{
    public class VisionResult
    {
        public string Description { get; set; }
        public bool HasFaces { get; set; }

        public VisionResult(ImageAnalysis imageAnalysis)
        {
            Description = imageAnalysis.ToStringMessage();
            HasFaces = imageAnalysis.Faces?.Any() ?? false;
        }
    }
}
