using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace XamarinVision.Visions
{
    public class VisionService
    {
        private static ComputerVisionClient _visionClient = new ComputerVisionClient(new ApiKeyServiceClientCredentials(Config.CognitiveServicesKey))
        {
            Endpoint = Config.CognitiveServicesEndpoint
        };

        public async Task<VisionResult> GetImageInfo(Stream image)
        {
            var features = new List<VisualFeatureTypes> {
                VisualFeatureTypes.Description,
                VisualFeatureTypes.Faces
            };
            var result = await _visionClient.AnalyzeImageInStreamAsync(image, features, null, Config.Language);
            return new VisionResult(result);
        }
    }
}
