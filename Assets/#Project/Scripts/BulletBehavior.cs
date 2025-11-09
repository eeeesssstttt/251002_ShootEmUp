using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private Vector3 direction;
    private float speed;

    private GameManager manager;

    public void Initialize(Vector3 position, Vector3 direction, float speed, GameManager manager)
    {
        this.direction = direction;
        this.speed = speed;
        this.manager = manager;
        transform.position = position;
    }

    public void Process()
    {
        Vector3 movement = speed * Time.deltaTime * direction;
        transform.Translate(movement, Space.World);
        transform.up = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyBehavior>(out EnemyBehavior enemy))
        {
            manager.EnemyDestroy(enemy);
        }
        manager.BulletDestroy(this);
    }
}
