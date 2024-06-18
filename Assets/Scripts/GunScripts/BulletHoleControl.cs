using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoleControl : MonoBehaviour
{
    [Header("Hit Effects")]
    [SerializeField] private List<GameObject> hitEffects;
    public int EffectIndex;
    private void Start()
    {
        hitEffects[EffectIndex].SetActive(true);
    }
}
