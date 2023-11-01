using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Used to change map when player enters trigger.
/// TEMPORARY DEMO SOLUTION
/// </summary>
public class MapChanger : MonoBehaviour
{
    [SerializeField] private string sceneName;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {        
            SceneManager.LoadScene(sceneName);
        }
    }
}
