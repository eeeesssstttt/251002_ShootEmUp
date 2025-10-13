using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CameraManager cameraManager;
    private float screenLeft;
    private float screenRight;
    private float screenBottom;
    private float screenTop;


    private PlayerBehavior player;
    private float playerHalfExtentX;
    private float playerHalfExtentY;
    private float playerDamageCooldown;


    private Spawner spawner;
    private List<EnemyBehavior> enemies = new();
    private float enemySpawnCooldown;


    public void Initialize(CameraManager cameraManager, PlayerBehavior player, float playerDamageCooldown, Spawner spawner, float cooldown)
    {
        this.cameraManager = cameraManager;
        this.player = player;
        this.playerDamageCooldown = playerDamageCooldown;
        this.spawner = spawner;
        this.enemySpawnCooldown = cooldown;

        screenLeft = cameraManager.GetLeft();
        screenRight = cameraManager.GetRight();
        screenBottom = cameraManager.GetBottom();
        screenTop = cameraManager.GetTop();

        playerHalfExtentX = player.GetExtents()[0];
        playerHalfExtentY = player.GetExtents()[1];

        gameObject.SetActive(true);

        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        for (int index = 0; index < enemies.Count; index++)
        {
            enemies[index].Process();
        }

        if (player.enabled)
        {
            player.Process();
            KeepPlayerOnScreen();
        }
    }

    public void EnemyOffScreen(EnemyBehavior enemy)
    {
        spawner.DeSpawn(enemy);
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemySpawnCooldown);
            EnemyBehavior enemy = spawner.Spawn();
            if (!enemies.Contains(enemy))
            {
                enemies.Add(enemy);
                enemy.Initialize(this);
            }
        }
    }

    private void KeepPlayerOnScreen()
    {
        Vector3 playerPosition = player.transform.position;

        playerPosition.x = Mathf.Clamp(playerPosition.x, screenLeft + playerHalfExtentX, screenRight - playerHalfExtentX);
        playerPosition.y = Mathf.Clamp(playerPosition.y, screenBottom + playerHalfExtentY, screenTop - playerHalfExtentY);

        player.transform.position = playerPosition;
    }

    public void PlayerHit(Collision collision)
    {
        spawner.DeSpawn(collision.gameObject.GetComponent<EnemyBehavior>());
        if (player.lifePoints >= 0)
        {
            player.LoseLife(playerDamageCooldown);
        }
        // else
        // {
        //     player.Die();
        // }
    }
}