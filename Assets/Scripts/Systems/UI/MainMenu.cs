using Adventure;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Systems.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] public string firstGameplaySceneName;
        
        /* Main Menu Scene buttons */
        [SerializeField] private Button startNewGameButton;
        [SerializeField] private Button continueGameButton;
        [SerializeField] private Button creditsButton;
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button quitGameButton;
        //[SerializeField] private Button backButton;

        private void Start()
        {
            //todo Check if the save file exists
            EventSystem eventSystem;
        }

        private void OnEnable()
        {
            startNewGameButton.onClick.AddListener(StartNewGame);
            continueGameButton.onClick.AddListener(ContinueGame);
            quitGameButton.onClick.AddListener(QuitGame);
            creditsButton.onClick.AddListener(OpenCreditsScene);
            optionsButton.onClick.AddListener(OpenOptionsScene);
        }

        private void OnDisable()
        {
            startNewGameButton.onClick.RemoveListener(StartNewGame);
            continueGameButton.onClick.RemoveListener(ContinueGame);
            quitGameButton.onClick.RemoveListener(QuitGame);
            creditsButton.onClick.RemoveListener(OpenCreditsScene);
            optionsButton.onClick.RemoveListener(OpenOptionsScene);
        }

        private void StartNewGame()
        {
            StaticContext.DoLoad = false;
            PlayerPrefs.SetInt("enemy1", 1);
            PlayerPrefs.SetInt("enemy2", 1);
            PlayerPrefs.SetInt("enemy3", 1);
            SceneManager.LoadScene(firstGameplaySceneName);
        }

        private void ContinueGame()
        {
            StaticContext.DoLoad = true;
            SceneManager.LoadScene(firstGameplaySceneName);
        }

        void OpenCreditsScene()
        {
            SceneManager.LoadScene("CreditsScene");
        }

        void OpenOptionsScene()
        {
            SceneManager.LoadScene("OptionsScene");
        }

        private void QuitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}