using Adventure;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] public string firstGameplaySceneName;

        
        /* Main Menu Scene buttons */
        [SerializeField] private Button startNewGameButton;
        [SerializeField] private Button continueGameButton;
        [SerializeField] private Button quitGameButton;

        /* Back Button. Directs to Scenes/MainMenu */
        [SerializeField] private Button backButton;

        private void Start()
        {
            //todo Check if the save file exists
        }

        private void OnEnable()
        {
            startNewGameButton.onClick.AddListener(StartNewGame);
            continueGameButton.onClick.AddListener(ContinueGame);
            quitGameButton.onClick.AddListener(QuitGame);
            backButton.onClick.AddListener(GoBack);
        }

        private void OnDisable()
        {
            startNewGameButton.onClick.RemoveListener(StartNewGame);
            continueGameButton.onClick.RemoveListener(ContinueGame);
            quitGameButton.onClick.RemoveListener(QuitGame);
            backButton.onClick.RemoveListener(GoBack);
        }

        private void StartNewGame()
        {
            StaticContext.DoLoad = false;
            SceneManager.LoadScene(firstGameplaySceneName);
        }

        private void ContinueGame()
        {
            StaticContext.DoLoad = true;
            SceneManager.LoadScene(firstGameplaySceneName);
        }

        private static void QuitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        private void GoBack()
        {
            SceneManager.LoadSceneAsync("Scenes/MainMenu", LoadSceneMode.Additive);
        }
    }
}