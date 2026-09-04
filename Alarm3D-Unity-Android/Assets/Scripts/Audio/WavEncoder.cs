using System;
using System.IO;
using UnityEngine;

namespace Alarm3D.Audio
{
    public static class WavEncoder
    {
        public static bool Save(AudioClip clip, string filePath)
        {
            if (clip == null || string.IsNullOrWhiteSpace(filePath))
                return false;

            try
            {
                string directory = Path.GetDirectoryName(filePath);

                if (!string.IsNullOrEmpty(directory) &&
                    !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                float[] samples = new float[clip.samples * clip.channels];
                clip.GetData(samples, 0);

                using (FileStream stream =
                       new FileStream(filePath, FileMode.Create))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    int sampleCount = samples.Length;
                    int channels = clip.channels;
                    int sampleRate = clip.frequency;

                    writer.Write(new[] { 'R', 'I', 'F', 'F' });
                    writer.Write(36 + sampleCount * 2);
                    writer.Write(new[] { 'W', 'A', 'V', 'E' });

                    writer.Write(new[] { 'f', 'm', 't', ' ' });
                    writer.Write(16);
                    writer.Write((short)1);
                    writer.Write((short)channels);
                    writer.Write(sampleRate);

                    int byteRate = sampleRate * channels * 2;
                    writer.Write(byteRate);
                    writer.Write((short)(channels * 2));
                    writer.Write((short)16);

                    writer.Write(new[] { 'd', 'a', 't', 'a' });
                    writer.Write(sampleCount * 2);

                    for (int i = 0; i < samples.Length; i++)
                    {
                        float sample = Mathf.Clamp(samples[i], -1f, 1f);
                        short value = (short)(sample * short.MaxValue);
                        writer.Write(value);
                    }
                }

                return true;
            }
            catch (Exception exception)
            {
                Debug.LogError(
                    $"Failed to save WAV file: {exception.Message}");

                return false;
            }
        }
    }
}
