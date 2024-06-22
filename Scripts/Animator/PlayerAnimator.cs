using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] internal PlayerScript PlayerScript;
    [SerializeField] internal CombatAnimator combatAnimator;

    public List<AnimationObject> MovementAnim = new List<AnimationObject>();
    public List<CombatAnimationObject> CombatAnim = new List<CombatAnimationObject>();
    public List<AnimationObject> InvoluntaryAnim = new List<AnimationObject>();
}
