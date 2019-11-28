using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinVision.Speech;
using XamarinVision.Visions;

namespace XamarinVision
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        //image recognition language
        //public static string Language = "pt";

        //https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/language-support
        //public static string SpeechLanguage = "pt-BR";

        private byte[] _imageInBytes;
        private MemoryStream GetImageAsStream() => new MemoryStream(_imageInBytes);
        private const string Describing = "Describing...";

        public FaceService FaceService { get; set; } = new FaceService();
        public VisionService VisionService { get; set; } = new VisionService();
        public SpeechService SpeechService { get; set; } = new SpeechService();

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand TakeAPictureCommand => new Command(async () => {
            try
            {
                await SeeLikeABoss();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        });

        private ImageSource _imageSource;

        public ImageSource CurrentImageSource {
            get => _imageSource;
            set {
                _imageSource = value;
                RaisePropertyChanged(nameof(CurrentImageSource));
            }
        }

        private string _description = "I see you!";

        public string Description { 
            get => _description; 
            set {
                _description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        public async Task SeeLikeABoss()
        {
            await TakeAPicture();
            SetBackground();
            await DescribePicture();
            await SayItLoud();
        }

        public async Task TakeAPicture()
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    SaveToAlbum = false,
                    PhotoSize = PhotoSize.Medium
                }
            );

            if (file == null)
            {
                return;
            }
            var mem = new MemoryStream();
            await file.GetStream().CopyToAsync(mem);
            file.Dispose();
            _imageInBytes = mem.ToArray();
        }

        private void SetBackground()
        {
            CurrentImageSource = ImageSource.FromStream(() => GetImageAsStream());
        }

        private async Task DescribePicture()
        {
            Description = Describing;
            var imageResult = await VisionService.GetImageInfo(GetImageAsStream());
            Description += Environment.NewLine + imageResult.Description;

            if(imageResult.HasFaces)
            {
                Description += Environment.NewLine + await FaceService.GetFaceInfo(GetImageAsStream());
            }
        }

        private async Task SayItLoud()
        {
            await SpeechService.SpeakTextAsync(Description.Replace(Describing, ""));
        }


        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
