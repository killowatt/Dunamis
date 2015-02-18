using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenTK.Audio.OpenAL;

namespace Dunamis.Audio
{
    public class WavAudioSource : AudioSource
    {
        private int _formatChunkSize;
        private short _audioFormat;
        private short _numChannels;
        private int _sampleRate;
        private int _byteRate;
        private int _dataChunkSize;
        private short _blockAlign;
        private short _bitsPerSample;
        private Stream _fileStream;
        private BinaryReader _reader;
        private ALFormat _alFormat;
        private int _currentBuffer = 0;
        private byte[] _data;
        private int _dataStart = 0;

        public WavAudioSource(string file, bool streaming = true)
            : this(File.Open(file, FileMode.Open), streaming)
        {

        }

        public WavAudioSource(Stream file, bool streaming = true)
        {
            Streaming = streaming;

            _fileStream = file;
            _reader = new BinaryReader(file);

            if (new string(_reader.ReadChars(4)) != "RIFF")
                throw new InvalidDataException("Specified stream is not a wave file.");

            int chunkSize = _reader.ReadInt32();

            if (new string(_reader.ReadChars(4)) != "WAVE")
                throw new InvalidDataException("Specified stream is not a wave file");
            if (new string(_reader.ReadChars(4)) != "fmt ")
                throw new NotSupportedException("Specified wave format is not supported.");

            _formatChunkSize = _reader.ReadInt32();
            _audioFormat = _reader.ReadInt16();
            if (_audioFormat != 1)
                throw new NotSupportedException("Compressed WAV files are not supported.");

            _numChannels = _reader.ReadInt16();
            _sampleRate = _reader.ReadInt32();
            _byteRate = _reader.ReadInt32();
            _blockAlign = _reader.ReadInt16();
            _bitsPerSample = _reader.ReadInt16();

            if (new string(_reader.ReadChars(4)) != "data")
                throw new InvalidDataException("Specified wave format is not supported.");

            _dataChunkSize = _reader.ReadInt32();

            if(_numChannels == 2)
            {
                if (_bitsPerSample == 8)
                    _alFormat = ALFormat.Stereo8;
                else
                    _alFormat = ALFormat.Stereo16;
            }
            else
            {
                if (_bitsPerSample == 8)
                    _alFormat = ALFormat.Mono8;
                else
                    _alFormat = ALFormat.Mono16;
            }

            _dataStart = (int)_reader.BaseStream.Position;

            if (!Streaming)
                _data = _reader.ReadBytes((int)_reader.BaseStream.Length);
        }

        public override IEnumerable<uint> GenBuffers()
        {
            while (State == AudioSourceState.Playing)
                yield return FillBuffer((uint)AL.GenBuffer());
        }

        public override uint FillBuffer(uint buffer)
        {
            if (Streaming)
            {
                byte[] tempData = _reader.ReadBytes(_byteRate);
                if (tempData.Length < _byteRate)
                    State = AudioSourceState.Ended;
                AL.BufferData((int)buffer, _alFormat, tempData, tempData.Length, _sampleRate);
            }
            else
            {
                AL.BufferData((int)buffer, _alFormat, _data, _data.Length, _sampleRate);
                State = AudioSourceState.Ended;
            }

            return buffer;
        }

        public override void Reset()
        {
            if (Streaming)
                _reader.BaseStream.Position = _dataStart;
            State = AudioSourceState.Playing;
        }
    }
}
