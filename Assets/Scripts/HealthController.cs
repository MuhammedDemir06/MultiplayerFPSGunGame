using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using TMPro;
using System;

public class HealthController : MonoBehaviour
{
    public delegate void PlayerHealthManager();
    public static PlayerHealthManager PlayerHealth;
    [SerializeField] private TextMeshProUGUI healthValueText;
    [SerializeField] private Image healthBar;
    private PhotonView pw;
    private Vector3 startPos;
    public static Action PlayerScore;
    public static Action EnemyScore;
    private void Start()
    {
        healthBar.fillAmount = 1;
        pw = GetComponent<PhotonView>();
        startPos = transform.position;
        GetComponent<PlayerManager>();
    }
    [PunRPC]
    public void Damage(float damageAmount,int actorNumber)
    {
        healthBar.fillAmount -= damageAmount;
        healthValueText.text = (healthBar.fillAmount * 100f).ToString();
        if(healthBar.fillAmount==0)
        {
            Die(actorNumber);
            print("Die");
        }
    }
    private void RespawnHealth()
    {
        healthBar.fillAmount = 1f;
        GetComponent<FpsCharacterController>().IsDead = false;
        print("Player respawn");
    }
    public void Die(int attackerActorNumber)
    {
        FpsCharacterController attacker = FindPlayerByActorNumber(attackerActorNumber);
        pw.RPC("ShowDeathMessage", RpcTarget.All, attacker.PlayerName, GetComponent<FpsCharacterController>().PlayerName);
        switch (pw.IsMine)
        {
            case true:
                if (EnemyScore != null)
                    EnemyScore();
                break;
            case false:
                if (PlayerScore != null)
                    PlayerScore();
                break;
        }
        if(!GameManager.GameOver)
            if (pw.IsMine)
                StartCoroutine(Respawn());
    }
    public void Score(FpsCharacterController killer)
    {
        killer.pw.RPC("PlayerKilled", RpcTarget.All);
    }
    [PunRPC]
    public void ShowDeathMessage(string playerName,string enemyName)
    {
        DeathMessageManager.Instance.ShowDeathMessageManager(playerName, enemyName);
    }
    private IEnumerator Respawn()
    {
        GetComponent<FpsCharacterController>().IsDead = true;
        if (PlayerHealth != null)
            PlayerHealth();
        yield return new WaitForSeconds(3f);
        transform.position = startPos;
        RespawnHealth();
    }
    private FpsCharacterController FindPlayerByActorNumber(int actorNumber)
    {
        foreach (FpsCharacterController p in FindObjectsOfType<FpsCharacterController>())
        {
            if (p.pw.Owner.ActorNumber==actorNumber)
            {
                return p;
            }
        }
        return null;
    }
    private void Update()
    {
        if (healthBar.fillAmount < 0.7f)
            healthBar.color = Color.red;
        else
            healthBar.color = Color.white;  
    }
}
