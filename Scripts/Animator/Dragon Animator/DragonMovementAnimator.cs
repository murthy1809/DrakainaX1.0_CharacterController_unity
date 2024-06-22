using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class DragonMovementAnimator : MovementAnimator
{
    protected override void Update()
    {
        base.Update();
        GetPriority();
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
                isCombatMode == PAnimator.MovementAnim[i].ISCOMBATMODE)
            {
                k = i;
                _Animancer.Animator.applyRootMotion = PAnimator.MovementAnim[i].applyRootMotion;
                //_Animancer.Play(PAnimator.CombatAnim[i].AnimClips[0]);
                if (_Animancer.Play(PAnimator.MovementAnim[i].AnimClip).NormalizedTime >= 0.8f)
                {

                }
                else
                {
                    PAnimator.MovementAnim[i].AnimClip.Events.OnEnd = Idle;
                }

            }
        }
    }
  
    protected override void AnimationOverride()
    {
        if (lastPriority == AnimationObject.Priority.High)
        {
            lastPriority = AnimationObject.Priority.Low;
            // Debug.Log("freefall");

        }
    }
}
