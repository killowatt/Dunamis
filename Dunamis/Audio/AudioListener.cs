using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Audio.OpenAL;

namespace Dunamis.Audio
{
    public class AudioListener
    {
        public Vector3 Position = Vector3.Zero;
        public Vector3 Velocity = Vector3.Zero;
        public Vector3 OrientationAt = new Vector3(0f, 0f, -1f);
        public Vector3 OrientationUp = new Vector3(0f, 1f, 0f);
        public float EfxMetersPerUnit = 1.0f;
        public float Gain = 1.0f;

        public AudioListener()
        {
        }

        public void Use()
        {
            AL.Listener(ALListener3f.Position, Position.X, Position.Y, Position.Z);
            AL.Listener(ALListener3f.Velocity, Velocity.X, Velocity.Y, Velocity.Z);
            float[] orientation = new float[] { OrientationAt.X, OrientationAt.Y, OrientationAt.Z, OrientationUp.X, OrientationUp.Y, OrientationUp.Z };
            AL.Listener(ALListenerfv.Orientation, ref orientation);
            AL.Listener(ALListenerf.EfxMetersPerUnit, EfxMetersPerUnit);
            AL.Listener(ALListenerf.Gain, Gain);
        }
    }
}
