using UnityEngine;

public class StaticPlayer : MonoBehaviour
{
    private static StaticPlayer _instance;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }
}
