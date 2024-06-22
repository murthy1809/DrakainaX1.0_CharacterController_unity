using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSoundClips : MonoBehaviour
{
    public List<SoundObjects> FootstepSounds = new List<SoundObjects>();
    public List<SoundObjects> DragonSounds = new List<SoundObjects>();
    public List<SoundObjects> MeleeSounds = new List<SoundObjects>();
    public List<SoundObjects> FireSounds = new List<SoundObjects>();
    DragonSoundManager manager;
    InputController input;
    public string audioGroup;
    private AudioClip audioClip, clip;
    DragonCombatAnimator animator;
    bool hasSoundPlayed = false;
   // public bool isMetalHit;
    private void Awake()
    {    
        manager = GetComponent<DragonSoundManager>();
        input = GetComponent<InputController>();
        animator = GetComponent<DragonCombatAnimator>();
    }


    private void Update()
    {
        PlayDragonSounds();
        PlayMeleeSounds();
        PlayFireSounds();
        
    }

    void Landing()
    {
        for (int i = 0; i < DragonSounds.Count; i++)
        {
            if (animator.eventFunctionName == DragonSounds[i].AudioGroup)
            {
                audioGroup = DragonSounds[i].AudioGroup;
                audioClip = DragonSounds[i].Clips[Random.Range(0, DragonSounds[i].Clips.Length)];
                manager.DragonSounds(audioClip);
            }
            else
            {
                audioClip = null;
            }
        }
    }

    public void PlaySound()
    {
        hasSoundPlayed = true;
    }

  
    protected virtual AudioClip PlayDragonSounds()
    {
       
        for (int i = 0; i < DragonSounds.Count; i++)
        {
           
            if (hasSoundPlayed)
            {
                audioGroup = DragonSounds[i].AudioGroup;
                audioClip = DragonSounds[i].Clips[Random.Range(0, DragonSounds[i].Clips.Length)];
                manager.DragonSounds(audioClip);
                hasSoundPlayed = false;
            }
            else
            {
                audioClip = null;
                hasSoundPlayed = false;
            }
        }
        
        return audioClip;
    }

   // public bool HasSoundPlayed()
   // {
   //     return hasSoundPlayed;
   // }

   //public void ResetSoundPlayed()
   // {
   //     hasSoundPlayed = false;
   // }


    protected virtual AudioClip PlayMeleeSounds()
    {
        for (int i = 0; i < MeleeSounds.Count; i++)
        {
            if (hasSoundPlayed)
            {
                audioGroup = MeleeSounds[i].AudioGroup;
                audioClip = MeleeSounds[i].Clips[Random.Range(0, MeleeSounds[i].Clips.Length)];
                manager.MeleeSounds(audioClip);
                hasSoundPlayed = false;
            }
            else
            {
                audioClip = null;
                hasSoundPlayed = false;
            }

        }
        return audioClip;
    }
    protected virtual AudioClip PlayFireSounds()
    {
        if (input.isCombatMode)
        {
            for (int i = 0; i < FireSounds.Count; i++)
            {
                if (FireSounds[i].AudioGroup == animator.eventFunctionName &&
                    animator.eventFired)
                {
                    audioGroup = FireSounds[i].AudioGroup;
                    audioClip = FireSounds[i].Clips[Random.Range(0, FireSounds[i].Clips.Length)];
                    manager.MeleeSounds(audioClip);
                    Debug.Log(audioClip);
                }
                else
                {
                    audioClip = null;
                }
            }
        }
        else
        {
            audioClip = FireSounds[0].Clips[Random.Range(0, FireSounds[0].Clips.Length)];
        }

        return audioClip;
    }
    protected virtual void FootSteps()
    {
        audioClip = GetFootStepClip();
        manager.FootStepSound(audioClip);

    }
    protected virtual AudioClip GetFootStepClip()
    {
        return FootstepSounds[0].Clips[Random.Range(0, FootstepSounds[0].Clips.Length)];
    }

   

}
