using Animancer;
using UnityEngine;
using System.Collections.Generic;


public class CombatAnimator : MonoBehaviour
{
    // public AnimationObject Anim;


    [SerializeField] internal PlayerAnimator PAnimator;
    [SerializeField] public AnimancerComponent _Animancer;
    [SerializeField] public ClipTransition _Idle;

    internal bool isMoving;
    internal string isDirection;
    internal bool isModified;
    internal bool isJumpPressed;
    internal bool onGround;
    internal bool isObstacle;
    internal bool isCondition;
    internal bool isHovermode;
    internal bool isFalling;
    internal bool isFlying;
    internal bool isCrouch;
    // Combat
    internal bool isPrimaryAttack;
    internal bool isSecondaryAttack;
    internal bool isCombatMode;
    internal bool isSheating;
    public AnimationObject.Priority lastPriority;
    public AnimationObject.Priority currentPriority;

    public int k; //animation index number 
    [SerializeField] internal int j;
    private int Sequencer= 0;

    protected virtual void start()
    {  
    }

    protected virtual void Update()
    {
        onGround = PAnimator.PlayerScript.playerContoller.TPS.isgrounded;
        isMoving = PAnimator.PlayerScript.playerContoller.inputController.isMoving;
        isModified = PAnimator.PlayerScript.playerContoller.inputController.isModified;
        isJumpPressed = PAnimator.PlayerScript.playerContoller.inputController.isJumpPressed;
        isObstacle = PAnimator.PlayerScript.colliderManager.rayCasts.isObstacle;
        isHovermode = PAnimator.PlayerScript.playerContoller.inputController.isHoverMode;
        isFalling = PAnimator.PlayerScript.playerContoller.TPS.isfreeFall;
        isFlying = PAnimator.PlayerScript.playerContoller.inputController.isFlying;
        isCrouch = PAnimator.PlayerScript.playerContoller.inputController.isCrouch;
        // Combat
        isPrimaryAttack = PAnimator.PlayerScript.playerContoller.inputController.isPrimaryAttack;
        isSecondaryAttack = PAnimator.PlayerScript.playerContoller.inputController.isSecondaryAttack;
        isCombatMode = PAnimator.PlayerScript.playerContoller.inputController.isCombatMode;

        isSheating = PAnimator.PlayerScript.playerContoller.inputController.isSheating;
        isDirection = PAnimator.PlayerScript.playerContoller.inputController.directions;
        PlayAnim();
      //  Animancer.AnimancerLayer.SetMaxStateDepth(100);
    }

    protected virtual void PlayAnim()
    {
        
    }

    //protected virtual void SequenceAnims()
    //{

    //    for (int i = 0; i < PAnimator.CombatAnim.Count; i++)
    //    {

    //        if ((isMoving == PAnimator.CombatAnim[i].ISMOVING) &&
    //            isModified == PAnimator.CombatAnim[i].ISMODFIED &&
    //            isJumpPressed == PAnimator.CombatAnim[i].ISJUMPPRESSED &&
    //            isFalling == PAnimator.CombatAnim[i].ISFALLING &&
    //            onGround == PAnimator.CombatAnim[i].ISGROUNDED &&
    //            isObstacle == PAnimator.CombatAnim[i].OBSTACLEDETECT &&
    //            isHovermode == PAnimator.CombatAnim[i].ISHOVERMODE &&
    //            isFlying == PAnimator.CombatAnim[i].ISFLYING &&
    //            isCrouch == PAnimator.CombatAnim[i].ISCROUCHED &&
    //            isPrimaryAttack == PAnimator.CombatAnim[i].ISPRIMARYATTACK &&
    //            isCombatMode == PAnimator.CombatAnim[i].ISCOMBATMODE 
    //             )
    //        {
    //            k = i;
    //            _Animancer.Animator.applyRootMotion = PAnimator.CombatAnim[i].applyRootMotion;
    //            if (PAnimator.CombatAnim[i].AnimClips.Count == 1)
    //            {
    //                _Animancer.Play(PAnimator.CombatAnim[i].AnimClips[0]);
    //                if (_Animancer.Play(PAnimator.CombatAnim[i].AnimClips[0]).NormalizedTime >= 0.8f)
    //                {
    //                    GetComponent<InputController>().isPrimaryAttack = false;
    //                }
    //            }
    //            else if (PAnimator.CombatAnim[i].AnimClips.Count > 1)
    //            {
    //                _Animancer.Play(PAnimator.CombatAnim[i].AnimClips[j]);
    //                if (_Animancer.Play(PAnimator.CombatAnim[i].AnimClips[j]).NormalizedTime >= 0.8f)
    //                {
    //                    GetComponent<InputController>().isPrimaryAttack = false;
    //                    j = j + 1;
    //                    if (PAnimator.CombatAnim[i].AnimClips.Count > j)
    //                    {
    //                        _Animancer.Play(PAnimator.CombatAnim[i].AnimClips[j]);
    //                    }
    //                    else
    //                    {
    //                        _Animancer.Play(PAnimator.CombatAnim[i].AnimClips[PAnimator.CombatAnim[i].AnimClips.Count]);
    //                    }

    //                }
    //                else
    //                {
    //                    PAnimator.CombatAnim[i].AnimClips[j].Events.OnEnd = Idle;
    //                }
    //            }
    //        }
    //    }
    //}
    //protected virtual void RandomAnims(int i)
    //{
    //    _Animancer.Play(PAnimator.CombatAnim[i].AnimClips[j]);
    //    if (_Animancer.Play(PAnimator.CombatAnim[i].AnimClips[j]).NormalizedTime >= 0.8f)
    //    {
    //        GetComponent<InputController>().isPrimaryAttack = false;
    //        j = Random.Range(0, PAnimator.CombatAnim[i].AnimClips.Count);
    //    }
    //    else
    //    {
    //        PAnimator.CombatAnim[i].AnimClips[j].Events.OnEnd = Idle;
    //    }
    //}


    protected virtual void Idle()
    {
        _Animancer.Play(_Idle);
        _Idle.FadeDuration = 0.25f;
    }

    
}
