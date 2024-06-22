using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

[CreateAssetMenu(fileName ="AnimationObject",menuName = "AnimationCards/Movement")]
public class AnimationObject : ScriptableObject
{
    
    public enum Options
    {
        NO,YES, OR
    }
    public new string name;
    public ClipTransition AnimClip;
    public bool applyRootMotion;
    private  bool onGround;
    private  bool isMoving;
    private  bool isModified;
    private  bool isFalling;
    private  bool isJumpPressed;
    private bool obstacleDetect;
    private bool isClimbing;
    private bool isHovermode;
    private bool isFlying;
    private bool isCrouch;
    private bool isAction;
    private string directions;
    // Combat
    private bool isPrimaryAttack;
    private bool isCombatMode;

    public bool ISMOVING
     { 
         get
         {
         if (IsMoving == Options.YES)
         {
           isMoving= true;

         }
         else if (IsMoving == Options.NO)
         {
           isMoving = false;          
         }
         else if (IsMoving == Options.OR)
         {
            isMoving = FindObjectOfType<InputController>().isMoving;
         }
         else
         {
             isMoving = false;
         }
         return isMoving; 
         }
        set
        {
            isMoving = value;
        } 
    }    
    
    public bool ISGROUNDED
     { 
         get
         {
         if (IsGrounded == Options.YES)
         {
           onGround = true;

         }
         else if (IsGrounded == Options.NO)
         {
           onGround = false;          
         }
         else if (IsGrounded == Options.OR)
         {
                onGround = FindObjectOfType<InputController>().onGround;
         }
         else
         {
             onGround = false;
         }
         return onGround; 
         }
        set
        {
            onGround = value;
        } 
    }
    public bool ISMODFIED
     { 
         get
         {
         if (IsModified == Options.YES)
         {
           isModified = true;

         }
         else if (IsModified == Options.NO)
         {
           isModified = false;          
         }
         else if(IsModified == Options.OR)
            {
                isModified = FindObjectOfType<InputController>().isModified;
            }
         else
         {
             isModified = false;
         }
         return isModified; 
         }
        set
        {
            isModified = value;
        } 
    }
    
    public bool ISFALLING
     { 
         get
         {
         if (IsFalling == Options.YES)
         {
           isFalling = true;

         }
         else if (IsFalling == Options.NO)
         {
           isFalling = false;          
         }
         else if (IsFalling == Options.OR)
         {
                isFalling = FindObjectOfType<InputController>().isFalling;
         }
            else
            {
                isFalling = false;
            }
         return isFalling; 
         }
        set
        {
            isFalling = value;
        } 
    }
    
    public bool ISJUMPPRESSED
     { 
         get
         {
         if (IsJumpPressed == Options.YES)
         {
           isJumpPressed = true;

         }
         else if (IsJumpPressed == Options.NO)
         {
           isJumpPressed = false;          
         }
         else
         {
             isJumpPressed = false;
         }
         return isJumpPressed; 
         }
        set
        {
            isJumpPressed = value;
        } 
    }

    public bool ISCROUCHED 
    {
        get
        {
            if (IsCrouched == Options.YES)
            {
                isCrouch = true;

            }
            else if (IsCrouched == Options.NO)
            {
                isCrouch = false;
            }
            else
            {
                isCrouch = false;
            }
            return isCrouch;
        }
        set
        {
            isCrouch = value;
        }
    }

    public bool ISACTION
    {
        get
        {
            if (IsAction == Options.YES)
            {
                isAction = true;

            }
            else if (IsAction == Options.NO)
            {
                isAction = false;
            }
            else if (IsAction == Options.OR)
            {
                isAction = FindObjectOfType<InputController>().isAction;
            }
            else
            {
                isAction = false;
            }
            return isAction;
        }
        set
        {
            isAction = value;
        }
    }

