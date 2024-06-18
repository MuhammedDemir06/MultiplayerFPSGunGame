using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("min=100,max=500")][SerializeField] private float mouseSens = 100f;
    private float inputX, inputY;
    private float xRotation;
    [SerializeField] private Transform player;
    [SerializeField] private Transform cam;
    private void GetInput()
    {
        inputX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        inputY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
    }
    private void Control()
    {
        player.Rotate(Vector3.up, inputX);
        xRotation -= inputY;
        xRotation = Mathf.Clamp(xRotation, -85, 85);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
    private void Update()
    {
        if(!player.GetComponent<FpsCharacterController>().IsDead)
        {
            GetInput();
            Control();
        }
    }
}
