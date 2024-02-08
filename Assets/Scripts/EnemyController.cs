using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [field: SerializeField]
    public int Health { get; private set; }

    [field: SerializeField] public float shootRate;

    private float currentTime;

    private bool canShoot;

    [SerializeField] private float moveSpeed;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingPos;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private Rigidbody2D enemyRb;

    public void Start()
    {
        Init();
    }
    
    public void Init()
    {
        canShoot = false;
        currentTime = 0.0f;
        if (CompareTag("EnemyShip"))
        {
            Health = 1;
            moveSpeed = 1.5f;
        }
        else if (CompareTag("Asteroid"))
        {
            Health = 2;
        }
    }

    public void Update()
    {
        MoveForward();
        Shoot();
        Rotate();
        gameObject.OutScreenDestroy();

    }

    public void Rotate()
    {
        if (CompareTag("Asteroid"))
        {
            transform.Rotate(transform.forward, 10.0f * Time.deltaTime);
        }
    }

    public void Shoot()
    {
        if (CompareTag("EnemyShip"))
        {
            if (Health > 0)
            {
                if (currentTime > shootRate)
                {
                    enemyAnimator.Play("EnemyShoot", -1, 0.0f);
                    canShoot = true;
                }
                else
                {
                    currentTime += Time.deltaTime;
                }

                if (canShoot)
                {
                    canShoot = false;
                    currentTime = 0.0f;
                    shootRate = Random.Range(1.5f, 5.0f);
                    GameObject newBullet = ObjectPooling.Instance.GetObject();
                    newBullet.transform.position = shootingPos.position;
                    newBullet.transform.rotation = Quaternion.LookRotation(bulletPrefab.transform.forward,
                        shootingPos.position - transform.position) * Quaternion.Euler(0,0,90.0f);
                    
                    if (newBullet.TryGetComponent(out BulletController bulletCtl)) ;
                    {
                        bulletCtl.SetSpeed(8.0f);
                        bulletCtl.direction = shootingPos.position - transform.position;
                        bulletCtl.isEnemyBullet = true;
                    }

                    // GameObject newBullet = Instantiate(bulletPrefab, shootingPos.position,
                    //     Quaternion.LookRotation(bulletPrefab.transform.forward,
                    //         shootingPos.position - transform.position) * Quaternion.Euler(0, 0, 90.0f));
                    // if (newBullet.TryGetComponent(out BulletController bulletCtl)) ;
                    // {
                    //     bulletCtl.SetSpeed(8.0f);
                    //     bulletCtl.direction = shootingPos.position - transform.position;
                    //     bulletCtl.isEnemyBullet = true;
                    // }
                    // Destroy(newBullet, 2.0f);
                }
            }
            
        }
    }

    public void MoveForward()
    {
        transform.Translate(-Vector3.up * (moveSpeed * Time.deltaTime), Space.World);
       
    }

    public void Die()
    {
        GameManager.Instance.GetScore(1);
        enemyRb.simulated = false;
        if (CompareTag("EnemyShip"))
        {
            enemyAnimator.Play("Die", -1, 0.0f);
            SoundManager.Instance.PlayOnce(SoundClip.SHIP_DESTROY);
        }
        else if (CompareTag("Asteroid"))
        {
            enemyAnimator.Play("Crack", -1, 0.0f);
            SoundManager.Instance.PlayOnce(SoundClip.ASTEROID_DESTROY);
        }
       
        this.Invoke(()=>Destroy(this.gameObject), 1.0f);
    }

    public bool CheckAlive()
    {
        if (Health > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            Die();
        }

        if (other.CompareTag("Bullet") && other.GetComponent<BulletController>().isEnemyBullet == false)
        {
            Health--;
            if (!CheckAlive())
            {
                Die();
            }
        }
    }
    
    
}
