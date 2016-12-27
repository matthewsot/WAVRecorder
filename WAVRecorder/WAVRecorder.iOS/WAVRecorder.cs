using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAVRecorder.iOS
{
    public class WAVRecorder : IWAVRecorder
    {
        public IDisposable RawWAVStream => throw new NotImplementedException();

        public Stream WAVStream => throw new NotImplementedException();

        public Task<bool> RequestPermissionsAndInitAsync()
        {
            throw new NotImplementedException();
        }

        public Task StartRecordingAsync()
        {
            throw new NotImplementedException();
        }

        public Task StopRecordingAsync()
        {
            throw new NotImplementedException();
        }
    }
}
