using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GameManager : MonoBehaviour
{
    public static bool GameOver;
    public static bool CursorLock;
    private void Start()
    {
        CursorLock = true;
    }
    private void CursorController()
    {
        switch (CursorLock)
        {
            case true:
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case false:
                Cursor.lockState = CursorLockMode.None;
                break;
        }
        if (GameOver)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        CursorController();
    }
}
