using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniversalMobileController;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private CharacterController controller;
    private Animator animator;


    [Header("References : ")]
    [Space]
    public Vector3 DefaultCameraPosition;
    public Vector3 RunCameraPosition;
    public Transform HandleTransform;
    public FloatingJoystick joyStick;
    public SpecialTouchPad TouchPad;
    public Transform Direction;
    public Transform CameraHook;

    [Header("Player States : ")]
    [Space]
    public bool isAlive = true;
    public bool HandlingObject;
    public bool isGrounded;
    public bool isMoving;
    public bool CanMove = true;
    public bool CanReceiveDamage = true;
    public bool isRunning;

    [Header("Shooter/Combat System : ")]
    [Space]
    
    public WeaponSettings weaponSettings;
    public float SmoothRotationOnShoot = 1f;
    public float ShootDelay = 0.15f;
    public float DelayAfterPunch = 0.4f;
    public bool isArmed;
    public bool isShooting;


    [Header("Shooter Effects :  ")]
    [Space]
    public GameObject MuzzleEffect;


    [Header("AudioSource : ")]
    [Space]
    public AudioSource ShootAudioSouce;
    public AudioSource FootStepAudioSource;
    public AudioClip[] FootStepAudioClips;
    public int FootStepIndex;


    [Header("Movement Behaviour : ")]
    [Space]
    public float RotationSmooth = 0.2f;
    public float ResetFootStepTime = 0.15f;
    public float JumpHeight = 5f;
    public float Raydistance = 1f;
    public float MouseSpeed = 2f;
    [HideInInspector]public float NormalSpeed = 4f;
    public float MovementSpeed = 4f;
    public float RunSpeed = 4f;
    public float SettleX;
    public float SettleY;
    public float SettleZ;
    public float CameraFollowSmooth = 0.3f;
    #region private
    private Vector3 playerVelocity;
    private Vector3 CameraFollowVelocity;
    private float resetFootStepTime;
    public float MouseX;
    public float MouseY;
    public float smoothmath;
    private Vector3 smoothvel1;
    private Vector3 smoothvel2;
    private Vector3 rot2;
    private float shootdelay;
    private Vector3 move;
    #endregion

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        Movement();
        CameraController();
        States();
    }
    void Movement()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up * -1, out hit, Raydistance))
        {
            isGrounded = true;
        }
        else {
            isGrounded = false; 
        }
        /*float InputX = joyStick.Horizontal;
        float InputY = joyStick.Vertical;*/
        float InputX = Input.GetAxis("Horizontal");
        float InputY = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(InputX * -1, 0, InputY * -1);
        move = Direction.TransformDirection(input);
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2;
        }
        if(CanMove)controller.Move(move * Time.deltaTime * NormalSpeed);
        if (input.magnitude >= 0.1f)
        {
            
            isMoving = true;
            animator.SetBool("isRunning", true);
            if (Time.time >= resetFootStepTime && isGrounded)
            {
                FootStepAudioSource.PlayOneShot(FootStepAudioClips[FootStepIndex]);
                resetFootStepTime = Time.time + ResetFootStepTime;
            }
            else if (FootStepAudioSource.isPlaying)
            {
                if (FootStepIndex < FootStepAudioClips.Length - 1)
                    FootStepIndex++;
                else
                    FootStepIndex = 0;
            }
        }
        
        else
        {
            isMoving = false;
            animator.SetBool("isRunning", false);
        }
        var CharacterRotation = Camera.main.gameObject.transform.rotation;
        CharacterRotation.x = 0;
        CharacterRotation.z = 0;
        
        
        if(isMoving && !isShooting) transform.rotation = Quaternion.Slerp(transform.rotation ,Quaternion.LookRotation(move) , RotationSmooth);
        playerVelocity.y -= 9.8f * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }
    void States()
    {
        /*
        if (Input.GetMouseButton(0) && Time.time >= shootdelay)
        {
            shootdelay = Time.time + ShootDelay;
            Debug.Log("Hit");
            if (isArmed)
            {
                //Shoot with weapon
                Shoot();
                isShooting = true;
            }
            else
            {
                //Hit with Hand
                CanMove = false;
                animator.SetTrigger("Punch");
                Invoke("ResetBehaviourAfterPunch", DelayAfterPunch);
            }
        }
        
        else if(!Input.GetMouseButton(0))
        {
            isShooting = false;
        }
        */
        if (isShooting)
        {
            animator.SetBool("isShooting" , true);
        }
        else {
            animator.SetBool("isShooting", false);
        }

        if(Input.GetKeyDown(KeyCode.Q) && !isArmed)
        {
            PickUpWeapon(null);
        }

        if (Input.GetKeyDown(KeyCode.Q) && isArmed)
        {
            DropWeapon();
        }


        if (isGrounded)
        {
            animator.SetBool("Jump", false);
        }
        else
        {
            animator.SetBool("Jump", true);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }
        else 
        {
            //if (move.magnitude == 0) {
                isRunning = false;
         //   }
        }
        if (isRunning)
        {
            animator.SetBool("isSprinting", true);
            NormalSpeed = RunSpeed;
            Camera.main.gameObject.transform.localPosition = Vector3.SmoothDamp(Camera.main.gameObject.transform.localPosition, RunCameraPosition , ref smoothvel1,0.15f);
        }
        else
        {
            animator.SetBool("isSprinting", false);
            NormalSpeed = MovementSpeed;
            Camera.main.gameObject.transform.localPosition = Vector3.SmoothDamp(Camera.main.gameObject.transform.localPosition, DefaultCameraPosition, ref smoothvel2, 0.15f);
        }
    }

    void ResetBehaviourAfterPunch()
    {
        CanMove = true;
    }
    void CameraController()
    {
        CameraHook.position = Vector3.SmoothDamp(CameraHook.position, transform.position + new Vector3(SettleX,
            SettleY, SettleZ), ref CameraFollowVelocity, CameraFollowSmooth);
        /*MouseX += TouchPad.touchPadInput.x * MouseSpeed;
        MouseY += TouchPad.touchPadInput.y * MouseSpeed;*/
        MouseX += Input.GetAxis("Mouse X") * MouseSpeed;
        MouseY += Input.GetAxis("Mouse Y") * MouseSpeed;
        Vector3 mouse = new Vector3(MouseY, MouseX, 0);
        CameraHook.rotation = Quaternion.Euler(mouse);
        Direction.rotation = Quaternion.Euler(new Vector3(0, mouse.y, 0));
        
    }
    public void Jump()
    {
        if (!isGrounded) return;
        playerVelocity.y += JumpHeight ;
    }
    public void Run()
    {
        if (move.magnitude == 0) return;
        isRunning = true;
        
    }

    public void Shoot()
    {
        ShootAudioSouce.PlayOneShot(weaponSettings.ShootAudioClip);
        float roty = Mathf.SmoothDamp(transform.eulerAngles.y, Camera.main.gameObject.transform.eulerAngles.y, ref smoothmath,SmoothRotationOnShoot);
        Vector3 rot = new Vector3(0, roty, 0);
        //transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles, rot, ref rot2, SmoothRotationOnShoot);
        transform.eulerAngles = rot;
        Instantiate(MuzzleEffect, GetComponentInChildren<Muzzle>().transform.position, GetComponentInChildren<Muzzle>().transform.rotation);
        RaycastHit hit = default(RaycastHit);
        if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward, out hit, 999f))
        {
            if (hit.collider.gameObject.tag == "val")
            {
                //Playe Hit val
            }
        }
    }
    public void PickUpWeapon(WeaponSettings weapon)
    {
        weaponSettings = weapon;
        isArmed = true;
    }

    public void DropWeapon()
    {
        isArmed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            FindObjectOfType<UIManager>().EndScreen.SetActive(true);
        }
    }
}
