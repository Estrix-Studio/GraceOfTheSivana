using System;
using System.Collections;
using UnityEngine;

namespace Systems
{
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundMusic : MonoBehaviour
    {
        [SerializeField] private float fadeDuration = 1.0f;

        [SerializeField] private float maxVolume = 1.0f;

        private AudioSource _audioSource;

        private AudioClip _queuedClip;

        public static BackgroundMusic Instance { get; private set; }

        
        public AudioClip Clip => _audioSource.clip;
        
        private bool IsPlaying { get; set; }

        private void Awake()
        {
            // On awake, if the other is playing, change tracks
            _audioSource = GetComponent<AudioSource>();

            if (Instance != null && Instance != this)
            {
                if (Instance.Clip != _audioSource.clip)
                    Instance.PlayMusic(_audioSource.clip);
                
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            var volume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
            maxVolume= volume;
            PlayMusic();
        }

        public event Action OnMusicFaded;

        private void PlayMusic()
        {
            PlayMusic(_audioSource.clip);
        }

        private void PlayMusic(AudioClip clip)
        {
            if (IsPlaying)
            {
                QueueMusic(clip);
                StopMusic();
                return;
            }

            Instance._audioSource.clip = clip;
            StartCoroutine(FadeVolume(0, maxVolume, fadeDuration));
            _audioSource.Play();
            IsPlaying = true;
        }

        private void StopMusic()
        {
            OnMusicFaded += StopRoutineFinished;
            StartCoroutine(FadeVolume(_audioSource.volume, 0.0f, fadeDuration));
        }

        public void SetMaxVolume(float volume)
        {
            maxVolume = volume;
            _audioSource.volume = volume;
        }
        
        private void StopRoutineFinished()
        {
            OnMusicFaded -= StopRoutineFinished;
            _audioSource.Stop();
            IsPlaying = false;
            if (_queuedClip != null)
                PlayQueuedMusic();
        }

        private void QueueMusic(AudioClip clip)
        {
            _queuedClip = clip;
        }

        private void PlayQueuedMusic()
        {
            PlayMusic(_queuedClip);
            _queuedClip = null;
        }

        private IEnumerator FadeVolume(float starVolume, float targetVolume, float duration)
        {
            Instance._audioSource.volume = starVolume;
            var startTime = Time.time;

            var passedTime = 0.0f;

            while (passedTime < duration)
            {
                passedTime += Time.deltaTime;
                var percentage = passedTime / duration;
                var newVolume = Mathf.Lerp(starVolume, targetVolume, percentage);
                Instance._audioSource.volume = newVolume;
                yield return null;
            }

            Instance._audioSource.volume = targetVolume;
            OnMusicFaded?.Invoke();
        }
    }
}