using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidSoundManager : SoundManager
{
    public AudioSource  footSteps, humanSound, swordSound;
 
    public void FootStepSound(AudioClip clip)
    {
        footSteps.PlayOneShot(clip);
    }

    public void HumanSound(AudioClip clip)
    {
        humanSound.PlayOneShot(clip);
        //humanSound.clip = clip;
        //if (!humanSound.isPlaying)
        //{
        //    humanSound.Play();
        //}
        //else
        //{
        //    humanSound.Stop();
        //}
    }
    public void SwordSound(AudioClip clip)
    {
        swordSound.PlayOneShot(clip);
    }

}
