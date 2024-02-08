using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField]
    public int Health { get; private set; }

    [field: SerializeField]
    public int MoveSpeed { get;  set; }

    [field: SerializeField]
    private Collider2D Collider { get;  set; }

    [field: SerializeField]
    private float ShootSpeed;

    private float nextShootTime;

    private bool isAlive;

    public bool isInvulnerable;

    [SerializeField] private bool canShoot;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPos;
    [SerializeField] private Animator playerAnimator;

    public void Init()
    {
        Health = 3;
        MoveSpeed = 10;
        canShoot = true;
        isAlive = true;
        isInvulnerable = false;
    }

    public void Start()
    {
        Init();
    }
    public void Update()
    {
        Shoot();
        TrackTouchPos();
    }

    public void TrackTouchPos()
    {
        if ( CheckAlive())
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                Vector2 targetPosition = Camera.main.ScreenToWorldPoint(touch.position);
                transform.position = Vector2.Lerp(transform.position, targetPosition,
                    Time.deltaTime * MoveSpeed);

            }
        }
        

    }
  


    public void Shoot()
    {
        if (isInvulnerable == false && CheckAlive())
        {
            if (Time.time > nextShootTime)
            {
                nextShootTime += ShootSpeed;
                playerAnimator.Play("PlayerShoot", -1, 0.0f);
                SoundManager.Instance.PlayOnce(SoundClip.SHOOT);
                GameObject newBullet = ObjectPooling.Instance.GetObject();
                
                if (newBullet.TryGetComponent(out BulletController bulletController))
                {
                    newBullet.transform.position = shootPos.position;
                    newBullet.transform.rotation = Quaternion.LookRotation(bulletPrefab.transform.forward,
                        shootPos.position - transform.position) * Quaternion.Euler(0,0,90);
                    bulletController.direction = shootPos.position - transform.position;
                   
                }
              
                

                // GameObject newBullet = Instantiate(bulletPrefab, shootPos.position,
                //     Quaternion.LookRotation(bulletPrefab.transform.forward, shootPos.position - transform.position
                //     ) * Quaternion.Euler(0, 0, 90));
                // newBullet.TryGetComponent(out BulletController bulletController);
                // bulletController.direction = shootPos.position - transform.position;
                // Destroy(newBullet, 2.0f);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isInvulnerable == false)
        {
           
            if (other.CompareTag("EnemyShip") || other.CompareTag("Asteroid"))
            {
                Health--;
                UIManager.Instance.UpdateLivesDisplay();
                if (CheckAlive())
                {
                    
                   OnHit();
                    
                }
                else
                {
                    Die();
                }


            }

            if (other.CompareTag("Bullet"))
            {
                if (other.GetComponent<BulletController>().isEnemyBullet)
                {
                    Health--;
                    UIManager.Instance.UpdateLivesDisplay();
                    if (CheckAlive())
                    {

                        OnHit();

                    }
                    else
                    {
                        Die();
                    }
                }
            }
        }
       
    }

    public void BecomeInvulnerable(float duration)
    {
        isInvulnerable = true;
        this.Invoke(() => { isInvulnerable = false;
            nextShootTime = Time.time + ShootSpeed;
        }, duration);

    }

    public bool CheckAlive()
    {
        if (Health > 0)
        {
            isAlive = true;
        }
        else
        {
            isAlive = false;
        }

        return isAlive;
    }

    private void Die()
    {
        playerAnimator.Play("Die", -1, 0.0f);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }

    public void OnHit()
    {
        playerAnimator.Play("Invulnerable", -1, 0.0f);
        BecomeInvulnerable(1.5f);
    }
}


