using System;
using System.Collections;
using UnityEngine;
using Cinemachine;
//using MalbersAnimations;

public class ThirdPersonController : MonoBehaviour
{
    [SerializeField] internal PlayerController playerController;
    [SerializeField] internal PlayerStats playerStats;
    [SerializeField] PlayerScript playerScript;
    [SerializeField] internal CharacterController controller;
    //[SerializeField] internal ColliderManager colliderManager;
    //[SerializeField] internal RayCasts rayCasts;
    [SerializeField] internal Transform cam;
    [SerializeField] internal float jumpHeight;
    [SerializeField] internal float DistToGround;
    [SerializeField] internal float gravityValue;
    [SerializeField] internal float speed;
    [SerializeField] internal float turnSmootheTime;
    [SerializeField] internal float speedModifier, maxSpeedModifier, minSpeedModifer;
    [SerializeField] internal GameObject leftFeet, rightFeet, obstacleRayCast;

    public CinemachineFreeLook vcam;
    public Vector3 playerVelocity;
    public bool isgrounded;
    public bool isfreeFall;
   [SerializeField] internal bool isobstacle;
    public float floatfreefall;
    public float num;  // raycast vertical movement
    public float num2; // front raycast
    public float num3; // back raycast
    public float num4; // raycast horizontal movement
    public int key;

    internal float turnSmoothVelocity;
    internal float flySmoothVelocity;

    internal float targetAngle, angle;

    internal RaycastHit hit,hit1,hit2,hit3,hit4;


    internal bool isMoving;
    internal bool isModified;
    internal bool isJumpPressed;
    internal bool isHoverMode;

    //private bool onGround;


    protected virtual void Start()
    {
       
    }

    protected virtual void Update()
    {
        if (!isfreeFall)
        {
            Walk();
            Jump();
        }
        SpeedModifier();
        GroundCheck();
        FreeFall();



        controller = playerScript.GetComponentInChildren<CharacterController>();// get controller
        //isobstacle = playerScript.colliderManager.rayCasts.isObstacle;
        

        Gravity();


        // onGround = playerController.TPS.isgrounded; // redundant
        isMoving = playerController.inputController.isMoving;
        isModified = playerController.inputController.isModified;
        isJumpPressed = playerController.inputController.isJumpPressed;
        isHoverMode = playerController.inputController.isHoverMode;

    }

    protected virtual void Gravity()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
        if (!(Input.GetKeyDown(KeyCode.F6)))
        {
            controller.Move(playerVelocity * Time.deltaTime);
        }
       
    }

    protected virtual void FreeFall()
    {
        if (playerVelocity.y < -floatfreefall && !isgrounded)
        {
            playerVelocity.y -= -1 * Time.deltaTime;
            isfreeFall = true;
        }
        if (playerVelocity.y < 0 && isgrounded)
        {
            playerVelocity.y = -floatfreefall;
            isfreeFall = false; /// responsible for landing animation
        }
        else if (playerVelocity.y>=0 && isfreeFall)
        {
            isfreeFall = false;
        }
    }

    protected virtual void Jump()
    {

        if (isJumpPressed && isgrounded && isobstacle)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight *- 1 * gravityValue);

        }
        else if (isJumpPressed && isgrounded && !isobstacle)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -1 * gravityValue);
 
        }
    }

    protected virtual void Walk()
    {
        CameraCalculations(out float targetAngle, out float angle);
        if (isMoving )
        {
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * playerStats.walkSpeed;
            //transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
           // vcam.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            if (!isobstacle)
            {
                controller.Move(moveDir.normalized * speed * speedModifier * Time.deltaTime);
            }
            else if (isobstacle)
            {
                controller.Move(moveDir.normalized * 0 * speedModifier * Time.deltaTime);
            }
        }
    }
    protected virtual void SpeedModifier()
    {
        if (isModified)
        {
            speedModifier = maxSpeedModifier;
        }
        else
        {
            speedModifier = minSpeedModifer;
        }
    }

    protected virtual void GroundCheck()
    {

        Ray landingRay = new Ray(leftFeet.transform.position, Vector3.down);
        Ray landingRay2 = new Ray(rightFeet.transform.position, Vector3.down);
        Debug.DrawRay(leftFeet.transform.position, Vector3.down * DistToGround);
        Debug.DrawRay(rightFeet.transform.position, Vector3.down * DistToGround);
        isgrounded = Physics.Raycast(landingRay, out hit, DistToGround);

        if (Physics.Raycast(landingRay, out hit, DistToGround) || Physics.Raycast(landingRay2, out hit, DistToGround) || controller.isGrounded)
        {
            isgrounded = true;
        }
        else
        {
            isgrounded = false;
        }
    }

    public virtual void CameraCalculations(out float targetAngle, out float angle)
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal , 0f, vertical).normalized;
        targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmootheTime);
    }


    /////////////////

    //
}
