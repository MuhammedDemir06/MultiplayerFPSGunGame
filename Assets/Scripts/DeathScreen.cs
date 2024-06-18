using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DeathScreen : MonoBehaviour
{
    [SerializeField] private float counter = 3f;
    [SerializeField] private TextMeshProUGUI deathTimeText;
    [SerializeField] private GameObject deathScreen;
    private void OnEnable()
    {
        HealthController.PlayerHealth += DeathScreenTime;
    }
    private void OnDisable()
    {
        HealthController.PlayerHealth -= DeathScreenTime;
    }
    public void DeathScreenTime()
    {
        counter = 4f;
        deathScreen.gameObject.SetActive(true);
        StartCoroutine(DeathTimer());
    }
    private IEnumerator DeathTimer()
    {
        while(!GameManager.GameOver)
        {
            if (counter > 0)
                counter--;
            yield return new WaitForSeconds(1f);
        }
    }
    private void Update()
    {
        deathTimeText.text = counter.ToString();
        if (counter == 0)
            deathScreen.SetActive(false);
    }
}
