using Adventure;
using UnityEngine;

namespace Adventure
{
    /// <summary>
    /// Makes player object static between scenes.
    /// </summary>
    public class StaticPlayer : MonoBehaviour
    {
        private static GameObject _instance;

        public static GameObject Instance => _instance;
    
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = gameObject;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
    
    private void OnDestroy()
    {
        SaveLoad.Instance.SavePlayer();
    }
}
