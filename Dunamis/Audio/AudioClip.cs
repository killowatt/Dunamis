using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Audio.OpenAL;

namespace Dunamis.Audio
{
    public class AudioClip
    {
        private uint _alSource;
        private AudioSource _source;
        private AudioClipState _state = AudioClipState.Playing;

        public AudioClipState State => _state;
        public AudioSource Source => _source;
        public uint DeviceSource => _alSource;
        public bool Looping = false;

        public AudioClip(AudioSource source)
        {
            _source = source;
            AL.GenSource(out _alSource);
            RequeueBuffers();
        }

        public void Play()
        {
            AL.SourcePlay(_alSource);
            _state = AudioClipState.Playing;
        }

        public void Pause()
        {
            AL.SourcePause(_alSource);
            _state = AudioClipState.Paused;
        }

        private void RequeueBuffers()
        {
            if (_source.Streaming)
            {
                List<uint> buffers = new List<uint>();
                for (int i = 0; i < 4 && _source.State == AudioSourceState.Playing; i++)
                    buffers.Add(_source.GenBuffers().First());
                AL.SourceQueueBuffers(_alSource, buffers.Count, buffers.ToArray());
            }
            else
                AL.SourceQueueBuffer((int)_alSource, (int)_source.GenBuffers().First());
        }

        internal void Process()
        {
            if (_state == AudioClipState.Playing)
            {
                if (_source.State == AudioSourceState.Ended && AL.GetSourceState(_alSource) == ALSourceState.Stopped)
                {
                    AL.SourceStop(_alSource);
                    _state = AudioClipState.Ended;
                    if (Looping)
                    {
                        _state = AudioClipState.Playing;
                        int bufferCount = 0;
                        AL.GetSource(_alSource, ALGetSourcei.BuffersQueued, out bufferCount);
                        AL.SourceUnqueueBuffers((int)_alSource, bufferCount);
                        _source.Reset();
                        RequeueBuffers();
                        AL.SourcePlay(_alSource);
                    }
                }
                int buffersProcessed = 0;
                AL.GetSource(_alSource, ALGetSourcei.BuffersProcessed, out buffersProcessed);
                while (buffersProcessed > 0)
                {
                    uint buffer = 0;
                    AL.SourceUnqueueBuffers(_alSource, 1, ref buffer);
                    if (_source.State == AudioSourceState.Ended) break;
                    _source.FillBuffer(buffer);
                    AL.SourceQueueBuffer((int)_alSource, (int)buffer);
                    buffersProcessed--;
                }
            }
        }
    }

    public enum AudioClipState
    {
        Stopped,
        Paused,
        Playing,
        Ended
    }
}
