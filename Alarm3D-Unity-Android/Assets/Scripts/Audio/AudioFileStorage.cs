using System;
using System.IO;
using UnityEngine;

namespace Alarm3D.Audio
{
    [DisallowMultipleComponent]
    public sealed class AudioFileStorage : MonoBehaviour
    {
        public static AudioFileStorage Instance { get; private set; }

        private const string AudioFolderName = "AlarmVoices";

        public string AudioDirectory =>
            Path.Combine(Application.persistentDataPath, AudioFolderName);

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            EnsureAudioDirectory();
        }

        public string SaveFilePath(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException(
                    "File name cannot be empty.",
                    nameof(fileName));

            string safeFileName = Path.GetFileName(fileName);

            if (string.IsNullOrWhiteSpace(safeFileName))
                throw new ArgumentException(
                    "Invalid file name.",
                    nameof(fileName));

            EnsureAudioDirectory();

            return Path.Combine(
                AudioDirectory,
                safeFileName);
        }

        public bool DeleteFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            string path;

            try
            {
                path = SaveFilePath(fileName);
            }
            catch
            {
                return false;
            }

            if (!File.Exists(path))
                return false;

            try
            {
                File.Delete(path);
                return true;
            }
            catch (Exception exception)
            {
                Debug.LogError(
                    $"Failed to delete audio file: {exception.Message}");

                return false;
            }
        }

        public bool FileExists(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            try
            {
                return File.Exists(SaveFilePath(fileName));
            }
            catch
            {
                return false;
            }
        }

        private void EnsureAudioDirectory()
        {
            try
            {
                if (!Directory.Exists(AudioDirectory))
                {
                    Directory.CreateDirectory(AudioDirectory);
                }
            }
            catch (Exception exception)
            {
                Debug.LogError(
                    $"Failed to create audio directory: {exception.Message}");
            }
        }
    }
}
