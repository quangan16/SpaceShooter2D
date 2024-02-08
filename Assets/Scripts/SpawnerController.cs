using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private float spawnRate;
    [SerializeField] private List<EnemyType> EnemyTypeList;
    [SerializeField] private BoxCollider2D collider2D;
    
  
    private float currentTime;
    public void Update()
    {
        SpawnEnemies();
    }

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        transform.position = Vector2.up *( Camera.main.orthographicSize + 0.5f);
        collider2D.size =
            new Vector2(Camera.main.orthographicSize *2 * Camera.main.aspect, 1);
    }

    public void SpawnEnemies()
    {
        if (currentTime < spawnRate)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0;
            spawnRate = Random.Range(1.5f, 3.0f);
            int randomIndex = Random.Range(0, EnemyTypeList.Count);
            GameObject newEnemy = Instantiate(EnemyTypeList[randomIndex].enemyPrefabs[Random.Range(0, 
                EnemyTypeList[randomIndex].enemyPrefabs.Length)], SetRandomPos(), Quaternion.identity * Quaternion.Euler(0,0,-180));
            
        }
        
       
    }
    
    // public Vector3 SetRandomPos()
    // {
    //     float screenWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
    //     Vector3 randomPos = new Vector3(Random.Range(-screenWidth*0.5f, screenWidth*0.5f), Camera.main.orthographicSize + 1.0f , 0);
    //     return randomPos;
    // }

    public Vector3 SetRandomPos()
    {
        Vector3 randomPos = new Vector3(Random.Range(collider2D.bounds.min.x, collider2D.bounds.max.x),
            collider2D.bounds.center.y,0);
        return randomPos;
    }
}

[Serializable]
public class EnemyType{
    [SerializeField] public GameObject[] enemyPrefabs;
}
