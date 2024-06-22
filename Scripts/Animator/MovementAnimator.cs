using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class MovementAnimator : MonoBehaviour
{
   // public AnimationObject Anim;
   

    [SerializeField] internal PlayerAnimator PAnimator;
    [SerializeField] public AnimancerComponent _Animancer;
    [SerializeField] public ClipTransition _Idle;
    
    internal bool isMoving;
    internal bool isModified;
    internal bool isJumpPressed;
    internal bool onGround;
    internal bool isObstacle;
    internal bool isCondition;
    internal bool isHovermode;
    internal bool isFalling;
    internal bool isFlying;
    internal bool isCrouch;
    internal bool isAction;
    // Combat
    internal bool isPrimaryAttack;
    internal bool isCombatMode;
    public AnimationObject.Priority lastPriority;
    public  AnimationObject.Priority currentPriority;




    public int k; //animation index number

    protected virtual void start()
    {
        for (int i = 0; i < PAnimator.MovementAnim.Count; i++)
        {
            if (isMoving == PAnimator.MovementAnim[i].ISMOVING &&
                isModified == PAnimator.MovementAnim[i].ISMODFIED &&
                isJumpPressed == PAnimator.MovementAnim[i].ISJUMPPRESSED &&
                isFalling == PAnimator.MovementAnim[i].ISFALLING &&
                onGround == PAnimator.MovementAnim[i].ISGROUNDED &&
                isObstacle == PAnimator.MovementAnim[i].OBSTACLEDETECT &&
                isHovermode == PAnimator.MovementAnim[i].ISHOVERMODE &&
                isFlying == PAnimator.MovementAnim[i].ISFLYING&&
                isCrouch == PAnimator.MovementAnim[i].ISCROUCHED &&
                isPrimaryAttack == PAnimator.MovementAnim[i].ISPRIMARYATTACK &&
                isCombatMode == PAnimator.MovementAnim[i].ISCOMBATMODE)
            {
                lastPriority = PAnimator.MovementAnim[i]._Priority;
            }
        }
    }

    protected virtual void Update()
    {
      
        GetPriority();
        AnimationOverride();
        onGround = PAnimator.PlayerScript.playerContoller.TPS.isgrounded;
        isMoving = PAnimator.PlayerScript.playerContoller.inputController.isMoving;
        isModified = PAnimator.PlayerScript.playerContoller.inputController.isModified;
        isJumpPressed = PAnimator.PlayerScript.playerContoller.inputController.isJumpPressed;
        //isObstacle = PAnimator.PlayerScript.colliderManager.rayCasts.isObstacle;
        isHovermode = PAnimator.PlayerScript.playerContoller.inputController.isHoverMode;
        isFalling = PAnimator.PlayerScript.playerContoller.TPS.isfreeFall;
        isFlying = PAnimator.PlayerScript.playerContoller.inputController.isFlying;
        isCrouch = PAnimator.PlayerScript.playerContoller.inputController.isCrouch;
        isAction = PAnimator.PlayerScript.playerContoller.inputController.isAction;
        // Combat
        isPrimaryAttack = PAnimator.PlayerScript.playerContoller.inputController.isPrimaryAttack;
        isCombatMode = PAnimator.PlayerScript.playerContoller.inputController.isCombatMode;
       // Animancer.AnimancerLayer.SetMaxStateDepth(100);
    }
     
    public virtual void PlayAnim()
    {

        for (int i = 0; i < PAnimator.MovementAnim.Count; i++)
        {
          
            if (lastPriority <= currentPriority)
            {
                if ((isMoving == PAnimator.MovementAnim[i].ISMOVING) &&
                    isModified == PAnimator.MovementAnim[i].ISMODFIED &&
                    isJumpPressed == PAnimator.MovementAnim[i].ISJUMPPRESSED &&
                    isFalling == PAnimator.MovementAnim[i].ISFALLING &&
                    onGround == PAnimator.MovementAnim[i].ISGROUNDED &&
                    isObstacle == PAnimator.MovementAnim[i].OBSTACLEDETECT &&
                    isHovermode == PAnimator.MovementAnim[i].ISHOVERMODE&&
                    isFlying == PAnimator.MovementAnim[i].ISFLYING &&
                    isCrouch == PAnimator.MovementAnim[i].ISCROUCHED &&
                    isPrimaryAttack == PAnimator.MovementAnim[i].ISPRIMARYATTACK &&
                isCombatMode == PAnimator.MovementAnim[i].ISCOMBATMODE)
                    // currentPriority
                {
                    lastPriority = currentPriority;
                    _Animancer.Play(PAnimator.MovementAnim[i].AnimClip);
                    _Animancer.Animator.applyRootMotion = PAnimator.MovementAnim[i].applyRootMotion;
                    k = i;
                    //if (_Animancer.Play(PAnimator.MovementAnim[i].AnimClip).NormalizedTime >= 1)
                    //{
                    //    Debug.Log("animationended");
                    //    GetComponent<InputController>().isPrimaryAttack = false;
                    //    //somebool

                    //}
                }
                else
                {
                    PAnimator.MovementAnim[i].AnimClip.Events.OnEnd= Idle;
                }
            }          

        } 
    }  

    protected virtual void GetPriority()
    {
        for (int i = 0; i < PAnimator.MovementAnim.Count; i++)
        {
            if (isMoving == PAnimator.MovementAnim[i].ISMOVING &&
                isModified == PAnimator.MovementAnim[i].ISMODFIED &&
                isJumpPressed == PAnimator.MovementAnim[i].ISJUMPPRESSED &&
                isFalling == PAnimator.MovementAnim[i].ISFALLING &&
                onGround == PAnimator.MovementAnim[i].ISGROUNDED &&
                isObstacle == PAnimator.MovementAnim[i].OBSTACLEDETECT &&
                isHovermode == PAnimator.MovementAnim[i].ISHOVERMODE &&
                isFlying == PAnimator.MovementAnim[i].ISFLYING &&
                isCrouch == PAnimator.MovementAnim[i].ISCROUCHED &&
                isPrimaryAttack == PAnimator.MovementAnim[i].ISPRIMARYATTACK &&
                isCombatMode == PAnimator.MovementAnim[i].ISCOMBATMODE)
            {
                currentPriority = PAnimator.MovementAnim[i]._Priority;
                PlayAnim();            
            }
        }
    }

    public virtual void Idle()
    {
        _Animancer.Play(_Idle);
        _Idle.FadeDuration = 0.25f;
    }

    protected virtual void AnimationOverride()
    {

        if (lastPriority == AnimationObject.Priority.High)
        {
            lastPriority = AnimationObject.Priority.Low;
           // Debug.Log("freefall");

        }
    }


}
