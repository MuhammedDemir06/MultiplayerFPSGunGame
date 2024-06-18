using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Gun",menuName = "Create Gun")]
public class GunManager : ScriptableObject
{
    public string Name;
    public Sprite IconUI;
    //Gun Features
    [HideInInspector] public int Magazine;
    [HideInInspector] public int CurrentBullet;
    [HideInInspector] public int MaxBullet;
    public float ReloadTime;
    //------------------
    [Header("Default Features")]
    public int MagazineDefault;
    public int CurrentBulletDefault;
    public int MaxBulletDefault;
}
