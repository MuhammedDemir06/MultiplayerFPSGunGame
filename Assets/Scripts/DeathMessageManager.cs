using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DeathMessageManager : MonoBehaviour
{
    public static DeathMessageManager Instance;
    [SerializeField] private TextMeshProUGUI playerNameText,enemyNameText;
    [SerializeField] private Animator killAnim;
    private void Start()
    {
        Instance = this;
    }
    public void ShowDeathMessageManager(string playerName,string enemyName)
    {
        playerNameText.text = playerName;
        enemyNameText.text = enemyName;
        killAnim.SetTrigger("Score");
    }
}
