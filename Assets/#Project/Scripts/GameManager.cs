using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Camera cam;
    private Vector3 screenBottomLeft;
    private Vector3 screenBottomRight;
    private Vector3 screenTopLeft;
    private Vector3 screenTopRight;
    private List<EnemyBehavior> activeEnemies;
    private Pool<EnemyBehavior> pool;
    private Queue<EnemyBehavior> inactiveEnemies;
    public void Initialize(Pool<EnemyBehavior> pool, Queue<EnemyBehavior> enemies)
    {
        cam = Camera.main;
        screenBottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        screenBottomRight = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));
        screenTopLeft = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
        screenTopRight = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));

        this.pool = pool;
        this.activeEnemies = new();
        this.inactiveEnemies = enemies;
    }

    public void Update()
    {
        // want to check position of each enemy. Need a for over an array for that
    }
}
