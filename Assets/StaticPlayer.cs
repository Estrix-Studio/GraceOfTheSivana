using UnityEngine;

public class StaticPlayer : MonoBehaviour
{
    private static GameObject _instance;

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
