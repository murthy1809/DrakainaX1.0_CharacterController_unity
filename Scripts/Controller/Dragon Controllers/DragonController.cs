using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FIMSpace.FSpine;
//using UnityEngine.ProBuilder;

//TODO Fix jumping problem with land and dive animations
//TODO Align controller when Land
// TODO reduce airspeed to zero when script disabled// may create public airspeed variable
// Enable collisions while flying
// Eliminate pitch glitching while low flying;



public class DragonController : ThirdPersonController
{
    [SerializeField] int hoverheight;
    [SerializeField] internal GameObject rightArm, leftArm,baseObject;
    [SerializeField] internal bool isFlying;
    [SerializeField] internal bool isGliding;
    [SerializeField] internal float rollTolerance,rollSmootheTime, rollAngle =90;
    [SerializeField] internal GameObject dragon;
    [SerializeField] public float airSpeed,momentum,stamina;
    [SerializeField] internal int minairSpeed;
    [SerializeField] internal int maxairSpeed,maxStamina,minStamina;
    [SerializeField] internal float glideDecay;
    [SerializeField] internal float acceleration;
    [SerializeField] internal int momentumModifier;
    [SerializeField] internal float glideSpeedDecay;
    [SerializeField] internal float downTimeRight;
    [SerializeField] internal float keydowntime;
    [SerializeField] GameObject model;

    public UIFlightStats flightStats;
    private bool butOnlyOnce = false;
    bool spineAnimator = false;
    internal float startRollAngle;
    [SerializeField] float slopeAngleX, slopeAngleZ;
    Vector3 oldEulerAngle;
    float anglex;
    Component[] SpineAnimators;
    [SerializeField] Rigidbody rb;
    public GameObject target;
    float oldRotationAngley;
    

    int layerMask = 1 << 9;


    public float hovermodedisttoground, hovermodepostdisttoground, walkTurnSmootheTime, hoverTurnSmootheTime, currentVelocity;

    [SerializeField] internal Vector3 rbodyVelocity;
    [SerializeField] internal bool rfeet, lfeet, rarm, larm;
    private void OnEnable()
    {
        airSpeed = 0;
        playerController.inputController.isHoverMode = false;
        playerController.inputController.isCombatMode = false;
    }
    protected override void Start()
    {
        base.Start();
        oldEulerAngle = transform.rotation.eulerAngles;
        keydowntime = 1;

        oldRotationAngley = transform.rotation.y;
        SpineAnimators = GetComponents<FIMSpace.FSpine.FSpineAnimator>();
       // rb = GetComponent<Rigidbody>();
  

    }
    protected override void Update()
    {
        //Debug.Log(LayerMask.GetMask("Player"));
        base.Update();
        Hover();
        GroundCheck();
        Walk();
        Fly();
        Roll();
        AirSpeedLogic();
        MovementTime();
        Gravity();
        Jump();
        
        //rb.position = model.transform.position;

        //   SpineAnimator();
        flightStats.CurrentSpeed(airSpeed);
        flightStats.CurrentStamina(stamina);

        flightStats.SetMaxAirSpeed(maxairSpeed);
        flightStats.SetMinAirSpeed(minairSpeed);

        flightStats.SetMaxStamina(maxStamina);
        flightStats.SetMinStamina(minStamina);
        DragonFlightParameters();

        rbodyVelocity = rb.velocity;

//;
//      

        Walk();
        if(controller == null)
        {
            return;
        }


    }
    protected override void FreeFall()
    {
        CameraCalculations(out float targetAngle, out float angle);
        if (rb.velocity.y < -floatfreefall)
        {
         
            isfreeFall = true;
            transform.rotation = Quaternion.Euler(slopeAngleX, angle, 0);

        }
        else if (isfreeFall)
        {
            transform.Translate(Vector3.forward * 20*Time.deltaTime );
        }
    }
    protected override void Gravity()
    {
        return;
    }

