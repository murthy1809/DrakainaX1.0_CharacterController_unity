using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvoluntaryAnimator : MovementAnimator
{
    void Start()
    {
        
    }
    protected override void Update()
    {
       PlayAnim();
    }


    public override void PlayAnim()
    {
        for (int i = 0; i < PAnimator.InvoluntaryAnim.Count; i++)
        {
            if (PAnimator.PlayerScript.playerContoller.TPS.isfreeFall == PAnimator.InvoluntaryAnim[0].ISFALLING)
            {
                _Animancer.Play(PAnimator.InvoluntaryAnim[i].AnimClip);
     
            }
            else
            {
                PAnimator.InvoluntaryAnim[i].AnimClip.Events.OnEnd = Idle;
            }
        }
    }
}

