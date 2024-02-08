using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundFitScaler : MonoBehaviour
{
    [SerializeField] private RawImage backgroundImage;
    [SerializeField] private float scrollSpeed;
    private Camera cam;

    public void Awake()
    {
        cam = Camera.main;
    }

    public void Update()
    {
        ParallaxBg();
    }

    public void ParallaxBg()
    {
        backgroundImage.uvRect = new Rect(backgroundImage.uvRect.position + Vector2.right * (scrollSpeed * Time.deltaTime), backgroundImage.uvRect.size);
    }
}
