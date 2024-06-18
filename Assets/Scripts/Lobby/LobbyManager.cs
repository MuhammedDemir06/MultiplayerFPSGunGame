using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LobbyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    private void Start()
    {
        PlayerPrefs.GetString("PlayerName");
    }
    public void SaveNameButton()
    {
        PlayerPrefs.SetString("PlayerName", playerNameText.text);
    }
}