    protected override void SpeedModifier()
    {
        if (isModified)
        {
            if (playerController.inputController.isCombatMode)
            {
                speedModifier = minSpeedModifer;
            }
            else
            {
                speedModifier = maxSpeedModifier;
            }
        }
        else
        {
            speedModifier = 1;
        }
    }
    protected override void Walk()
    {
        CameraCalculations(out float targetAngle, out float angle);
        if (!isMoving && !isFlying && isgrounded && !isHoverMode)
        {
            //transform.rotation = Quaternion.Euler(slopeAngleX, oldRotationAngley, slopeAngleZ);

            turnSmootheTime = walkTurnSmootheTime;

        }
        if (isMoving && !isFlying && isgrounded && !isHoverMode)
        {
            transform.rotation = Quaternion.Euler(slopeAngleX, angle, slopeAngleZ);
            turnSmootheTime = walkTurnSmootheTime;
            oldRotationAngley = angle;
            transform.Translate(Vector3.forward * speed*speedModifier * Time.deltaTime);

        }
    }

    protected virtual void Hover()
    {
        CameraCalculations(out float targetAngle, out float angle);
        if (isHoverMode)
        {
            isJumpPressed = false;
            isfreeFall = false;
            turnSmootheTime = hoverTurnSmootheTime;
            rb.useGravity = false;
            rb.velocity = new Vector3(0, 0, 0);
            oldRotationAngley = angle;
            if (isgrounded)
            {


                transform.Translate(Vector3.up * hoverheight * Time.deltaTime);
                DistToGround = hovermodedisttoground;
            }
            else 
            {
                hovermodedisttoground = hovermodepostdisttoground;
            }
            if (Input.GetAxisRaw("Hover") != 0 && !isMoving && !isgrounded)
            {

                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                transform.Translate(Vector3.up * Input.GetAxisRaw("Hover") * hoverheight * Time.deltaTime);
            }
            else if (Input.GetAxisRaw("Hover") == 0 && !isMoving && !isgrounded && airSpeed <= minairSpeed)
            {
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
            else if (isGliding && airSpeed >= minairSpeed)
            {
                gravityValue = Mathf.Lerp(0, -9.81f, glideDecay / airSpeed);
                //playerVelocity.y += Physics.gravity.y * Time.deltaTime;

                transform.Translate(rb.velocity.normalized* gravityValue*-1* Time.deltaTime);
            }

        }

        else
        {
           DistToGround = hovermodepostdisttoground;
            rb.useGravity = true;
            oldRotationAngley = angle;
        }
    }

    private void SpineAnimator()
    {

        foreach (FSpineAnimator spine in SpineAnimators)
        {
            if (isHoverMode)
            {
                spine.enabled = false;
            }
            else if (isgrounded && isMoving)
            {
                spine.enabled = true;
            }
        }
    }

    protected override void GroundCheck()
    {

        Ray LeftFeet = new Ray(leftFeet.transform.position, Vector3.down);
        Ray RightFeet = new Ray(rightFeet.transform.position, Vector3.down);
        Ray RightArm = new Ray(rightArm.transform.position, Vector3.down);
        Ray LeftArm = new Ray(leftArm.transform.position, Vector3.down);
        Debug.DrawRay(leftFeet.transform.position, Vector3.down * DistToGround);
        Debug.DrawRay(rightFeet.transform.position, Vector3.down * DistToGround);
        Debug.DrawRay(leftArm.transform.position, Vector3.down * DistToGround);
        Debug.DrawRay(rightArm.transform.position, Vector3.down * DistToGround);

        lfeet = Physics.Raycast(LeftFeet, out hit, DistToGround, ~layerMask);
        rfeet = Physics.Raycast(RightFeet, out hit, DistToGround, ~layerMask);
        rarm = Physics.Raycast(RightArm, out hit, DistToGround, ~layerMask);
        larm = Physics.Raycast(LeftArm, out hit, DistToGround, ~layerMask);

        //lfeet = Physics.Raycast(LeftFeet, out hit, DistToGround);
        //rfeet = Physics.Raycast(RightFeet, out hit, DistToGround);
        //rarm = Physics.Raycast(RightArm, out hit, DistToGround);
        //larm = Physics.Raycast(LeftArm, out hit, DistToGround);
        FineGrounding();

    }

    private void FineGrounding()
    {
        int q, w, e, r;
        if (lfeet)
        {
            q = 1;
        }
        else
        {
            q = 0;
        }
        if (larm)
        {
            w = 1;
        }
        else
        {
            w = 0;
        }
        if (rfeet)
        {
            e = 1;
        }
        else
        {
            e = 0;
        }
        if (rarm)
        {
            r = 1;
        }
        else
        {
            r = 0;
        }
        if (!isHoverMode)
        {
            if (!lfeet && !rfeet && (rarm && larm))
            {
                isgrounded = true;
                isfreeFall = true;

            }
            else if (lfeet && rfeet && (!rarm && !larm))
            {
                isfreeFall = true;
                isgrounded = false;

            }
            else if (q + w + e + r >= 3)
            {
                isgrounded = true;
                isfreeFall = false;
            }
            else if (q + w + e + r == 0)
            {
                isfreeFall = true;
                isgrounded = false;
            }
            else if (lfeet&&larm || rfeet&& rarm)
            {
                isgrounded = true;
            }
            else if (!lfeet && !rfeet && !larm && !rarm)
            {
                isgrounded = false;

            }
            else
            {
                isgrounded = false;
            }
        }
        else if (isHoverMode)
        {
            if (lfeet || rfeet || larm || rarm)
            {
                isgrounded = true;

            }
            else
            {
                isgrounded = false;
            }
        }
    }

    protected virtual void Fly()
    {
      
        Quaternion roll = Quaternion.AngleAxis(startRollAngle, Vector3.forward);
        Quaternion rotation = Quaternion.LookRotation(-cam.position + transform.position, Vector3.up);
        Quaternion pitch = Quaternion.LookRotation(-cam.position + transform.position, Vector3.forward);
        if (isHoverMode && isMoving)
        {
           
            isFlying = true;
            isGliding = false;
            isfreeFall = false;
            transform.Translate(Vector3.forward*airSpeed * Time.deltaTime,Space.Self);
            Vector3 pos = transform.position;
            if (isgrounded )
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, roll.eulerAngles.z);
                if(GetComponent<DragonColliderManager>().dragonObstacleDetect)
                    pos.y = Terrain.activeTerrain.SampleHeight(transform.position) + 3f;
                transform.position = pos;
                if (isModified)
                {
                    transform.Translate(Vector3.up * 3, Space.Self);

                }
            }
            else if (Input.GetButton("SecondaryAttack"))
            {
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            }
            else if(!Input.GetButton("SecondaryAttack"))
            {

                if (!GetComponent<VCam>().freeLook)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(pitch.eulerAngles.x, transform.eulerAngles.y, roll.eulerAngles.z);
                }

            }
        }
        else if (isHoverMode && airSpeed >= minairSpeed && !isMoving)
        {

            oldRotationAngley = transform.rotation.eulerAngles.y;
            // controller.Move(moveDir.normalized * airSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * airSpeed * Time.deltaTime,Space.Self);
            isGliding = true;
            isFlying = true;
            isfreeFall = false;
            oldRotationAngley = rotation.y;
            Vector3 pos = transform.position;
            if (isgrounded )
            {
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
                if (GetComponent<DragonColliderManager>().dragonObstacleDetect)
                    pos.y = Terrain.activeTerrain.SampleHeight(transform.position) + 3f;
                //pos.y = Terrain.activeTerrain.SampleHeight(transform.position) + 3f;
                transform.position = pos;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
            }
            else if (Input.GetButton("SecondaryAttack"))
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
            }
            else
            {

                if (!GetComponent<VCam>().freeLook)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(pitch.eulerAngles.x, transform.eulerAngles.y, roll.eulerAngles.z);
                }
            }
        }
        else
        {
            isFlying = false;
            isGliding = false;
            //airSpeed = 0;
        }
    }

    private void Roll()
    {
        if (oldEulerAngle.y - transform.rotation.eulerAngles.y >= rollTolerance)
        {
           // startRollAngle = Mathf.SmoothDamp(transform.eulerAngles.z, rollAngle, ref turnSmoothVelocity, rollSmootheTime);

            startRollAngle = Mathf.LerpAngle(transform.eulerAngles.z, rollAngle, rollSmootheTime);
            
        }
        else if (oldEulerAngle.y - transform.rotation.eulerAngles.y <= -rollTolerance)
        {
            //startRollAngle = Mathf.SmoothDamp(transform.eulerAngles.z, -rollAngle, ref turnSmoothVelocity, rollSmootheTime);
            startRollAngle = Mathf.LerpAngle(transform.eulerAngles.z, -rollAngle, rollSmootheTime);

        }
        else
        {
           // startRollAngle = Mathf.SmoothDampAngle(transform.eulerAngles.z, 0, ref turnSmoothVelocity, rollSmootheTime);
            startRollAngle = Mathf.LerpAngle(transform.eulerAngles.z, 0, rollSmootheTime);
            rollAngle = 50;
        }
        oldEulerAngle = transform.rotation.eulerAngles;

    }

    protected override void Jump()
    {
        if (isJumpPressed)
        {
            transform.Translate(Vector3.up * num *Time.deltaTime);
            //transform.Translate(Vector3.forward * 10 * Time.deltaTime);
        }
     
    }
    protected virtual float AirSpeedLogic()
    {
        // smaller the smoothetime faster the decay
        if (isHoverMode && isMoving)
        {
            airSpeed = Mathf.SmoothDamp(airSpeed, maxairSpeed, ref flySmoothVelocity, acceleration); //acceleration 
        }
        if (isHoverMode && airSpeed >= minairSpeed && !isMoving)
        {
            airSpeed = Mathf.SmoothDamp(airSpeed, 0, ref flySmoothVelocity, momentum); // momentum = acceleration *keydowntime
        }
        if (isGliding && isModified)
        {
            airSpeed = Mathf.SmoothDamp(airSpeed, 0, ref flySmoothVelocity, keydowntime / acceleration); // airbrakes
        }
        if (!isHoverMode && !isgrounded && isfreeFall)
        {
            airSpeed = Mathf.SmoothDamp(airSpeed, 0, ref flySmoothVelocity, acceleration);// acceleration function of pitch
        }
        if (isgrounded && !isHoverMode)
        {
            airSpeed = 0;
        }
        return airSpeed; 
    }

    protected virtual void DragonFlightParameters()
    {
        momentum = (airSpeed) / momentumModifier;
        if (isMoving && isFlying)
        {
            if(stamina <= minStamina)
            {
                stamina = minStamina;
            }
            else
            {
                stamina = stamina - acceleration/50;
            }
           
        }
        if (!isMoving && (isHoverMode))
        {
            if (stamina >=maxStamina)
            {
                stamina = maxStamina;
            }
            else
            {
                stamina = stamina + acceleration/50;
            }          
        }

        if (isgrounded)
        {
            if(stamina >= maxStamina)
            {
                stamina = maxStamina;
            }
            else
            {

            }
            stamina = stamina + acceleration / 50;
        }

    }
    private void MovementTime()
    {
     
        if (isMoving)
        {
            downTimeRight += Time.deltaTime;
            butOnlyOnce = true;

        }
        if (!isMoving && butOnlyOnce)
        {
            butOnlyOnce = false;
            keydowntime = downTimeRight; // okay, now set
            downTimeRight = 0; // and reset downTimeRight
        }
    }


}