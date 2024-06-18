using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraController))]
[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(HealthController))]
public class FpsCharacterController : MonoBehaviour
{
    public delegate void FPSCharacterManager();
    public static FPSCharacterManager CharacterRespawn;
    public static FpsCharacterController LocalPlayerInstance;

    public bool IsDead;
    public string PlayerName;
    private CharacterController CC;
    private float moveX, moveZ;
    private float moveSpeed = 3f;
    private Vector3 velocity;
    [SerializeField] private float mass = 20f;
    [SerializeField] private float ground = -10f;
    public bool IsGrounded;
    [SerializeField] private GameObject checkGround;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpPower;
    [HideInInspector] public bool IsMoveAnimation;
    [Header("Sound Manager")]
    [SerializeField] private AudioSource walk;
    public int ActorNumber;
    [HideInInspector]public PhotonView pw;
    private void Awake()
    {
        PlayerName = PlayerPrefs.GetString("PlayerName");
    }
    private void Start()
    {
        LocalPlayerInstance = this;
        CC = GetComponent<CharacterController>();
        ActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        pw = GetComponent<PhotonView>();
    }
    private void GetInput()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
    }
    private void Move()
    {
        //Is ground
        IsGrounded = Physics.CheckSphere(checkGround.transform.position, groundDistance, groundLayer);
        if (velocity.y < 0 && IsGrounded)
            velocity.y = -1f;
        //Move
        var move = transform.forward * moveZ + transform.right * moveX;
        CC.Move(move * moveSpeed * Time.deltaTime);
        //Ground 
        velocity.y += ground * mass * Time.deltaTime;
        CC.Move(velocity * Time.deltaTime);
        //Jump
        if (IsGrounded)
            if (Input.GetKeyDown(KeyCode.Space))
                velocity.y = Mathf.Sqrt(ground * -2 * jumpPower);
        //Fast walk
        if (Input.GetKey(KeyCode.LeftShift))
            moveSpeed = 4.5f;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            moveSpeed = 3f;
        //Is Move
        if (moveX > 0.1f || moveZ > 0.1f || moveX < -0.1f || moveZ < -0.1f)
        {
            IsMoveAnimation = true;
            walk.gameObject.SetActive(true);
        }
        else
        {
            IsMoveAnimation = false;
            walk.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (!IsDead)
        {
            GetInput();
            Move();
        }
    }
}
