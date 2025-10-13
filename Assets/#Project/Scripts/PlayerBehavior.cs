using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]

public class PlayerBehavior : MonoBehaviour
{
    private InputActionAsset actions;
    private InputAction xAxis;
    private InputAction yAxis;

    private GameManager manager;

    private Collider collider;

    public int lifePoints { get; private set; }
    private bool isInvincible = false;

    private float velocity;

    public void Initialize(InputActionAsset actions, GameManager manager, Vector3 position, int lifePoints, float velocity)
    {
        this.actions = actions;
        xAxis = this.actions.FindActionMap("PlayerControls").FindAction("XAxis");
        yAxis = this.actions.FindActionMap("PlayerControls").FindAction("YAxis");

        this.manager = manager;

        this.collider = GetComponent<Collider>();

        SetPosition(position);

        this.lifePoints = lifePoints;
        this.velocity = velocity;

        gameObject.SetActive(true);
    }

    void OnEnable()
    {
        actions.FindActionMap("PlayerControls").Enable();
    }

    void OnDisable()
    {
        actions.FindActionMap("PlayerControls").Disable();
    }

    public void Process()
    {
        Move();
    }

    public Vector3 GetExtents()
    {
        return collider.bounds.extents;
    }

    private void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    private void Move()
    {
        transform.position += xAxis.ReadValue<float>() * velocity * Time.deltaTime * Vector3.right;
        transform.position += yAxis.ReadValue<float>() * velocity * Time.deltaTime * Vector3.up;
    }

    public void LoseLife(float cooldown = 0)
    {
        if (!isInvincible)
        {
            lifePoints -= 1;
            Debug.Log("Death approaches...");
            if (cooldown > 0)
            {
                StartCoroutine(DamageCooldown(cooldown));
            }
        }
    }

    public IEnumerator DamageCooldown(float cooldown)
    {
        isInvincible = true;
        yield return new WaitForSeconds(cooldown);
        isInvincible = false;
    }

    public void Die()
    {
        gameObject.SetActive(false);
        Debug.Log("DEATH!");
    }

    public void ResetPlayer(Vector3 position)
    {
        SetPosition(position);
        gameObject.SetActive(true);
    }


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        manager.PlayerHit(collision);
    }
}
