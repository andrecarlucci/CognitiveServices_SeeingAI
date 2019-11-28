using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System.IO;
using System.Threading.Tasks;
using XamarinVision.Faces;

namespace XamarinVision
{
    public class FaceService
    {
        private static FaceClient _faceClient = new FaceClient(new ApiKeyServiceClientCredentials(Config.CognitiveServicesKey))
        {
            Endpoint = Config.CognitiveServicesEndpoint
        };

        public async Task<string> GetFaceInfo(Stream image)
        {
            var faceAttributes = new []
            {
                FaceAttributeType.Gender, FaceAttributeType.Age,
                FaceAttributeType.Emotion
            };
            var results = await _faceClient.Face.DetectWithStreamAsync(image, true, false, faceAttributes);
            return results.ToStringMessage();
        }
    }
}