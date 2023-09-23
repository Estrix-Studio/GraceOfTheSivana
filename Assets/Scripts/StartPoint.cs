using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private Color backgroundColor;
    private void Start()
    {
        StaticPlayer.Instance.transform.position = transform.position;
        StaticPlayer.Instance.GetComponentInChildren<Camera>().backgroundColor = backgroundColor;
    }
}