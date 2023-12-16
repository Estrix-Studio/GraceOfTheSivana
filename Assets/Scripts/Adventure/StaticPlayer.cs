using UnityEngine;

namespace Adventure
{
    /// <summary>
    ///     Makes player object static between scenes.
    /// </summary>
    public class StaticPlayer : MonoBehaviour
    {
        public static GameObject Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = gameObject;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void OnDestroy()
        {
            SaveLoad.Instance.SavePlayer();
        }
    }
}