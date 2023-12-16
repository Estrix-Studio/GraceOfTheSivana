using System;
using System.Collections;
using UnityEngine;

namespace Systems
{
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundMusic : MonoBehaviour
    {
        private static BackgroundMusic _instance;
        public static BackgroundMusic Instance => _instance;
        
        private AudioSource _audioSource;
        
        [SerializeField] private float fadeDuration = 1.0f;
        
        [SerializeField] private float maxVolume = 1.0f;

        private bool _isPlaying;
        public bool IsPlaying => _isPlaying; 
        
        public event Action OnMusicFaded;
        
        private AudioClip _queuedClip;
        
        private void Awake()
        {
            // On awake, if the other is playing, change tracks
            _audioSource = GetComponent<AudioSource>();
            
            if (_instance != null && _instance != this)
            {
                _instance.PlayMusic(_audioSource.clip);
                Destroy(gameObject);
                return;
            }
        
            _instance = this;
            DontDestroyOnLoad(gameObject);
            PlayMusic();
        }
    
        public void PlayMusic()
        {
            PlayMusic(_audioSource.clip);
        }
        
        public void PlayMusic(AudioClip clip)
        {
            if (_isPlaying)
            {
                QueueMusic(clip);
                StopMusic();
                return;
            }
            
            _instance._audioSource.clip = clip;
            StartCoroutine(FadeVolume(0, maxVolume, fadeDuration));
            _audioSource.Play();
            _isPlaying = true;
        } 
    
        public void StopMusic()
        {
            OnMusicFaded += StopRoutineFinished;
            StartCoroutine(FadeVolume(_audioSource.volume, 0.0f, fadeDuration));            
        }

        private void StopRoutineFinished()
        {
            OnMusicFaded -= StopRoutineFinished;
            _audioSource.Stop();
            _isPlaying = false;
            if (_queuedClip != null)
                PlayQueuedMusic();
        }
        
        private void QueueMusic(AudioClip clip)
        {
            _queuedClip = clip;
        }
        
        public void PlayQueuedMusic()
        {
            PlayMusic(_queuedClip);
            _queuedClip = null;
        }
        
        private IEnumerator FadeVolume(float starVolume, float targetVolume, float duration)
        {
            _instance._audioSource.volume = starVolume;
            var startTime = Time.time;

            var passedTime = 0.0f;
            
            while (passedTime < duration)
            {
                passedTime += Time.deltaTime;
                var percentage = passedTime / duration;
                var newVolume = Mathf.Lerp(starVolume, targetVolume, percentage);
                _instance._audioSource.volume = newVolume;
                yield return null;
            }
        
            _instance._audioSource.volume = targetVolume;
            OnMusicFaded?.Invoke();
        }
    }
}
