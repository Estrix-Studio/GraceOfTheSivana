using Adventure;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Systems.UI
{
    public class Options : MonoBehaviour
    {
        [SerializeField] private Button backButton;

        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;

        
        private void Start()
        {
            // Load saved volume settings
            LoadVolumeSettings();
        }

        private void OnEnable()
        {
            backButton.onClick.AddListener(GoBack);
            // Add listeners for volume sliders
            sfxVolumeSlider.onValueChanged.AddListener(SetSfxVolume);
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        private void OnDisable()
        {
            backButton.onClick.RemoveListener(GoBack);
            // Remove listeners for volume sliders
            sfxVolumeSlider.onValueChanged.RemoveListener(SetSfxVolume);
            musicVolumeSlider.onValueChanged.RemoveListener(SetMusicVolume);
        }

        private static void GoBack()
        {
            // Save volume settings before going back to MainMenu
            SaveVolumeSettings();
            SceneManager.LoadScene("MainMenu");
        }

        public void SetSfxVolume(float volume)
        {
            // As of our slider goes from 1 to 10 we divide the value by maxValue
            // and getting float number from 0 to 1 to set into our MusicClip.
            PlayerPrefs.SetFloat("SfxVolume", volume / musicVolumeSlider.maxValue);
            Debug.Log("<color=cyan>" +
                      "SFX Volume: " + PlayerPrefs.GetFloat("SfxVolume")
                      + "</color>");
        }

        public void SetMusicVolume(float volume)
        {
            // As of our slider goes from 1 to 10 we divide the value by maxValue
            // and getting float number from 0 to 1 to set into our MusicClip.
            PlayerPrefs.SetFloat("MusicVolume", volume / sfxVolumeSlider.maxValue);
            Debug.Log("<color=cyan>Music Volume: " + 
                      PlayerPrefs.GetFloat("MusicVolume")
                      + "</color>");
        }

        private void LoadVolumeSettings()
        {
            // Load saved volume settings or use default values
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SfxVolume", 0.5f);
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        }

        private static void SaveVolumeSettings()
        {
            // Save volume settings
            PlayerPrefs.Save();
        }
    }
}
