using UnityEngine;

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