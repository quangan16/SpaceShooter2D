using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public Vector3 direction;
    [SerializeField] public bool isEnemyBullet;

    public void OnEnable()
    {
        Init();
    }
    
    public void Update()
    {
        Move();
    }

    public void Init()
    {
        isEnemyBullet = false;
        speed = 10.0f;
    }

    public void Move()
    {
        transform.position += direction.normalized * speed *Time.deltaTime;
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEnemyBullet && !other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
        
    }

    public void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
