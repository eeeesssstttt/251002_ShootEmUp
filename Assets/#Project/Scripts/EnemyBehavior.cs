using UnityEngine;

public class EnemyBehavior : MonoBehaviour, IPoolClient
{
    private float velocity = 1f;

    // Ideally, would set velocity in GameInitializer, and give it to GameManager or Pool.

    public void Appear(Vector3 position, Quaternion rotation)
    {
        gameObject.SetActive(true);
        transform.SetPositionAndRotation(position, rotation);
    }

    public void Disappear()
    {
        gameObject.SetActive(false);
    }

    // public void SetVelocity(float velocity)
    // {
    //     this.velocity = velocity;
    // }

    public void MoveRightToLeft()
    {
        transform.position += velocity * Time.deltaTime * Vector3.left;
    }
}
