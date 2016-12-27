using Android.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WAVRecorder.Android
{
    public class WAVRecorder : IWAVRecorder
    {
        public IDisposable RawWAVStream { get; private set; }

        public System.IO.Stream WAVStream
        {
            get
            {
                return null;
            }
        }

        private AudioRecord audioRecorder;
        private byte[] audioBuffer;

        //See: https://raw.githubusercontent.com/xamarin/monodroid-samples/master/Example_WorkingWithAudio/Example_WorkingWithAudio/LowLevelRecordAudio.cs
        public async Task<bool> RequestPermissionsAndInitAsync()
        {
            audioBuffer = new Byte[100000];
            audioRecorder = new AudioRecord(
                // Hardware source of recording.
                AudioSource.Mic,
                // Frequency
                16000,
                // Mono or stereo
                ChannelIn.Mono,
                // Audio encoding
                Encoding.Pcm16bit,
                // Length of the audio clip.
                audioBuffer.Length
            );

            return true;
        }

        public async void StartRecordingAsync()
        {
            audioRecorder.StartRecording();
            // Off line this so that we do not block the UI thread.
            await ReadAudioAsync();
        }

        public async void StopRecordingAsync()
        {
        }
    }
}
