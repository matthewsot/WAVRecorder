using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;

namespace WAVRecorder.UWP
{
    public class WAVRecorder : IWAVRecorder
    {
        public IDisposable RawWAVStream { get; private set; }

        public Stream WAVStream {
            get
            {
                return ((InMemoryRandomAccessStream)RawWAVStream).AsStream();
            }
        }

        private MediaCapture mediaCapture;
        private MediaEncodingProfile mediaProfile;

        public async Task<bool> RequestPermissionsAndInitAsync()
        {
            mediaCapture = new MediaCapture();

            var settings = new MediaCaptureInitializationSettings()
            {
                StreamingCaptureMode = StreamingCaptureMode.Audio
            };
            await mediaCapture.InitializeAsync();

            mediaProfile = MediaEncodingProfile.CreateWav(AudioEncodingQuality.Auto);

            RawWAVStream = new InMemoryRandomAccessStream();

            return true;
        }

        public async Task StartRecordingAsync()
        {
            await mediaCapture.StartRecordToStreamAsync(mediaProfile, (InMemoryRandomAccessStream)RawWAVStream);
        }

        public async Task StopRecordingAsync()
        {
            await mediaCapture.StopRecordAsync();
        }
    }
}
