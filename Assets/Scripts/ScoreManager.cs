using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    [SerializeField] private TextMeshProUGUI playerScoreText, enemyScoreText;
    [SerializeField] private FpsCharacterController playerManager;
    public int PlayerScore = 0;
    public int EnemyScore = 0;
    [Header("Timer")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private int seconds,minnute;

    private void OnEnable()
    {
        HealthController.PlayerScore += ScoreUpdatePlayer;
        HealthController.EnemyScore += ScoreUpdateEnemy;
    }
    private void OnDisable()
    {
        HealthController.PlayerScore -= ScoreUpdatePlayer;
        HealthController.EnemyScore -= ScoreUpdateEnemy;
    }
    private void Start()
    {
        Instance = this;
        playerScoreText.text = PlayerScore.ToString();
        enemyScoreText.text = EnemyScore.ToString();
        PlayTime();
    }
    public void ScoreUpdatePlayer()
    {
        PlayerScore++;
        playerScoreText.text = PlayerScore.ToString();
    }
    public void ScoreUpdateEnemy()
    {
        EnemyScore++;
        enemyScoreText.text = EnemyScore.ToString();
    }
    private void PlayTime()
    {
        timerText.text = minnute.ToString() + ":" + seconds.ToString();
        StartCoroutine(UpdateTimerForSeconds());
        StartCoroutine(UpdateTimerForMinnute());
    }
    private IEnumerator UpdateTimerForSeconds()
    {
        while(seconds>-1)
        {
            if(!GameManager.GameOver)
            {
                if (seconds == 0)
                    seconds = 60;
                seconds--;
                timerText.text = minnute.ToString() + ":" + seconds.ToString();
                yield return new WaitForSeconds(1f);
            }
        }
        yield return null;
    }
    private IEnumerator UpdateTimerForMinnute()
    {
        while(minnute>0)
        {
            if (!GameManager.GameOver)
            {
                minnute--;
                timerText.text = minnute.ToString() + ":" + seconds.ToString();
                yield return new WaitForSeconds(60f);
                seconds = 60;
            }
        }
        yield return null;
    }
    private void Update()
    {
        if (PlayerScore == 40 || EnemyScore == 40)
            GameManager.GameOver = true;
    }
}
