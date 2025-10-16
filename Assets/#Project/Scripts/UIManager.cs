using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    private CanvasRenderer lifeSprite;
    private List<CanvasRenderer> lifeSprites;
    public void Initialize(GameManager gameManager, CanvasRenderer lifeSprite)
    {
        this.gameManager = gameManager;
        this.lifeSprite = lifeSprite;
        lifeSprites = new List<CanvasRenderer>();
        lifeSprites.Add(GetComponentInChildren<CanvasRenderer>());
    }

    public void DisplayLives()
    {
        for (int i = 0; i < gameManager.GetPlayerLives(); i++)
        {

        }
    }

    void Update()
    {
        DisplayLives();
    }
}
