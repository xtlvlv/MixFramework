using System;
using System.Collections.Generic;
using BaseFramework;
using BaseFramework.Core;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ScriptsHotfix
{
    // public static class AudioSourcePath
    // {
    //     public static string world_bgm = "Music/world_bgm";
    // }

    public class AudioManager : Singleton<AudioManager>, ISingletonAwake
    {

        /// <summary>
        /// 背景音乐优先级
        /// </summary>
        private int BackgroundPriority = 0;

        /// <summary>
        /// 单通道音效优先级
        /// </summary>
        private int SinglePriority = 10;

        /// <summary>
        /// 多通道音效优先级
        /// </summary>
        private int MultiplePriority = 20;

        /// <summary>
        /// 世界音效优先级
        /// </summary>
        private int WorldPriority = 1;

        private float backgroundVolume = 1f;
        /// <summary>
        /// 背景音乐音量
        /// </summary>
        public float BackgroundVolume
        {
            get => backgroundVolume;
            set
            {
                backgroundVolume = value;
                backgroundAudio.volume = value;
            }
        }
        /// <summary>
        /// 音效音量
        /// </summary>
        private float soundEffectVolume = 1;
        public float SoundEffectVolume
        {
            get => soundEffectVolume;
            set
            {
                soundEffectVolume = value;
                singleAudio.volume = value;
                foreach (var audio in worldAudio.Values)
                {
                    audio.volume = value;
                }
                foreach (var audio in multipleAudio)
                {
                    audio.volume = value;
                }
            }
        }
        
        /// <summary>
        /// 静音
        /// </summary>
        public bool Mute
        {
            get => isMute;
            set
            {
                isMute = value;
                backgroundAudio.mute = value;
                singleAudio.mute = value;
                foreach (var audio in multipleAudio)
                {
                    audio.mute = value;
                }

                foreach (KeyValuePair<GameObject, AudioSource> audio in worldAudio)
                {
                    audio.Value.mute = value;
                }
            }
        }
        
        private          AudioSource                         backgroundAudio;
        private          AudioSource                         singleAudio;
        private          GameObject                          _multipleObj;
        private readonly List<AudioSource>                   multipleAudio = new List<AudioSource>();
        private readonly Dictionary<GameObject, AudioSource> worldAudio    = new Dictionary<GameObject, AudioSource>();
        private          bool                                isMute;
        private          Transform                           root;

        private Dictionary<string, AudioClip> _clipsCache = new Dictionary<string, AudioClip>();

        #region 通用
        
        public void Awake()
        {
            root            = new GameObject(nameof(AudioManager)).transform;
            backgroundAudio = CreateAudioSource("BackgroundAudio", BackgroundPriority, BackgroundVolume);
            singleAudio     = CreateAudioSource("SingleAudio", SinglePriority, SoundEffectVolume);
            this.CreateMultipleAudioSource("MultipleAudio", MultiplePriority, SoundEffectVolume);
            Object.DontDestroyOnLoad(root);
        }

        public AudioClip GetAddClip(string path)
        {
            if (_clipsCache.TryGetValue(path, out var clip))
            {
                return clip;
            }
            clip = AssetMgr.Load<AudioClip>(path);
            _clipsCache.Add(path, clip);
            return clip;
        }

        #endregion
        
        #region 背景音乐

        /// <summary>
        /// 播放背景音乐
        /// </summary>
        public void PlayMusic(string clipName, bool isLoop = true, float speed = 1)
        {
            var clip = GetAddClip(clipName);
            if (backgroundAudio.isPlaying)
            {
                backgroundAudio.Stop();
            }

            backgroundAudio.clip = clip;
            backgroundAudio.loop = isLoop;
            backgroundAudio.pitch = speed;
            backgroundAudio.spatialBlend = 0;
            backgroundAudio.Play();
        }

        /// <summary>
        /// 暂停播放背景音乐
        /// </summary>
        public void PlayMusic(bool isGradual = true)
        {
            if (!backgroundAudio.isPlaying) return;
            // if (isGradual)
            // {
            //     _backgroundAudio.DOFade(0, 2);
            // }
            // else
            {
                backgroundAudio.volume = 0;
            }
        }

        /// <summary>
        /// 恢复播放背景音乐
        /// </summary>
        public void UnPauseMusic(bool isGradual = true)
        {
            if (!backgroundAudio.isPlaying) return;
            // if (isGradual)
            // {
            //     _backgroundAudio.DOFade(BackgroundVolume, 2);
            // }
            // else
            {
                backgroundAudio.volume = BackgroundVolume;
            }
        }

        /// <summary>
        /// 停止播放背景音乐
        /// </summary>
        public void StopMusic()
        {
            if (backgroundAudio.isPlaying)
            {
                backgroundAudio.Stop();
            }
        }

        public void ChangeMusicVolume(float volume)
        {
            BackgroundVolume = Mathf.Clamp01(volume);
        }
        
        #endregion

        #region 单通道音效
        
        /// <summary>
        /// 创建一个音源
        /// </summary>
        private AudioSource CreateAudioSource(string name, int priority, float volume)
        {
            GameObject audioObj = new GameObject(name);
            audioObj.transform.SetParent(root);
            audioObj.transform.LocalReset();
            AudioSource audio = audioObj.AddComponent<AudioSource>();
            audio.playOnAwake = false;
            audio.priority    = priority;
            audio.volume      = volume;
            audio.mute        = isMute;
            return audio;
        }

        /// <summary>
        /// 播放单通道音效
        /// </summary>
        public  void PlaySingleSound(string clipName, bool isLoop = false, float speed = 1)
        {
            var clip = GetAddClip(clipName);
            if (singleAudio.isPlaying)
            {
                singleAudio.Stop();
            }

            singleAudio.clip = clip;
            singleAudio.loop = isLoop;
            singleAudio.pitch = speed;
            singleAudio.spatialBlend = 0;
            singleAudio.Play();
        }
        
        public void PlaySingleSoundNotStop(string clipName, bool isLoop = false, float speed = 1)
        {
            var clip = GetAddClip(clipName);
            if (singleAudio.isPlaying)
            {
                return;
            }
            singleAudio.clip = clip;
            singleAudio.loop = isLoop;
            singleAudio.pitch = speed;
            singleAudio.spatialBlend = 0;
            singleAudio.Play();
        }

        /// <summary>
        /// 暂停播放单通道音效
        /// </summary>
        public void PauseSingleSound(bool isGradual = true)
        {
            if (!singleAudio.isPlaying) return;
            // if (isGradual)
            // {
            //     _singleAudio.DOFade(0, 2);
            // }
            // else
            {
                singleAudio.volume = 0;
            }
        }

        /// <summary>
        /// 恢复播放单通道音效
        /// </summary>
        public void UnPauseSingleSound(bool isGradual = true)
        {
            if (!singleAudio.isPlaying) return;
            // if (isGradual)
            // {
            //     _singleAudio.DOFade(SoundEffectVolume, 2);
            // }
            // else
            {
                singleAudio.volume = SoundEffectVolume;
            }
        }

        /// <summary>
        /// 停止播放单通道音效
        /// </summary>
        public void StopSingleSound()
        {
            if (singleAudio.isPlaying)
            {
                singleAudio.Stop();
            }
        }
        
        public void ChangeSingleMusicVolume(float volume)
        {
            SoundEffectVolume = Mathf.Clamp01(volume);
        }
        
        #endregion

        #region 世界物体音效
        
        /// <summary>
        /// 附加一个音源，给世界音源使用
        /// </summary>
        private AudioSource AttachAudioSource(GameObject target, int priority, float volume)
        {
            AudioSource audio = target.AddComponent<AudioSource>();
            audio.playOnAwake = false;
            audio.priority    = priority;
            audio.volume      = volume;
            audio.mute        = isMute;
            return audio;
        }
        
        /// <summary>
        /// 播放世界音效
        /// </summary>
        public void PlayWorldSound(GameObject attachTarget, string clipName, bool isLoop = false, float speed = 1)
        {
            var clip = GetAddClip(clipName);
            if (worldAudio.ContainsKey(attachTarget))
            {
                AudioSource audio = worldAudio[attachTarget];
                if (audio.isPlaying)
                {
                    audio.Stop();
                }

                audio.clip = clip;
                audio.loop = isLoop;
                audio.pitch = speed;
                audio.spatialBlend = 1;
                audio.Play();
            }
            else
            {
                AudioSource audio = AttachAudioSource(attachTarget, WorldPriority, SoundEffectVolume);
                worldAudio.Add(attachTarget, audio);
                audio.clip = clip;
                audio.loop = isLoop;
                audio.pitch = speed;
                audio.spatialBlend = 1;
                audio.Play();
            }
        }

        /// <summary>
        /// 暂停播放指定的世界音效
        /// </summary>
        public void PauseWorldSound(GameObject attachTarget, bool isGradual = true)
        {
            if (!worldAudio.ContainsKey(attachTarget)) return;
            AudioSource audio = worldAudio[attachTarget];
            if (!audio.isPlaying) return;
            // if (isGradual)
            // {
            //     audio.DOFade(0, 2);
            // }
            // else
            {
                audio.volume = 0;
            }
        }

        /// <summary>
        /// 恢复播放指定的世界音效
        /// </summary>
        public void UnPauseWorldSound(GameObject attachTarget, bool isGradual = true)
        {
            if (!worldAudio.ContainsKey(attachTarget)) return;
            AudioSource audio = worldAudio[attachTarget];
            if (!audio.isPlaying) return;
            // if (isGradual)
            // {
            //     audio.DOFade(SoundEffectVolume, 2);
            // }
            // else
            {
                audio.volume = SoundEffectVolume;
            }
        }

        /// <summary>
        /// 停止播放所有的世界音效
        /// </summary>
        public void StopAllWorldSound()
        {
            foreach (KeyValuePair<GameObject, AudioSource> audio in worldAudio)
            {
                if (audio.Value.isPlaying)
                {
                    audio.Value.Stop();
                }
            }
        }
        

        /// <summary>
        /// 销毁所有闲置中的世界音效的音源
        /// </summary>
        public void ClearIdleWorldAudioSource()
        {
            HashSet<GameObject> removeSet = new HashSet<GameObject>();
            foreach (KeyValuePair<GameObject, AudioSource> audio in worldAudio)
            {
                if (!audio.Value.isPlaying)
                {
                    removeSet.Add(audio.Key);
                    Object.Destroy(audio.Value);
                }
            }

            foreach (GameObject item in removeSet)
            {
                worldAudio.Remove(item);
            }
        }
        
        #endregion

        #region 多通道音源
        
        /// <summary>
        /// 特效音源
        /// </summary>
        private void CreateMultipleAudioSource(string name, int priority, float volume)
        {
            _multipleObj = new GameObject(name);
            _multipleObj.transform.SetParent(root);
            _multipleObj.transform.LocalReset();
            for (int i = 0; i < 10; i++)
            {
                AudioSource audio = _multipleObj.AddComponent<AudioSource>();
                audio.playOnAwake = false;
                audio.priority    = priority;
                audio.volume      = volume;
                audio.mute        = isMute;
            }
        }

        /// <summary>
        /// 提取闲置中的多通道音源
        /// </summary>
        private AudioSource GetIdleMultipleAudioSource()
        {
            foreach (var audioSource in multipleAudio)
            {
                if (!audioSource.isPlaying)
                {
                    return audioSource;
                }
            }

            AudioSource audio = _multipleObj.AddComponent<AudioSource>();
            audio.playOnAwake = false;
            audio.priority    = MultiplePriority;
            audio.volume      = SoundEffectVolume;
            audio.mute        = isMute;
            multipleAudio.Add(audio);
            
            return audio;
        }
        
        /// <summary>
        /// 播放音效
        /// </summary>
        public AudioSource PlayEffect(string clipName, bool isLoop = false, float speed = 1)
        {
            var clip  = GetAddClip(clipName);
            var audio = GetIdleMultipleAudioSource();
            audio.clip         = clip;
            audio.loop         = isLoop;
            audio.pitch        = speed;
            audio.spatialBlend = 0;
            audio.Play();
            return audio;
        }

        public void StopMultipleSound()
        {
            foreach (var audio in multipleAudio)
            {
                if (audio.isPlaying)
                {
                    audio.Stop();
                }
            }
        }

        #endregion


        public override void Dispose()
        {
            StopMusic();
            StopSingleSound();
            StopAllWorldSound();
            StopMultipleSound();
        }
    }
}