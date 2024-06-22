using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DragonCombatAnimator : CombatAnimator
{
    internal string weaponType;
    public bool animEvent = true;//
    public string eventFunctionName;
    public bool eventFired, jumpaction;
    public float triggertolerance,lowertolerance,num;
    float triggerTime;
    float animLength;
    float animTime;
    float normTriggerTime;
    int count;
    List<float> nums = new List<float>();

    protected override void Update()
    {
        base.Update();
        // GetEventName();
        weaponType = GetComponent<DragonCombatController>().weaponType;
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
                isSecondaryAttack == PAnimator.CombatAnim[i].ISSECONDARYATTACK )
            {
                k = i;
                _Animancer.Animator.applyRootMotion = PAnimator.CombatAnim[i].applyRootMotion;
                if (PAnimator.CombatAnim[i].AnimClips.Count == 1)
                {
                    j = 0;
                    if (_Animancer.Play(PAnimator.CombatAnim[i].AnimClips[j]).NormalizedTime >=
                        _Animancer.Play(PAnimator.CombatAnim[i].AnimClips[j]).NormalizedEndTime)
                    {
                       // eventFunctionName = PAnimator.CombatAnim[i].AnimClips[j].Clip.events[j].functionName;
                        if (!isFlying)
                        {
                            GetComponent<InputController>().isPrimaryAttack = false;
                            GetComponent<InputController>().isSheating = false;
                            animEvent = true;
                            if (isJumpPressed)
                            {
                                isJumpPressed = false;
                            }
                        }

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
                       // eventFunctionName = PAnimator.CombatAnim[i].AnimClips[j].Clip.events[j].functionName;
                          if (j == PAnimator.CombatAnim[i].AnimClips.Count - 1)
                        {
                            GetComponent<InputController>().isPrimaryAttack = false;
                            GetComponent<InputController>().isSheating = false;
                            //GetComponent<InputController>().isJumpPressed = false;
                            j = 0;
                        }
                        else
                        {
                            j = j + 1; ;
                        }
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
}
