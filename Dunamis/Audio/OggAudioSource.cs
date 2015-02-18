using System.IO;
using System.Collections.Generic;
using OpenTK.Audio.OpenAL;
using NVorbis;

namespace Dunamis.Audio
{
    public class OggAudioSource : AudioSource
    {
        private VorbisReader _reader;
        private ALFormat _format;
        private short[] _data;
        private int _sampleRate;
        private Stream _fileStream;

        public OggAudioSource(string file, bool streaming = true)
            : this(File.Open(file, FileMode.Open), streaming)
        {

        }

        public OggAudioSource(Stream file, bool streaming = true)
        {
            Streaming = streaming;
            _fileStream = file;

            _reader = new VorbisReader(file, false);
            _sampleRate = _reader.SampleRate;
            _format = (_reader.Channels == 2 ? ALFormat.Stereo16 : ALFormat.Mono16);
            
            if(!Streaming)
            {
                _data = new short[_reader.TotalSamples];
                float[] samples = new float[_reader.TotalSamples];
                _reader.ReadSamples(samples, 0, (int)_reader.TotalSamples);
                CastSamples(ref samples, ref _data, samples.Length);
            }
        }

        public override IEnumerable<uint> GenBuffers()
        {
            while (State == AudioSourceState.Playing)
                yield return FillBuffer((uint)AL.GenBuffer());
        }

        public override uint FillBuffer(uint buffer)
        {
            if(Streaming)
            {
                float[] samples = new float[16384];
                int samplesRead = _reader.ReadSamples(samples, 0, samples.Length);
                if (samplesRead < 16384)
                    State = AudioSourceState.Ended;
                short[] tempData = new short[samplesRead];
                CastSamples(ref samples, ref tempData, samplesRead);
                AL.BufferData((int)buffer, _format, tempData, tempData.Length * sizeof(short), _sampleRate);
            }
            else
            {
                AL.BufferData((int)buffer, _format, _data, _data.Length * sizeof(short), _sampleRate);
                State = AudioSourceState.Ended;
            }

            return buffer;
        }

        public override void Reset()
        {
            _fileStream.Position = 0;
            _reader = new VorbisReader(_fileStream, false);   
        }

        //https://github.com/renaudbedard/nvorbis/blob/a85499d6ba8fcbc32cb01cfcf4c1dfe0da2f15c5/OggStream/OggStream.cs#L447
        private void CastSamples(ref float[] samples, ref short[] outBuffer, int length)
        {
            for(int i = 0; i < length; i++)
            {
                int temp = (int)(32767f * samples[i]);
                if (temp > short.MaxValue) temp = short.MaxValue;
                else if (temp < short.MinValue) temp = short.MinValue;
                outBuffer[i] = (short)temp;
            }
        }
    }
}
