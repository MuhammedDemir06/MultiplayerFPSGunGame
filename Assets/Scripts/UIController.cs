using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    [Header("Gun")]
    public GunManager ManagerGun;
    [Header("Gun Icon")]
    [SerializeField] private Image gunImage;
    [Header("Gun Magazine Control")]
    [SerializeField] private TextMeshProUGUI currentBulletText;
    [SerializeField] private TextMeshProUGUI magazineText;
    [Header("Screens")]
    [SerializeField] private GameObject deathScreen;
    private void Start()
    {
        //Icon
        gunImage.sprite = ManagerGun.IconUI;
    }
    private void GunMagazine()
    {
        currentBulletText.text = ManagerGun.CurrentBullet.ToString();
        magazineText.text = ManagerGun.Magazine.ToString();

        if (ManagerGun.CurrentBullet == 0)
            currentBulletText.color = Color.red;
        else
            currentBulletText.color = Color.white;
    }
    private void Update()
    {
        GunMagazine();
    }
    //Buttons
    public void LobbyButton()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
        print("Player Left Room");
    }
}
