using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeController : MonoBehaviour
{
    [SerializeField] private Vector3 scopePos,startPos;
    [SerializeField] private float speed;
    [SerializeField] private Camera cam;
    public static bool IsScopeOpen;
    private void Start()
    {
        startPos = transform.localPosition;
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
            IsScopeOpen = !IsScopeOpen;
        switch (IsScopeOpen)
        {
            case true:
                transform.localPosition = Vector3.Slerp(transform.localPosition, scopePos, speed * Time.deltaTime);
                if (cam.fieldOfView > 40)
                    cam.fieldOfView -= 30 * Time.deltaTime;
                break;
            case false:
                transform.localPosition = Vector3.Slerp(transform.localPosition, startPos, speed * Time.deltaTime);
                if (cam.fieldOfView < 60)
                    cam.fieldOfView += 30 * Time.deltaTime;
                break;
        }
    }
}
