using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Linq;

namespace CWX_AlarmChanger
{
    public class AlarmChangerScript : MonoBehaviour
    {
        private List<AudioClip> _clips = new List<AudioClip>();

        public void Init()
        {
            LoadAudioFilePaths();
            SetSounds();
        }

        public void SetSounds()
        {
            if (_clips.Count <= 0) return;

            var subscribers = FindObjectsOfType<InteractiveSubscriber>().Where(x => x.name.Contains("Siren_")).ToList();

            foreach (var sub in subscribers)
            {
                sub.Sounds[0].Clip = _clips[UnityEngine.Random.Range(0, _clips.Count)];
            }
        }

        private void LoadAudioFilePaths()
        {
            var AudioFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "/BepInEx/plugins/CWX/Sounds/");

            foreach (var File in AudioFiles)
            {
                LoadAudioClip(File);
            }
        }

        private async void LoadAudioClip(string path)
        {
            var audioClip = await RequestAudioClip(path);

            _clips.Add(audioClip);
        }

        private async Task<AudioClip> RequestAudioClip(string path)
        {

            UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV);

            var sendWeb = www.SendWebRequest();

            while (!sendWeb.isDone)
            {
                await Task.Yield();
            }

            if (www.isNetworkError || www.isHttpError)
            {
                return null;
            }

            AudioClip audioclip = DownloadHandlerAudioClip.GetContent(www);

            return audioclip;
        }
    }
}