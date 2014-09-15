using System;
using System.Collections.Generic;
using System.Linq;

namespace Dunamis.Graphics
{
    //public class Animation
    //{
    //    AnimationChannel[] animationChannels;

    //    float duration;
    //    float tickRate;

    //    public void ReadNodeHeirarchy()
    //    {

    //    }
    //    public Animation(AnimationChannel[] channels, float tickRate, float time, float duration, Matrix4[] bones)
    //    {
    //        animationChannels = channels;

    //        Matrix4 identity = Matrix4.Identity;

    //        float ticksPerSecond = tickRate != 0 ? tickRate : 25.0f;
    //        float timeInTicks = time * ticksPerSecond;
    //        float animationTime = timeInTicks % duration;

    //        for (int i = 0; i < bones.Length; i++)
    //        {
    //           //bones[i] = 
    //        }
    //    }
    //}
    public struct Animation
    {
        public AnimationChannel[] Channels;

        public double TickRate;
        public double Duration;

        public void Update(float time, Dictionary<string, Bone> bones)
        {
            Matrix4 identity = Matrix4.Identity;

            float timeInTicks = time * (float)TickRate;
            float animationTime = timeInTicks % (float)Duration;

            readNodeHierarchy(bones.Values.ToArray<Bone>()[0], bones, animationTime, identity);

        }
        public void readNodeHierarchy(Bone bone, Dictionary<string, Bone> bones, float time, Matrix4 parentTransform)
        {
            string nodeName = bone.Name;

            Matrix4 nodeTransformation = bone.Offset;

            AnimationChannel ch = findNodeAnim(nodeName);

            // BLOC
            Vector3 translation = calculateInterpolatedPosition(time, ch);
            Matrix4 translationMatrix = Matrix4.CreateTranslation(translation);

            nodeTransformation = translationMatrix;
            // BLOC

            Matrix4 globalTransformation = parentTransform * nodeTransformation;

            Bone x = bones.Values.First<Bone>(bonef => bonef.Name == nodeName);
            if (x != bones.Values.Last<Bone>())
            {
                Bone[] b = bones.Values.ToArray<Bone>();
                bones[nodeName].Transformation = b[0].Offset * globalTransformation * bones[nodeName].Offset;
            }

            for (int i = 0; i < bone.Children.Length; i++)
            {
                readNodeHierarchy(bone.Children[i], bones, time, globalTransformation);
            }
        }
        public Vector3 calculateInterpolatedPosition(float time, AnimationChannel channel)
        {
            if (channel.PositionKeys.Length == 1)
            {
                return channel.PositionKeys[0].Value;
            }
            
            uint positionIndex = findPosition(time, channel);
            uint nextPositionIndex = positionIndex + 1;
            float deltaTime = (float)(channel.PositionKeys[nextPositionIndex].Time - channel.PositionKeys[positionIndex].Time);
            float factor = (time - (float)channel.PositionKeys[positionIndex].Time) / deltaTime;
            Vector3 start = channel.PositionKeys[positionIndex].Value;
            Vector3 end = channel.PositionKeys[nextPositionIndex].Value;
            Vector3 delta = end - start;
            return start + factor * delta;
        }
        uint findPosition(float time, AnimationChannel channel)
        {
            for (uint i = 0; i < channel.PositionKeys.Length - 1; i++)
            {
                if (time < (float)channel.PositionKeys[i + 1].Time)
                {
                    return i;
                }
            }
            return 0;
        }
        AnimationChannel findNodeAnim(string nodeName)
        {
            for (uint i = 0; i < Channels.Length; i++)
            {
                AnimationChannel channel = Channels[i];
                if (channel.Name == nodeName)
                {
                    return channel;
                }
            }
            return default(AnimationChannel);
        }

        public Animation(AnimationChannel[] channels, double tickRate, double duration)
        {
            Channels = channels;
            TickRate = tickRate;
            Duration = duration;
        }
    }
    public struct AnimationChannel
    {
        public string Name;
        public VectorKey[] PositionKeys;
        public QuaternionKey[] RotationKeys;
        public VectorKey[] ScalingKeys;
        
        public AnimationChannel(string name, VectorKey[] positionKeys, QuaternionKey[] rotationKeys, VectorKey[] scalingKeys)
        {
            Name = name;
            PositionKeys = positionKeys;
            RotationKeys = rotationKeys;
            ScalingKeys = scalingKeys;
        }
    }
    public struct VectorKey
    {
        public double Time;
        public Vector3 Value;

        public VectorKey(double time, Vector3 value)
        {
            Time = time;
            Value = value;
        }
    }
    public struct QuaternionKey
    {
        public double Time;
        public Vector4 Value;

        public QuaternionKey(double time, Vector4 value)
        {
            Time = time;
            Value = value;
        }
    }
}
