using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using XamarinVision.Faces;
using Xunit;

namespace XamarinVision.Tests
{
    public class EmotionExtensionsTests
    {
        [Fact]
        public void Should_get_most_probable_emotion()
        {
            var emotion = new Emotion(1, 2, 3, 4, 5, 6, 7, 8);
            Assert.Equal("Surprise", emotion.GetMostProbable());
        }
    }
}
