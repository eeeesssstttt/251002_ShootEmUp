using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private EnemyBehavior enemyPrefab;
    // [SerializeField] private float velocity = 1f;
    private Queue<EnemyBehavior> enemies;
    [SerializeField] private float enemyVelocity = 1f;
    private Pool<EnemyBehavior> pool;

    private void Start()
    {
        CreateObjects();
        InitializeObjects();
    }

    private void CreateObjects()
    {
        gameManager = Instantiate(gameManager);
        pool = new Pool<EnemyBehavior>();
    }

    private void InitializeObjects()
    {
        enemies = pool.CreateBatch(enemyPrefab.gameObject);
        gameManager.Initialize(pool, enemies);
    }
}
