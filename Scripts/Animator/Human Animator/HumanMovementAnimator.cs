using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMovementAnimator : MovementAnimator
{
    public bool vaultOverride;
    bool isClimbing;
    internal string isDirection;

    protected override void Update()
    {
        GetPriority();
        base.Update();
        isObstacle = PAnimator.PlayerScript.colliderManager.rayCasts.isObstacle;
        isClimbing = GetComponent<HumanoidColliderManger>().isClimbing;
        isDirection = PAnimator.PlayerScript.playerContoller.inputController.directions;

    }
    protected override void GetPriority()
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
                isCombatMode == PAnimator.MovementAnim[i].ISCOMBATMODE &&
                isAction == PAnimator.MovementAnim[i].ISACTION &&
                isClimbing == PAnimator.MovementAnim[i].ISCLIMBING &&
                isDirection == PAnimator.MovementAnim[i].DIRECTIONS )
            {
                k = i;
                _Animancer.Animator.applyRootMotion = PAnimator.MovementAnim[i].applyRootMotion;
                PAnimator.MovementAnim[i].AnimClip.Events.OnEnd = Idle;

                if (_Animancer.IsPlaying(PAnimator.MovementAnim[7].AnimClip))
                {

                    if (_Animancer.Play(PAnimator.MovementAnim[i].AnimClip).NormalizedTime >= 0.8f)
                    {
                       
                        GetComponent<InputController>().isAction = false;

                    }
                }
                else
                {
                    _Animancer.Play(PAnimator.MovementAnim[i].AnimClip);
                }
            }
            else
            {
                PAnimator.MovementAnim[i].AnimClip.Events.OnEnd = Idle;
            }
        }
    }

    //if (_Animancer.Play(PAnimator.MovementAnim[i].AnimClip).NormalizedTime >= 0.8f)
    //{

    //}
}



