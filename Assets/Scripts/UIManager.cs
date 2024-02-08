using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI activeBulletTxt;
    public GameObject livesDisplay;

    public void UpdateScoreUI()
    {
        StringBuilder updateScoreTxt = new StringBuilder("Score: " + GameManager.Instance.Score);
        scoreTxt.text = updateScoreTxt.ToString();
    }

    public void UpdateActiveBullet()
    {
        activeBulletTxt.text = ObjectPooling.Instance.CountActiveObjects().ToString();
    }

    public void Update()
    {
        UpdateActiveBullet();
    }

    public void UpdateLivesDisplay()
    {
        livesDisplay.transform.GetChild(GameManager.Instance.playerCtl.Health).gameObject.SetActive(false);
    }
}
