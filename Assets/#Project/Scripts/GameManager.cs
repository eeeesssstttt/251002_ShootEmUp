using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Spawner spawner;
    private PlayerController player;
    private List<EnemyBehavior> enemies = new();
    private List<BulletBehavior> bullets = new();
    private float cooldown;
    private float timer = 0f;

    private int playerLives;

    private LifeDisplay lifeDisplay;

    public void Initialize(Spawner spawner, float cooldown, PlayerController player, int startingLives, LifeDisplay lifeDisplay)
    {
        this.spawner = spawner;
        this.cooldown = cooldown;
        this.player = player;
        playerLives = startingLives;
        this.lifeDisplay = lifeDisplay;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= cooldown)
        {
            timer = 0f;
            EnemyBehavior enemy = spawner.Spawn();
            if (!enemies.Contains(enemy))
            {
                enemies.Add(enemy);
                enemy.Initialize(this);
            }
        }

        for (int index = 0; index < enemies.Count; index++)
        {
            enemies[index].Process();
        }

        for (int index = 0; index < bullets.Count; index++)
        {
            bullets[index].Process();
        }

        player.Process();
    }

    public void AddBullet(BulletBehavior bullet)
    {
        bullets.Add(bullet);
    }

    public void BulletDestroy(BulletBehavior bullet)
    {
        bullets.Remove(bullet);
        Destroy(bullet.gameObject);
    }

    public void EnemyDestroy(EnemyBehavior enemy)
    {
        spawner.Despawn(enemy);
    }

    public void PlayerContact(GameObject other)
    {
        if (other.TryGetComponent(out EnemyBehavior enemy))
        {
            playerLives -= 1;
            if (playerLives >= 0) lifeDisplay.UpdateDisplay(playerLives);
            EnemyDestroy(enemy);
        }
    }
}