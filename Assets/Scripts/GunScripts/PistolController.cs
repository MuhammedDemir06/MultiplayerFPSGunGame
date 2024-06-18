using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolController : GunController
{
    RaycastHit hit;
    private void OnEnable()
    {
        FpsCharacterController.CharacterRespawn += GunRespawn;
    }
    private void OnDisable()
    {
        FpsCharacterController.CharacterRespawn -= GunRespawn;
    }
    private void Start()
    {
        animationManager = GetComponent<AnimationController>();
        //Default
        ManagerGun.Magazine = ManagerGun.MagazineDefault;
        ManagerGun.CurrentBullet = ManagerGun.CurrentBulletDefault;
        ManagerGun.MaxBullet = ManagerGun.MaxBulletDefault;
    }
    [PunRPC]
    private void Control()
    {
        if (!GetComponent<FpsCharacterController>().IsDead)
            if (Input.GetMouseButtonDown(0))
            {
                if (ManagerGun.CurrentBullet > 0)
                {
                    animationManager.GunShootAnim();
                    shoot.Play();
                    Ray ray = gameObject.GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit))
                    {
                        BulletHoleEffect(hit);
                    }
                    muzzleEffect.Play();
                    if (hit.collider != null)
                    {
                        if (hit.collider.GetComponent<PhotonView>() != null)
                        {
                            var photonView = hit.collider.GetComponent<PhotonView>();
                            FpsCharacterController attacker = GetComponentInParent<FpsCharacterController>();
                            if (photonView != null && attacker != null)
                            {
                                photonView.RPC("Damage", RpcTarget.All, Damage, attacker.ActorNumber);
                                print("Damage");
                            }
                        }
                    }
                }
                if (ManagerGun.CurrentBullet > 0)
                    ManagerGun.CurrentBullet -= 1;
            }
       
    }
    public void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            MagazineControl();
            if (!MagazineIsFull)
                if (ManagerGun.Magazine > 0)
                    StartCoroutine(ReloadTime());
                else
                    print("Insufficient Bullet");
        }
    }
    private IEnumerator ReloadTime()
    {
        animationManager.GunReloadAnim();
        reload.Play();
        yield return new WaitForSeconds(ManagerGun.ReloadTime);
        int requiredBulletNumber;
        requiredBulletNumber = ManagerGun.MaxBullet - ManagerGun.CurrentBullet;
        if (requiredBulletNumber < ManagerGun.Magazine)
        {
            ManagerGun.Magazine = ManagerGun.Magazine - requiredBulletNumber;
            ManagerGun.CurrentBullet = ManagerGun.CurrentBullet + requiredBulletNumber;
        }
        else
        {
            ManagerGun.CurrentBullet = ManagerGun.Magazine + ManagerGun.CurrentBullet;
            ManagerGun.Magazine = 0;
        }
    }
    private void BulletHoleEffect(RaycastHit hit)
    {
        if(GetComponent<PhotonView>().IsMine)
        {
            if (hit.collider.tag == "Player")
                bulletHole.GetComponent<BulletHoleControl>().EffectIndex = 1;
            else
                bulletHole.GetComponent<BulletHoleControl>().EffectIndex = 0;

            GameObject obj = PhotonNetwork.Instantiate(bulletHole.name, hit.point, Quaternion.LookRotation(hit.normal));
            obj.gameObject.transform.SetParent(hit.transform);
            Destroy(obj, deleteBulletHoleTime);
        }
    }
    private void MagazineControl()
    {
        if (ManagerGun.CurrentBullet == ManagerGun.MaxBullet)
            MagazineIsFull = true;
        else if (ManagerGun.CurrentBullet != ManagerGun.MaxBullet)
        {
            MagazineIsFull = false;
            ScopeController.IsScopeOpen = false;
        }
    }
    private void GunRespawn()
    {
        ManagerGun.Magazine = ManagerGun.MagazineDefault;
        ManagerGun.CurrentBullet = ManagerGun.CurrentBulletDefault;
        ManagerGun.MaxBullet = ManagerGun.MaxBulletDefault;
    }
    private void Update()
    {
        Control();
        Reload();
    }
}
