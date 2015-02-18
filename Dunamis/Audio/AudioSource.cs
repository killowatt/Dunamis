using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunamis.Audio
{
    public abstract class AudioSource
    {
        public bool Streaming = true;
        public AudioSourceState State = AudioSourceState.Playing;

        public abstract IEnumerable<uint> GenBuffers();
        public abstract uint FillBuffer(uint buffer);
        public abstract void Reset();
    }

    public enum AudioSourceState
    {
        Playing,
        Ended,
        Error
    }
}
