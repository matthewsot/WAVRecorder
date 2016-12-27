using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAVRecorder
{
    public enum WAVRecorderStatus
    {
        Recording,
        Stopped
    }
    public interface IWAVRecorder
    {
        //WAVRecorderStatus Status { get; }

        Task<bool> RequestPermissionsAndInitAsync();

        Stream WAVStream { get; }

        IDisposable RawWAVStream { get; }

        Task StartRecordingAsync();

        Task StopRecordingAsync();
    }
}
