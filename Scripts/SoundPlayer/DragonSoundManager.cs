using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSoundManager : SoundManager
{
    public AudioSource footSteps, dragonSounds, meleeSounds,fireSounds;

    public void FootStepSound(AudioClip clip)
    {
        footSteps.PlayOneShot(clip);
    }

    public void DragonSounds(AudioClip clip)
    {
        //dragonSounds.PlayOneShot(clip); ;
        dragonSounds.clip = clip;
        dragonSounds.Play() ;
        // 
    }

    public void MeleeSounds(AudioClip clip)
    {
        meleeSounds.PlayOneShot(clip);
        //meleeSounds.clip = clip;
        //if (!meleeSounds.isPlaying)
        //{
        //    meleeSounds.Play();
           
        //}
        //else
        //{
        //    meleeSounds.Stop();
        //}
    }
    public void FireSounds(AudioClip clip)
    {
        fireSounds.PlayOneShot(clip);
    }
}
