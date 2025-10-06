using UnityEngine;

public interface IPoolClient
{
    public void Rise(Vector3 position, Quaternion rotation);
    public void Fall();
}
