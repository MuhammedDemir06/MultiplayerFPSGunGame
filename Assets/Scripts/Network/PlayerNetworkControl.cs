using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkControl : MonoBehaviour
{
    private PhotonView view;
    [Header("Objects")]
    [SerializeField] private GameObject playerUI;
    private void Start()
    {
        view = GetComponent<PhotonView>();
        Photon();
    }
    private void Photon()
    {
        if(!view.IsMine)
        {
            GetComponentInChildren<ScopeController>().enabled = false;
            GetComponent<FpsCharacterController>().enabled = false;
            GetComponent<CameraController>().enabled = false;
            GetComponent<GunController>().enabled = false;
            GetComponent<HealthController>().enabled = false;
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
            playerUI.gameObject.GetComponent<UIController>().enabled = false;
            playerUI.SetActive(false);
        }
    }
}
