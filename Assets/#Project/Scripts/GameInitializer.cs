using UnityEngine;
using UnityEngine.InputSystem;


public class GameInitializer : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private Vector3 camPosition;
    [SerializeField] private Quaternion camRotation;
    [SerializeField] private float cameraDistance = 20f;

    [Header("Controls")]
    [SerializeField] private InputActionAsset actions;

    [Space]
    [Header("Spawner")]
    [SerializeField] private Spawner spawner;
    [SerializeField] private EnemyBehavior enemyPrefab;
    [SerializeField] private int batchNumber = 10;
    [SerializeField] private float enemySpawnCooldown = 0.1f;

    [Space]
    [Header("Player")]
    [SerializeField] private PlayerBehavior player;
    [SerializeField] private int lifePoints = 3;
    [SerializeField] private float playerHitCooldown = 1f;
    [SerializeField] private float velocity = 1f;

    [Space]
    [Header("Game Manager")]
    [SerializeField] private GameManager gameManager;


    void Start()
    {
        CreateObjects();
        InitializeObjects();
        Destroy(gameObject);
    }

    private void CreateObjects()
    {
        cameraManager = Instantiate(cameraManager);
        player = Instantiate(player);
        spawner = Instantiate(spawner);
        gameManager = Instantiate(gameManager);
    }

    private void InitializeObjects()
    {
        cameraManager.Initialize(camPosition, camRotation, cameraDistance);
        (Vector3 min, Vector3 max) = cameraManager.GetRightBorderPoints();
        player.Initialize(actions, gameManager, cameraManager.GetCenter(), lifePoints, velocity);
        spawner.Initialize(enemyPrefab, min, max, batchNumber);
        gameManager.Initialize(cameraManager, player, playerHitCooldown, spawner, enemySpawnCooldown);
    }
}
