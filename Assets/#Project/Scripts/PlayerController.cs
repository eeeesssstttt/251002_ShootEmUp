using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputActionAsset actions;
    private InputAction moveAction;
    private InputAction targetAction;
    private string actionMapName;
    private string moveActionName;
    private string targetActionName;
    private string shootActionName;
    private float speed;

    private Camera cam;
    private float cameraDistance;

    private BulletBehavior bullet;

    private GameManager gameManager;

    public void Initialize(Vector3 position, Camera cam, float cameraDistance, Quaternion rotation, float speed, InputActionAsset actions, string actionMapName, string moveActionName, string targetActionName, string shootActionName, BulletBehavior bullet, GameManager gameManager)
    {
        transform.SetLocalPositionAndRotation(position, rotation);

        this.cam = cam;
        this.cameraDistance = cameraDistance;

        this.speed = speed;

        this.actions = actions;
        this.actionMapName = actionMapName;
        this.moveActionName = moveActionName;
        this.targetActionName = targetActionName;
        this.shootActionName = shootActionName;
        moveAction = actions.FindActionMap(actionMapName).FindAction(moveActionName);
        targetAction = actions.FindActionMap(actionMapName).FindAction(targetActionName);
        this.bullet = bullet;

        this.gameManager = gameManager;
    }

    private void OnEnable()
    {
        actions.FindActionMap(actionMapName).FindAction(shootActionName).performed += Shoot;
        actions.FindActionMap(actionMapName).Enable();
    }

    private void OnDisable()
    {
        actions.FindActionMap(actionMapName).FindAction(shootActionName).performed -= Shoot;
        actions.FindActionMap(actionMapName).Disable();
    }

    public void Process()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movement = speed * Time.deltaTime * moveAction.ReadValue<Vector2>();
        transform.Translate(movement);

        Vector3 screenPosition = cam.WorldToScreenPoint(transform.position);

        screenPosition.x = Mathf.Clamp(screenPosition.x, 0f, Screen.width);
        screenPosition.y = Mathf.Clamp(screenPosition.y, 0f, Screen.height);

        transform.position = cam.ScreenToWorldPoint(screenPosition);
    }

    private void Shoot(InputAction.CallbackContext callbackContext)
    {
        Vector3 targetPosition = targetAction.ReadValue<Vector2>();
        targetPosition.z = cameraDistance;

        targetPosition = cam.ScreenToWorldPoint(targetPosition);

        Vector3 direction = (targetPosition - transform.position).normalized;
        BulletBehavior newBullet = Instantiate(bullet);
        newBullet.Initialize(transform.position, direction, 5f, gameManager);
        gameManager.AddBullet(newBullet);
    }
}
