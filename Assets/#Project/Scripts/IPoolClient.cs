using UnityEngine;

public interface IPoolClient
{
    public void Appear(Vector3 position, Quaternion rotation);
    public void Disappear();
}
