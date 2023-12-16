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
            // Implement setting SFX volume logic here
            // You may use AudioManager or other methods to handle audio volume
            PlayerPrefs.SetFloat("SfxVolume", volume);
            Debug.Log("<color=blue>SFX Volume: " + PlayerPrefs.GetFloat("SfxVolume") + "</color>");
        }

        public void SetMusicVolume(float volume)
        {
            // Implement setting MUSIC volume logic here
            // You may use AudioManager or other methods to handle audio volume
            PlayerPrefs.SetFloat("MusicVolume", volume);
            Debug.Log("<color=blue>Music Volume: " + PlayerPrefs.GetFloat("MusicVolume") + "</color>");
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