    public bool ISCLIMBING
    {
        get
        {
            if (IsClimbing == Options.YES)
            {
                isClimbing = true;

            }
            else if (IsClimbing == Options.NO)
            {
                isClimbing = false;
            }
            else if (IsClimbing == Options.OR)
            {
                isClimbing = FindObjectOfType<HumanoidColliderManger>().isClimbing;
            }
            else
            {
                isClimbing = false;
            }
            return isClimbing;
        }
        set
        {
            isClimbing = value;
        }
    }

    public string DIRECTIONS
    {
        get
        {
            if (InputKey == Directions.W)
            {
                directions = "W";
            }
            else if (InputKey == Directions.A)
            {
                directions = "A";
            }
            else if (InputKey == Directions.S)
            {
                directions = "S";
            }
            else if (InputKey == Directions.D)
            {
                directions = "D";
            }
            else if (InputKey == Directions.None)
            {
                directions = FindObjectOfType<InputController>().directions;
            }
            return directions;
        }
        set
        {
            directions = value;
        }
    }
    public bool ISHOVERMODE
    {
        get
        {
            if (IsHoverMode == Options.YES)
            {
                isHovermode = true;

            }
            else if (IsHoverMode == Options.NO)
            {
                isHovermode = false;
            }
            else
            {
                isHovermode = false;
            }
            return isHovermode;
        }
        set
        {
            isHovermode = value;
        }
    }


    public bool OBSTACLEDETECT
     { 
         get
         {
         if (ObstacleDetect == Options.YES)
         {
           obstacleDetect = true;

         }
         else if (ObstacleDetect == Options.NO)
         {
           obstacleDetect = false;          
         }
            else if (ObstacleDetect == Options.OR)
            {
                obstacleDetect = FindObjectOfType<ThirdPersonController>().isobstacle;
            }
         else 
         {
            obstacleDetect = false; 
         }
          return  obstacleDetect ;
         }
        set
        {
            obstacleDetect = value;
        } 
    }

    public bool ISFLYING 
    {
        get
        {
            if (IsFlying == Options.YES)
            {
                isFlying = true;

            }
            else if (IsFlying == Options.NO)
            {
                isFlying = false;
            }
            return isFlying;
        }
        set
        {
            isFlying = value;
        }
    }

    ///Combat///

    public bool ISPRIMARYATTACK
    {
        get
        {
            if (IsPrimaryAttack == Options.YES)
            {
                isPrimaryAttack = true;

            }
            else if (IsPrimaryAttack == Options.NO)
            {
                isPrimaryAttack = false;
            }
            else if(IsPrimaryAttack == Options.OR)
            {
                isPrimaryAttack = FindObjectOfType<InputController>().isPrimaryAttack;
            }
            else
            {
                isPrimaryAttack = false;
            }
            return isPrimaryAttack;
        }
        set
        {
            isPrimaryAttack = value;
        }
    }
    public bool ISCOMBATMODE
    {
        get
        {
            if (IsCombatMode == Options.YES)
            {
                isCombatMode = true;

            }
            else if (IsCombatMode == Options.NO)
            {
                isCombatMode = false;
            }
            else if (IsCombatMode == Options.OR)
            {
                isCombatMode = FindObjectOfType<InputController>().isCombatMode;
            }
            else
            {
                isCombatMode = false;
            }
            return isCombatMode;
        }
        set
        {
            isPrimaryAttack = value;
        }
    }

    public Options IsMoving;
    public Options IsGrounded;
    public Options IsModified;
    public Options IsFalling;
    public Options IsJumpPressed;
    public Options IsCrouched;
    public Options ObstacleDetect;
    public Options IsClimbing;
    public Options IsHoverMode;
    public Options IsFlying;
    public Options IsAction;
    public Directions InputKey;

    //Combat
    public Options IsPrimaryAttack;
    public Options IsCombatMode;

    public enum Priority
    {
        Low,// Could specify "Low = 0," if we want to be explicit.
        Medium,// Medium = 1,
        High,// High = 2,
    }
    public enum Directions
    {
        None, W, S, A, D
    }
    public Priority _Priority;

    public AudioClip[] Sounds;

}

