using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    private List<GameObject> _gameObjectsList;
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int initAmount;
    [SerializeField] private int poolSize;


    public void Awake()
    {
        _gameObjectsList = new List<GameObject>();
    }

    public void Start()
    {
        InitPool();
    }

    public void InitPool()
    {
        AddObject(initAmount);
    }

    public GameObject GetObject()
    {
        foreach (var go in _gameObjectsList)
        {
            if (go.activeSelf == false)
            {
                go.SetActive(true);
                return go;
            }
        }

        AddObject(1);
        return GetObject();
    }

    public int CountActiveObjects()
    {
        int count = 0;
        foreach (var ob in _gameObjectsList)
        {
            if (ob.activeSelf)
            {
                count++;
            }
        }

        return count;
    }

    private void AddObject(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject newGO = Instantiate(objectPrefab, transform);
            if (newGO != null)
            {
                _gameObjectsList.Add(newGO);
                newGO.SetActive(false);
                poolSize++;
            }
        }
    }
    
    
    
}
