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
        public uint DeviceSource;
        public AudioSource Source;
        public AudioClipState State = AudioClipState.Playing;

        public bool Looping = false;

        public AudioClip(AudioSource source)
        {
            Source = source;
            AL.GenSource(out DeviceSource);
            RequeueBuffers();
        }

        public void Play()
        {
            AL.SourcePlay(DeviceSource);
            State = AudioClipState.Playing;
        }

        public void Pause()
        {
            AL.SourcePause(DeviceSource);
            State = AudioClipState.Paused;
        }

        private void RequeueBuffers()
        {
            if (Source.Streaming)
            {
                List<uint> buffers = new List<uint>();
                for (int i = 0; i < 4 && Source.State == AudioSourceState.Playing; i++)
                    buffers.Add(Source.GenBuffers().First());
                AL.SourceQueueBuffers(DeviceSource, buffers.Count, buffers.ToArray());
            }
            else
                AL.SourceQueueBuffer((int)DeviceSource, (int)Source.GenBuffers().First());
        }

        internal void Process()
        {
            if (State == AudioClipState.Playing)
            {
                if (Source.State == AudioSourceState.Ended && AL.GetSourceState(DeviceSource) == ALSourceState.Stopped)
                {
                    AL.SourceStop(DeviceSource);
                    State = AudioClipState.Ended;
                    if (Looping)
                    {
                        State = AudioClipState.Playing;
                        int bufferCount = 0;
                        AL.GetSource(DeviceSource, ALGetSourcei.BuffersQueued, out bufferCount);
                        AL.SourceUnqueueBuffers((int)DeviceSource, bufferCount);
                        Source.Reset();
                        RequeueBuffers();
                        AL.SourcePlay(DeviceSource);
                    }
                }
                int buffersProcessed = 0;
                AL.GetSource(DeviceSource, ALGetSourcei.BuffersProcessed, out buffersProcessed);
                while (buffersProcessed > 0)
                {
                    uint buffer = 0;
                    AL.SourceUnqueueBuffers(DeviceSource, 1, ref buffer);
                    if (Source.State == AudioSourceState.Ended) break;
                    Source.FillBuffer(buffer);
                    AL.SourceQueueBuffer((int)DeviceSource, (int)buffer);
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
