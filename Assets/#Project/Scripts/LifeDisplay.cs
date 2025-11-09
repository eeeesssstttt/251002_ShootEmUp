using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class LifeDisplay : MonoBehaviour
{
    private Image image;
    private Canvas canvas;
    private Vector2 _1stImagePosition;
    private Vector2 offset;

    private List<Image> images = new();

    public void Initialize(Image image, int startingLives, Vector2 _1stImagePosition, Vector2 offset)
    {
        this.image = image;
        this._1stImagePosition = _1stImagePosition;
        this.offset = offset;

        canvas = GetComponent<Canvas>();

        UpdateDisplay(startingLives);
    }

    public void UpdateDisplay(int lives)
    {
        if (images.Count < lives)
        {
            for (int index = 0; index < images.Count; index++)
            {
                images[index].gameObject.SetActive(true);
            }
            for (int index = images.Count; index < lives; index++)
            {
                Image newImage = Instantiate(image, canvas.transform);
                images.Add(newImage);
                Vector2 position = _1stImagePosition + index * offset;
                newImage.rectTransform.anchoredPosition = position;
            }
        }
        else
        {
            for (int index = 0; index < images.Count; index++)
            {
                images[index].gameObject.SetActive(index < lives);
            }
        }
    }
}