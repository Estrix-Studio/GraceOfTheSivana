using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Adventure
{
    [RequireComponent(typeof(MovingObject))]
    public class EnemyCollision : MonoBehaviour
    {
        private MovingObject _movingObject;

        [SerializeField] private GameObject enemyPrefabForBattle;
     
        [SerializeField] private string enemyID;

        private void Awake()
        {
            _movingObject = GetComponent<MovingObject>();
            var enemyIsHere = PlayerPrefs.GetInt(enemyID, 1);
        
            if (enemyIsHere == 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // transfer to battle scene
                // TODO: implement
                _movingObject.isMoving = false;
                StaticContext.EnemyPrefabForBattle = enemyPrefabForBattle;
                SceneManager.MoveGameObjectToScene(other.gameObject, SceneManager.GetActiveScene());
                SceneManager.LoadScene("EncounterScene");
                PlayerPrefs.SetInt(enemyID, 0);
            }
        }
    }
}