using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : MonoBehaviour,Click
{
    public delegate void EnemyManager();
    public static EnemyManager HealthControl;
    public string EnemyName;
    [Range(0,20)][SerializeField] private float healthAmount;
    [SerializeField] private float damage;
    public void Shoot()
    {
        Health();
    }
    private void Health()
    {
        healthAmount -= damage;
        if(healthAmount<5)
        {
            if (HealthControl != null)
                HealthControl();
            print("Enemy Dead");
        }   
    }
}
