using Microsoft.CognitiveServices.Speech;
using System;
using System.Threading.Tasks;

namespace XamarinVision.Speech
{
    public class SpeechService : IDisposable
    {
        private readonly SpeechSynthesizer _synthesizer;

        public SpeechService()
        {
            var config = SpeechConfig.FromSubscription(Config.CognitiveServicesKey, Config.CognitiveServicesRegion);
            config.SpeechSynthesisLanguage = Config.SpeechLanguage;
            _synthesizer = new SpeechSynthesizer(config);
        }

        public async Task SpeakTextAsync(string text)
        {
            using (var result = await _synthesizer.SpeakTextAsync(text)){ }
        }
        public void Dispose()
        {
            _synthesizer.Dispose();
        }
    }
}
