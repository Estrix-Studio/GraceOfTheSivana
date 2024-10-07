using System;
using Adventure;
using UnityEngine;

namespace Battle.Core
{
    public class EnemySpawner : MonoBehaviour
    {
        private void Awake()
        {
            var enemyPrefabForBattle = StaticContext.EnemyPrefabForBattle;
            if (enemyPrefabForBattle == null)
            {
                throw new Exception("Enemy prefab for battle is not set");
            }
            
            var enemy = Instantiate(enemyPrefabForBattle, transform.position, Quaternion.identity);
        }
    }
}