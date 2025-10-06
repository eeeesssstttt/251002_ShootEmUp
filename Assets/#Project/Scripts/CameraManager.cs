using NUnit.Framework.Constraints;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{

    private Camera cam;
    private float cameraDistance;
    public void Initialize(Vector3 position, Quaternion rotation, float cameraDistance)
    {
        transform.SetPositionAndRotation(position, rotation);
        cam = GetComponent<Camera>();
        this.cameraDistance = cameraDistance;

        gameObject.SetActive(true);
    }

    public Vector3 GetCenter()
    {
        return cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, cameraDistance));
    }

    public (Vector3, Vector3) GetRightBorderPoints()
    {
        Vector3 top = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cameraDistance));
        Vector3 bottom = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, cameraDistance));
        return (bottom, top);
    }

    private Vector3 GetBottomLeftCorner()
    {
        return cam.ScreenToWorldPoint(new Vector3(0, 0, cameraDistance));
    }

    private Vector3 GetTopRightCorner()
    {
        return cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cameraDistance));
    }

    public float GetLeft()
    {
        return GetBottomLeftCorner()[0];
    }

    public float GetBottom()
    {
        return GetBottomLeftCorner()[1];
    }

    public float GetRight()
    {
        return GetTopRightCorner()[0];
    }

    public float GetTop()
    {
        return GetTopRightCorner()[1];
    }
}
