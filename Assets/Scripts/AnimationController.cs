using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [Header("Animations")]
    [SerializeField] private Animator animationBody;
    private FpsCharacterController characterManager;
    private void Start()
    {
        characterManager = GetComponent<FpsCharacterController>();
    }
    private void WalkAnim()
    {
        if(characterManager.IsMoveAnimation)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                animationBody.SetFloat("Walk", 0.8f);
                ScopeController.IsScopeOpen = false;
            }          
            else
                animationBody.SetFloat("Walk", 0.5f);
        }
        else
        {
            animationBody.SetFloat("Walk", 0.2f);
        }
    }
    public void GunReloadAnim()
    {
        animationBody.SetTrigger("Reload");
    }
    public void GunShootAnim()
    {
        animationBody.SetTrigger("Shoot");
    }
    private void Update()
    {
        WalkAnim();
    }
}
