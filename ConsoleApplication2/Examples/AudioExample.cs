using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Dunamis.Audio;

namespace DunamisExamples.Examples
{
    public class AudioExample : BaseExample
    {
        public AudioExample()
        {

        }

        public override void Run()
        {
            AudioContext.Initialize();
            WavAudioSource source = new WavAudioSource("Resources/garbage_day.wav");
            AudioClip clip = new AudioClip(source);
            AudioEmitter emitter = new AudioEmitter();
            emitter.AddClip(clip);
            clip.Play();
            while (clip.State != AudioClipState.Ended)
            {
                emitter.Process();
            }
            AudioContext.Destroy();
        }
    }
}
