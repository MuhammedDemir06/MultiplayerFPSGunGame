using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class GunController : MonoBehaviour
{
    [Tooltip("Gun")]
    public float Damage;
    [SerializeField] protected float shootDistance;
    //[SerializeField] protected Transform gunShootPos;
    protected AnimationController animationManager;
    [Header("Gun TYPE")]
    public GunManager ManagerGun;
    public bool MagazineIsFull;
    [SerializeField] protected ParticleSystem muzzleEffect;
    [SerializeField] protected GameObject bulletHole;
    [SerializeField] protected float deleteBulletHoleTime;
    [Header("Sound Manager")]
    [SerializeField] protected AudioSource shoot;
    [SerializeField] protected AudioSource reload;
    public static string EnemyName; 
}
