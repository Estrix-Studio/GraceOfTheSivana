using Adventure;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenu : MonoBehaviour
    {
        [FormerlySerializedAs("FirstGameplaySceneName")] [SerializeField] private string firstGameplaySceneName;

        [SerializeField] private Button startNewGameButton;
        [SerializeField] private Button continueGameButton;
        [SerializeField] private Button quitGameButton;

        private void Start()
        {
            //todo Check if the save file exists
        }

        private void OnEnable()
        {
            startNewGameButton.onClick.AddListener(StartNewGame);
            continueGameButton.onClick.AddListener(ContinueGame);
            quitGameButton.onClick.AddListener(QuitGame);
        }

        private void OnDisable()
        {
            startNewGameButton.onClick.RemoveListener(StartNewGame);
            continueGameButton.onClick.RemoveListener(ContinueGame);
            quitGameButton.onClick.RemoveListener(QuitGame);
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
    }
}