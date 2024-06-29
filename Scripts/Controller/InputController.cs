using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
   [SerializeField] PlayerController playerController;
   [SerializeField] MasterScript MS;
    public bool isMoving;
    public bool isForward;
    public bool isBackward;
    public bool isLeft;
    public bool isRight;
    public bool isModified;
    public bool isFalling;
    public bool isJumpPressed;
    public bool isTurning;
    public bool onGround;
    public bool isAction;
    public bool isFlying;
    public bool isAirBrake;
    public bool isCondition;
    public bool isCrouch = false;
    public bool isHoverMode =false;
    public string directions;
    public bool isClimbing;
    private float pressDuration = 0f;

    // combat//
    public bool isPrimaryAttack,isWeaponDischarge;
    public bool isSecondaryAttack;
    public bool isCombatMode = false;
    public bool isSheating,isloadingArrow;
    public bool arrowLoaded;
    public bool fistEquip , swordEquip, bowEquip;
    [SerializeField] bool previousjump;


    [SerializeField]  bool currentjump = true;

    private DragonController DC;
    public bool isSliding = false;
    public bool canSlideAgain = true;

    void Start()
    {
        DC = GetComponent<DragonController>();
    }

    void Update()
    {
        ControlInputs();
        CombatInputs();
        onGround = playerController.TPS.isgrounded;
        isFalling = playerController.TPS.isfreeFall;
        if(GetComponent<HumanoidColliderManger>() == null)
        {
            return;
        }
        else
        {
            isClimbing = GetComponent<HumanoidColliderManger>().isClimbing;
        }
       
           
    }
    private void CameraCalculations( out Vector3 direction)
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;
    }
    private void ControlInputs()
    {
        CameraCalculations(out Vector3 direction);
        if(MS.charcterIndicator == "human")
        {
            HumanMovement(direction);
        }
        else if (MS.charcterIndicator == "dragon")
        {
            DragonMovement(direction);
        }


    }

    private void DragonMovement(Vector3 direction)
    {
        if (direction.magnitude >= 0.1)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        if (Input.GetButtonDown("Jump") && ((!isFlying && isModified && isMoving)))
        {
            isJumpPressed = true;
            currentjump = true;
            previousjump= false; 
        }
        if (isJumpPressed && isFalling)
        {
            isJumpPressed = false;
            currentjump = false;
        }
        if (Input.GetButton("Modified"))
        {
            isModified = true;
        }
        else /*if(!isPrimaryAttack && Input.GetButtonUp("Modified"))*/
        {
            isModified = false;
        }
        HoverMode();
        if (GetComponent<DragonController>().isFlying)
        {
            isFlying = true;
            isFalling = false;
        }
        else
        {
            isFlying = false;
        }
        if (GetComponent<DragonController>().isGliding &&
           GetComponent<DragonController>().airSpeed > GetComponent<DragonController>().minairSpeed &&
           isModified)
        {
            isAirBrake = true;
        }
        else
        {
            isAirBrake = false;
        }
        if (Input.GetButton("Action"))
        {
            isAction = true;
           
        }
        else
        {
            isAction = false;
            
        }
        if (Input.GetButton("SecondaryAttack"))
        {
            isSecondaryAttack = true;
        }
        else
        {
            isSecondaryAttack = false;
        }
    }

    private void HoverMode()
    {
        if (Input.GetButtonDown("HoverMode") && onGround && !isMoving)
        {
            isHoverMode = true;
        }

        if (Input.GetButtonDown("HoverMode") && (!onGround) && isHoverMode)
        {
            isHoverMode = false;
        }

        if (isFalling)
        {
            if(!isHoverMode && Input.GetButtonDown("HoverMode"))
            {
                isHoverMode = true;
                isFalling = false;
            }
        }
    }

    private void HumanMovement(Vector3 direction)
    {
        Moving(direction);
        if (!isCombatMode)
        {
            if (Input.GetButtonDown("Jump") && onGround)
            {
                isJumpPressed = true;
                isCrouch = false;
            }
            else if (!onGround)
            {
                isJumpPressed = false;
            }
        }

        if (isCombatMode)
        {
            if (Input.GetButtonDown("Jump") )
            {
                isJumpPressed = true;
                isCrouch = false;
            }
            //else 
            //{
            //    isJumpPressed = false;
            //}
        }


        if (Input.GetButton("Modified"))
        {
            isModified = true;
        }
        else
        {
            isModified = false;
        }
        if (Input.GetButtonDown("Crouch"))
        {
            isCrouch = !isCrouch;
        }
        if (Input.GetButton("Action") && canSlideAgain)
        {
            isAction = true;
        }
        else if (Input.GetButton("Action") && !canSlideAgain)
        {
            isAction = false;
        }
        else
        {
            isAction = false;
        }

        if (isAction && isMoving && isModified && !isSliding)
        {
            StartCoroutine(Slide());

            // pressDuration += Time.deltaTime;
        }
        //if (isAction == false)
        //{
        //    canSlideAgain = true;
        //}
        IEnumerator Slide()
        {          
            canSlideAgain = true;
            yield return new WaitForSeconds(2);
            canSlideAgain = false;
            yield return new WaitForSeconds(2);
            canSlideAgain = true;
        }
        isHoverMode = false;
    }

    private void CombatInputs()
    {
        HumanCombatInput();

        DragonCombatInput();

    }

    private void DragonCombatInput()
    {
        if (MS.charcterIndicator == "dragon")
        {
            if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Alpha3))
            {
                isCombatMode = true;
            }
            else if (Input.GetButtonDown("HoistWeapons"))
            {
                isCombatMode = false;
            }
            if (isCombatMode && Input.GetButton("PrimaryAttack"))
            {
                isPrimaryAttack = true;
                isWeaponDischarge = true;
            }

            if(isCombatMode && !Input.GetButton("PrimaryAttack"))
            {
                isWeaponDischarge = false;
            }
            //if (isCombatMode && Input.GetButton("SecondaryAttack"))
            //{
            //    isSecondaryAttack = true;
            //}
            //else
            //{
            //    isSecondaryAttack = false;
            //}
            if (isMoving && onGround && !isFlying)
            {
                isPrimaryAttack = false;
            }
        }
    }

    private void HumanCombatInput()
    {
        if (MS.charcterIndicator == "human")
        {
            if (isCombatMode && Input.GetButtonDown("PrimaryAttack"))
            {
                if (bowEquip)
                {
                    if (Input.GetButton("PrimaryAttack"))
                    {
                        Debug.Log("LoadArrow");
                        isPrimaryAttack = true;
                        isloadingArrow = true;
                        arrowLoaded = true;
                    }
                    if (arrowLoaded)
                    {
                        if (Input.GetButton("PrimaryAttack"))
                        {
                            isPrimaryAttack = true;
                            Debug.Log("DrawArrow");
                            isloadingArrow = true;
                        }
                    }

                    if (Input.GetButtonUp("PrimaryAttack"))
                    {
                        Debug.Log("ShootArrow");
                        isPrimaryAttack = true;
                        isloadingArrow = false;
                        arrowLoaded = false;
                    }
                }
                else
                {
                    isPrimaryAttack = true;
                }
            }
            if (fistEquip || swordEquip)
            {
                isCrouch = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (!swordEquip) // !!bowEquip && !!magicEqiup
                {
                    fistEquip = !fistEquip;
                    isCombatMode = !isCombatMode;
                }
             
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                fistEquip = false;
                isCombatMode = true;
                isSheating = true;
                swordEquip = true;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                fistEquip = false;
                swordEquip = false;
                isCombatMode = true;
                isSheating = true;
                bowEquip = true;               
            }

            if (Input.GetButton("Holster"))
            {
                isCombatMode = false;
                isSheating = true;
                swordEquip = false;
                bowEquip = false;
            }

                //if (isSheating)
                //{
                //    fistEquip = false;
                //}

            if (Input.GetButton("SecondaryAttack"))
            {
                isSecondaryAttack = true;
            }
            else
            {
                isSecondaryAttack = false;
            }

            if (isCombatMode)
            {
                isCrouch = false;
            }
        }
    }

    private void Moving(Vector3 direction)
    {
        if (Input.GetKey(KeyCode.W)) 
        {
            isMoving = true;
            directions = "W";
        }
        else if (Input.GetKey(KeyCode.A))
        {
            isMoving = true;
            directions = "A";
        }
        else if (Input.GetKey(KeyCode.S))
        {
            isMoving = true;
            directions = "S";
        }
        else if (Input.GetKey(KeyCode.D))
        {
            isMoving = true;
            directions = "D";
        }

        else
        {
            isMoving = false;
            directions = "None";
        }
    }
}
