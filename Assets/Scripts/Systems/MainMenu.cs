using Adventure;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private string FirstGameplaySceneName;
    
        [SerializeField] private Button startNewGameButton;
        [SerializeField] private Button continueGameButton;
    
        private void Start()
        {
            //todo Check if the save file exists
        
        }

        private void OnEnable()
        {
            startNewGameButton.onClick.AddListener(StartNewGame);
            continueGameButton.onClick.AddListener(ContinueGame);
        }

        private void OnDisable()
        {
            startNewGameButton.onClick.RemoveListener(StartNewGame);
            continueGameButton.onClick.RemoveListener(ContinueGame);
        }
    
        public void StartNewGame()
        {
            StaticContext.DoLoad = false;
            SceneManager.LoadScene(FirstGameplaySceneName);
        }

        public void ContinueGame()
        {
            StaticContext.DoLoad = true;
            SceneManager.LoadScene(FirstGameplaySceneName);
        }
    }
}
