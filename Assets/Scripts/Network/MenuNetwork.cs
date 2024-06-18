using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class MenuNetwork : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI countOfPlayersText;
    public void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    private void Update()
    {
        countOfPlayersText.text = "Count Of Player:" + PhotonNetwork.CountOfPlayers.ToString();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Entered Server");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Entered Lobby");
    }
    public void StartButton()
    {
        PhotonNetwork.JoinOrCreateRoom("NewRoom", new RoomOptions { MaxPlayers = 4, IsOpen = true, IsVisible = true }, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
      //  if(PhotonNetwork.CountOfPlayers>=2)
        PhotonNetwork.LoadLevel("Game");
    }
}
