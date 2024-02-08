using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFitter : MonoBehaviour
{
    private Camera cam;
    private Renderer render;
    [SerializeField] private float scrollSpeed;

    public void Awake()
    {
        cam = Camera.main;
        render = GetComponent<MeshRenderer>();
    }

    public void Start()
    {
        Fit();
    }

    public void Update()
    {
        StartParallax();
    }

    public void Fit()
    {
        float cameraHeight = cam.orthographicSize * 2.0f;
        float cameraWidth = cameraHeight * cam.aspect;

        transform.localScale = new Vector2( cameraHeight, cameraWidth);
    }

    public void StartParallax()
    {
        render.material.mainTextureOffset += Vector2.right * scrollSpeed * Time.deltaTime;
        render.material.mainTextureOffset = new Vector2(Mathf.Repeat(render.material.mainTextureOffset.x, 1), 0);
    }

}
