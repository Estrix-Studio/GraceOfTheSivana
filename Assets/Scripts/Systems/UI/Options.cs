using System;
using Adventure;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Systems.UI
{
    public class Options : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnEnable()
        {
            backButton.onClick.AddListener(GoBack);
        }

        private void OnDisable()
        {
            backButton.onClick.RemoveListener(GoBack);
        }

        private static void GoBack()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
