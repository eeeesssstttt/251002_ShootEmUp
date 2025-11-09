using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Vector3 spawnAreaBottom;
    private Vector3 spawnAreaTop;
    private Pool<EnemyBehavior> pool;


    public void Initialize(EnemyBehavior enemy, Vector3 spawnAreaBottom, Vector3 spawnAreaTop, int batchSize)
    {
        this.spawnAreaBottom = spawnAreaBottom;
        this.spawnAreaTop = spawnAreaTop;

        pool = new(enemy.gameObject, batchSize);
    }

    public EnemyBehavior Spawn()
    {
        float t = Random.Range(0f, 1f);
        return pool.Get(Vector3.Lerp(spawnAreaBottom, spawnAreaTop, t), Quaternion.identity);
    }

    public void Despawn(EnemyBehavior enemy)
    {
        pool.Add(enemy);
    }
}
