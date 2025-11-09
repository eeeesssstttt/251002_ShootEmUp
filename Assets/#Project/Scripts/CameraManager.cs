using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{

    public Camera Cam { get; private set; }
    private float cameraDistance;
    public void Initialize(Vector3 position, Quaternion rotation, float cameraDistance)
    {
        transform.SetPositionAndRotation(position, rotation);
        Cam = GetComponent<Camera>();
        this.cameraDistance = cameraDistance;

        gameObject.SetActive(true);
    }

    public Vector3 GetCenter()
    {
        return Cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, cameraDistance));
    }

    public (Vector3, Vector3) GetRightBorderPoints()
    {
        Vector3 bottom = Cam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, cameraDistance));
        Vector3 top = Cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cameraDistance));
        return (bottom, top);
    }
}
