using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private InputActionAsset actions;
    private InputAction xAxis;
    private InputAction yAxis;
    [SerializeField] private float velocity = 1f;
    private Camera cam;
    // private Vector3 screenBottomLeft;
    // private Vector3 screenBottomRight;
    // private Vector3 screenTopLeft;
    // private Vector3 screenTopRight;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private void Move()
    {
        transform.position += xAxis.ReadValue<float>() * velocity * Time.deltaTime * Vector3.right;
        transform.position += yAxis.ReadValue<float>() * velocity * Time.deltaTime * Vector3.up;
    }

    private void Start()
    {
        cam = Camera.main;
        minX = cam.ScreenToWorldPoint(Vector3.zero + Vector3.forward * cam.nearClipPlane)[0];
        maxX = cam.ScreenToWorldPoint(Vector3.zero + Vector3.right * cam.pixelWidth + Vector3.forward * cam.nearClipPlane)[0];
        minY = cam.ScreenToWorldPoint(Vector3.zero + Vector3.forward * cam.nearClipPlane)[1];
        maxY = cam.ScreenToWorldPoint(Vector3.zero + Vector3.up * cam.pixelHeight + Vector3.forward * cam.nearClipPlane)[1];


        Debug.Log($"{minX}, {maxX}, {minY}, {maxY}");
        // screenBottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        // screenBottomRight = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));
        // screenTopLeft = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
        // screenTopRight = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));

        xAxis = actions.FindActionMap("PlayerControl").FindAction("XAxis");
        yAxis = actions.FindActionMap("PlayerControl").FindAction("YAxis");
    }

    private void Update()
    {
        Move();

    }
}
