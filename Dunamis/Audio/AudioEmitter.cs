using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Audio.OpenAL;
using OpenTK;

namespace Dunamis.Audio
{
    public class AudioEmitter
    {
        private List<AudioClip> _sources;

        public Vector3 Position = Vector3.Zero;
        public Vector3 Velocity = Vector3.Zero;
        public Vector3 Direction = Vector3.Zero;
        public float Rolloff = 0.0f;
        public bool SourceRelative = true;

        public AudioEmitter()
        {
            _sources = new List<AudioClip>();
        }

        public void Process()
        {
            List<AudioClip> _clipsToRemove = new List<AudioClip>();
            foreach(AudioClip clip in _sources)
            {
                AL.Source(clip.DeviceSource, ALSource3f.Position, Position.X, Position.Y, Position.Z);
                AL.Source(clip.DeviceSource, ALSource3f.Velocity, Velocity.X, Velocity.Y, Velocity.Z);
                AL.Source(clip.DeviceSource, ALSource3f.Direction, Direction.X, Direction.Y, Direction.Z);
                AL.Source(clip.DeviceSource, ALSourceb.SourceRelative, SourceRelative);
                AL.Source(clip.DeviceSource, ALSourcef.RolloffFactor, Rolloff);

                clip.Process();
                if (clip.State == AudioClipState.Ended)
                    _clipsToRemove.Add(clip);
            }
            _clipsToRemove.ForEach(x => _sources.Remove(x));
            _clipsToRemove.Clear();
        }

        public void AddClip(AudioClip clip)
        {
            _sources.Add(clip);
        }

        public void RemoveClip(AudioClip clip)
        {
            _sources.Remove(clip);
        }
    }
}
