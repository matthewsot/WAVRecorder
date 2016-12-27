using Android.Media;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace WAVRecorder.Android
{
    public class WAVRecorder : IWAVRecorder
    {
        public IDisposable RawWAVStream {
            get
            {
                return WAVStream;
            }
        }

        public System.IO.Stream WAVStream { get; private set; }

        private AudioRecord audioRecorder;
        private byte[] audioBuffer;

        //See: https://raw.githubusercontent.com/xamarin/monodroid-samples/master/Example_WorkingWithAudio/Example_WorkingWithAudio/LowLevelRecordAudio.cs
        public Task<bool> RequestPermissionsAndInitAsync()
        {
            audioBuffer = new Byte[100000];
            audioRecorder = new AudioRecord(
                AudioSource.Mic,
                16000,
                ChannelIn.Mono,
                Encoding.Pcm16bit,
                audioBuffer.Length
            );

            WAVStream = new MemoryStream();

            //TODO: write the WAV header?

            return new Task<bool>(() => true);
        }

        private bool endRecording;

        private async Task HandleBufferAsync()
        {
            while (true)
            {
                if (endRecording)
                {
                    endRecording = false;
                    break;
                }

                try
                {
                    // Keep reading the buffer while there is audio input.
                    int numBytes = await audioRecorder.ReadAsync(audioBuffer, 0, audioBuffer.Length);
                    //TODO: does this reset the audioRecorder?
                    await WAVStream.WriteAsync(audioBuffer, 0, numBytes);
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine(ex.Message);
                    //break;
                }
            }
        }

        public Task StartRecordingAsync()
        {
            audioRecorder.StartRecording();
            // Off line this so that we do not block the UI thread.
            HandleBufferAsync();

            return new Task(() => { });
        }

        public Task StopRecordingAsync()
        {
            endRecording = true;
            while (endRecording == true)
            {
                Thread.Sleep(500);
            }

            return new Task(() => { });
        }
    }
}
