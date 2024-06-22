using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSoundPlayer : SoundPlayer
{
    [SerializeField] internal InputController input;
    private AudioClip audioClip;
    public string audioGroup;

    private void Update()
    {
        GlideScreech();
    }
    protected virtual void Flying()
    {
        AudioClip clip = GetSoundClips();
        audioSource1.PlayOneShot(clip);
        audioGroup = "Flying";
        
    }

    protected virtual void Hovering()
    {
        AudioClip clip = GetSoundClips();
        audioSource1.PlayOneShot(clip);
        audioGroup = "Hovering";
    }

    protected virtual void GlideScreech()
    {
        if(input.isFlying && Input.GetKey(KeyCode.E))
        {
            Screech();
        }
    }

    void Screech()
    {
        AudioClip clip = GetSoundClips();
        if (!audioSource2.isPlaying)
        {
            audioSource2.PlayOneShot(clip);
            audioGroup = "Screech";
        }

    }
    //protected virtual void Hovermode()
    //{
    //    AudioClip clip = GetSoundClips();
    //    audioSource.PlayOneShot(clip);
    //    audioGroup = "Hovermode";
    //}
    //protected virtual void FootSteps()
    //{
    //    AudioClip clip = GetSoundClips();
    //    audioSource.PlayOneShot(clip);
    //    audioGroup = "FootSteps";
    //}

    //protected virtual void Landing()
    //{
    //    AudioClip clip = GetSoundClips();
    //    audioSource.PlayOneShot(clip);
    //    audioGroup = "Landing";
    //}


    private AudioClip GetSoundClips()
    {
        for (int i = 0; i < SoundCards.Count; i++)
        {
            if(audioGroup == SoundCards[i].AudioGroup)
            {
                audioClip = SoundCards[i].Clips[Random.Range(0, SoundCards[i].Clips.Length)];
            }
        }
        return audioClip;
    }


}
