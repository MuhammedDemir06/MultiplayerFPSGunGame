using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOrLostController : MonoBehaviour
{
    [SerializeField] private GameObject winScreen, lostScreen,winOrLostScreen;
    
    private void Control()
    {
        if (GameManager.GameOver)
            winOrLostScreen.SetActive(true);
        if(ScoreManager.Instance.PlayerScore==40)
        {
            winScreen.SetActive(true);
        }
        else if(ScoreManager.Instance.EnemyScore==40)
        {
            lostScreen.SetActive(true);
        }
    }
    private void Update()
    {
        Control();
    }
}
