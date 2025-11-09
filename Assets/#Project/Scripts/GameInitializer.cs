using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameInitializer : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private Vector3 camPosition;
    [SerializeField] private Quaternion camRotation;
    [SerializeField] private float cameraDistance = 20f;


    [Space]
    [Header("Controls")]
    [SerializeField] private InputActionAsset actions;
    [SerializeField] private string actionMapName;
    [SerializeField] private string moveActionName;
    [SerializeField] private string targetActionName;
    [SerializeField] private string shootActionName;


    [Space]
    [Header("Spawner")]
    [SerializeField] private Spawner spawner;
    [SerializeField] private EnemyBehavior enemyPrefab;
    [SerializeField] private int batchSize = 10;
    [SerializeField] private float cooldown = 0.1f;


    [Space]
    [Header("Game Manager")]
    [SerializeField] private GameManager gameManager;


    [Space]
    [Header("Player")]
    [SerializeField] private PlayerController player;
    [SerializeField] private int startingLives = 3;
    [SerializeField] float speed = 5f;


    [Space]
    [Header("Bullets")]
    [SerializeField] BulletBehavior defaultBullet;


    [Space]
    [Header("UI")]
    [SerializeField] private LifeDisplay lifeDisplay;
    [SerializeField] private Image lifeImage;
    [SerializeField] private Vector2 _1stImagePosition;
    [SerializeField] private Vector2 offset;


    void Start()
    {
        CreateObjects();
        InitializeObjects();
        // After creating all necessary elements, the GameInitializer self-destructs.
        Destroy(gameObject);
    }

    private void CreateObjects()
    {
        cameraManager = Instantiate(cameraManager);
        player = Instantiate(player);
        spawner = Instantiate(spawner);
        gameManager = Instantiate(gameManager);
        lifeDisplay = Instantiate(lifeDisplay);
    }

    private void InitializeObjects()
    {
        cameraManager.Initialize(camPosition, camRotation, cameraDistance);
        (Vector3 screenBottomRight, Vector3 screenTopRight) = cameraManager.GetRightBorderPoints();
        spawner.Initialize(enemyPrefab, screenBottomRight, screenTopRight, batchSize);


        player.Initialize(cameraManager.GetCenter(), cameraManager.Cam, cameraDistance, Quaternion.identity, speed, actions, actionMapName, moveActionName, targetActionName, shootActionName, defaultBullet, gameManager);
        player.gameObject.SetActive(true);

        player.GetComponent<PlayerCollisionInfo>().Initialize(gameManager);

        gameManager.Initialize(spawner, cooldown, player, startingLives, lifeDisplay);
        lifeDisplay.Initialize(lifeImage, startingLives, _1stImagePosition, offset);
    }
}
