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
        
        private void Awake()
        {
            _movingObject = GetComponent<MovingObject>();
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
            }
        }
    }
}