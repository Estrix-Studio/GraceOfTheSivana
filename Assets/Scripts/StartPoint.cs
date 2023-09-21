using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private void Start()
    {
        StaticPlayer.Instance.transform.position = transform.position;
    }
}