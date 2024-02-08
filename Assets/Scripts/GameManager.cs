using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject[] playerPrefab;
    [SerializeField] private Transform initPlayPos;
    [SerializeField] public PlayerController playerCtl;
    [field: SerializeField]
    public int Score { get; private set; }
    
    
    public void Start()
    {
        Init();
        SpawnPlayer();
    }
    

    public void Init()
    {
        Application.targetFrameRate = 60;
        Score = 0;
    }

    public void SpawnPlayer()
    {
        int randomIndex = Random.Range(0, playerPrefab.Length);
        playerCtl = Instantiate(playerPrefab[randomIndex], initPlayPos.position, Quaternion.identity).GetComponent<PlayerController>();
    }

    public void GetScore(int _point)
    {
        Score += _point;
        UIManager.Instance.UpdateScoreUI();
    }
}
