using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameNetwork : MonoBehaviourPunCallbacks
{
    public static GameNetwork Instance;
    public int PlayersNumber;
    [Header("Player Prefab")]
    public Transform Player;
    [Header("Spawn Points")]
    public Transform PlayerSpawnPoint;
    public Transform EnemySpawnPoint;
    private void Start()
    {
        if(!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene(0); //return Menu
            return;
        }
        else
        {
            if (PhotonNetwork.IsConnectedAndReady)
            {
                SpawnPlayer();
            }     
            Debug.Log("Entered Room" + PhotonNetwork.CurrentRoom.Name);
            UpdatePlayerNumber();
        }
    }
    public void SpawnPlayer()
    {
        Transform spawnPoint = PhotonNetwork.CurrentRoom.PlayerCount == 1 ? PlayerSpawnPoint : EnemySpawnPoint;
        PhotonNetwork.Instantiate(Player.name, spawnPoint.position, Quaternion.identity);
    }
    public void UpdatePlayerNumber()
    {
        if (PhotonNetwork.CurrentRoom != null)
            PlayersNumber = PhotonNetwork.CurrentRoom.PlayerCount;
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerNumber();
        Debug.Log("New Player entered room: " + newPlayer.NickName);
    }
    public override void OnLeftRoom()
    {
        UpdatePlayerNumber();
    }
}
