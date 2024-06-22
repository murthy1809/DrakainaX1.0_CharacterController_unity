using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidCombatAnimator : CombatAnimator
{
    // [SerializeField] internal HumanoidColliderManger humanoidColliderManger;
    // [SerializeField] internal PlayerAnimator playerAnimator;
    private bool isClimbing, isAction;
    private string primaryTaps;
    internal string weaponType;
    public string eventFunctionName;
    public bool eventFired;
    public float triggertolerance;
    float triggerTime;
    float animLength;
    float animTime;
    float normTriggerTime;

    protected override void Update()
    {
        base.Update();
        //GetEventName();
        isClimbing = GetComponent<HumanoidColliderManger>().isClimbing;
        isDirection = PAnimator.PlayerScript.playerContoller.inputController.directions;
        isObstacle = PAnimator.PlayerScript.colliderManager.rayCasts.isObstacle;
        primaryTaps = GetComponent<HumanoidCombatController>().clicks;
        weaponType = GetComponent<HumanoidCombatController>().weaponType;
        isAction = PAnimator.PlayerScript.playerContoller.inputController.isAction;

    }

    protected override void PlayAnim()
    {
        for (int i = 0; i < PAnimator.CombatAnim.Count; i++)
        {

            if ((isMoving == PAnimator.CombatAnim[i].ISMOVING) &&
                isModified == PAnimator.CombatAnim[i].ISMODFIED &&
                isJumpPressed == PAnimator.CombatAnim[i].ISJUMPPRESSED &&
                isFalling == PAnimator.CombatAnim[i].ISFALLING &&
                onGround == PAnimator.CombatAnim[i].ISGROUNDED &&
                isObstacle == PAnimator.CombatAnim[i].OBSTACLEDETECT &&
                isHovermode == PAnimator.CombatAnim[i].ISHOVERMODE &&
                isFlying == PAnimator.CombatAnim[i].ISFLYING &&
                isCrouch == PAnimator.CombatAnim[i].ISCROUCHED &&
                isPrimaryAttack == PAnimator.CombatAnim[i].ISPRIMARYATTACK &&
                isCombatMode == PAnimator.CombatAnim[i].ISCOMBATMODE &&
                weaponType == PAnimator.CombatAnim[i].WEAPONTYPE &&
                isSheating == PAnimator.CombatAnim[i].ISSHEATING &&
                isDirection == PAnimator.CombatAnim[i].DIRECTIONS &&
                weaponType == PAnimator.CombatAnim[i].WEAPONTYPE &&
                isSecondaryAttack == PAnimator.CombatAnim[i].ISSECONDARYATTACK &&
                primaryTaps == PAnimator.CombatAnim[i].PRIMARYTAPS && 
                isAction == PAnimator.CombatAnim[i].ISACTION &&
                isClimbing == PAnimator.CombatAnim[i].ISCLIMBING
                )
            {
                k = i;
                _Animancer.Animator.applyRootMotion = PAnimator.CombatAnim[i].applyRootMotion;
                if (PAnimator.CombatAnim[i].AnimClips.Count == 1)
                {
                    j = 0;

                    if (_Animancer.Play(PAnimator.CombatAnim[i].AnimClips[j]).NormalizedTime >= 
                        _Animancer.Play(PAnimator.CombatAnim[i].AnimClips[j]).NormalizedEndTime)
                    {
                        GetComponent<InputController>().isPrimaryAttack = false;
                        GetComponent<InputController>().isSheating = false;
                    }
                    else
                    {
                        PAnimator.CombatAnim[i].AnimClips[j].Events.OnEnd = Idle;
                    }
                }
                else if (PAnimator.CombatAnim[i].AnimClips.Count > 1)
                {

                    if (_Animancer.Play(PAnimator.CombatAnim[i].AnimClips[j]).NormalizedTime >=
                        _Animancer.Play(PAnimator.CombatAnim[i].AnimClips[j]).NormalizedEndTime)
                    {
                       // _Animancer.Play(PAnimator.CombatAnim[i].AnimClips[j]);
                       
                        if (j == PAnimator.CombatAnim[i].AnimClips.Count - 1)
                        {
                            GetComponent<InputController>().isPrimaryAttack = false;
                            GetComponent<InputController>().isSheating = false;
                            j = 0;
                        }
                        else
                        {
                            j = j + 1; ;
                        }
                        // IF J > COUNT-1 THEN J = 0 OR J = COUNT OR CANCEL ANIM
                    }
                }
                else
                {
                    PAnimator.CombatAnim[i].AnimClips[j].Events.OnEnd = Idle;
                }
                if (PAnimator.CombatAnim[i].AnimClips.Count == 0)
                {
                    return;
                }

            }
        }
    }



    protected override void Idle()
    {
        _Animancer.Play(_Idle);
        _Idle.FadeDuration = 0.25f;
    }

   

}
