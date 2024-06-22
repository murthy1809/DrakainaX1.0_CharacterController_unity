using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidSoundClips : MonoBehaviour
{
    public List<SoundObjects> FootstepSounds = new List<SoundObjects>();
    public List<SoundObjects> SwordSounds = new List<SoundObjects>();
    public List<SoundObjects> HumanSounds = new List<SoundObjects>();
    HumanoidSoundManager manager;
    private TerrainDetector terrainDetector;
    public string audioGroup;
    private AudioClip audioClip,clip;
    public bool isMetalHit;
    private void Awake()
    {
        terrainDetector = new TerrainDetector();
        manager = GetComponent<HumanoidSoundManager>();
    }

   
    private void Update()
    {
        PlaySwordSounds();
       // PlayHumanSounds();
    }

    protected virtual AudioClip PlaySwordSounds()
    {
        for (int i = 0; i < SwordSounds.Count; i++)
        {
            if( GetComponent<HumanoidCombatAnimator>().eventFunctionName == SwordSounds[i].AudioGroup &&
                GetComponent<HumanoidCombatAnimator>().eventFired)
            {
                audioGroup = SwordSounds[i].AudioGroup;
                audioClip = SwordSounds[i].Clips[Random.Range(0, SwordSounds[i].Clips.Length)];
                manager.SwordSound(audioClip);
                print("sound");

            }
            else
            {
                audioClip = null;
            }
            
        }
        return audioClip;
    }


    //protected virtual AudioClip PlayHumanSounds()
    //{
    //    for (int i = 0; i < HumanSounds.Count; i++)
    //    {
    //        if ( GetComponent<HumanoidCombatAnimator>().eventFunctionName == HumanSounds[i].AudioGroup  &&
    //            GetComponent<HumanoidCombatAnimator>().eventFired)
    //        {
    //            audioGroup = HumanSounds[i].AudioGroup;
    //            audioClip = HumanSounds[i].Clips[Random.Range(0, HumanSounds[i].Clips.Length)];
    //            manager.HumanSound(audioClip);
    //        }
    //        else
    //        {
    //            audioClip = null;
    //        }

    //    }
    //    return audioClip;
    //}


    protected void SendEvent()
    {
        for (int i = 0; i < HumanSounds.Count; i++)
        {
            audioClip = HumanSounds[i].Clips[Random.Range(0, HumanSounds[i].Clips.Length)];
            manager.HumanSound(audioClip);
        }
    }

    protected virtual void FootSteps()
    {
        AudioClip clip = GetFootStepClip();
        manager.FootStepSound(clip);
    }


    protected virtual AudioClip GetFootStepClip()
    {
        int terrainTextureIndex = terrainDetector.GetActiveTerrainTextureIdx(transform.position);
        switch (terrainTextureIndex)
        {
            case 0:
                return FootstepSounds[0].Clips[Random.Range(0, FootstepSounds[0].Clips.Length)];
            case 1:
                return FootstepSounds[1].Clips[Random.Range(0, FootstepSounds[1].Clips.Length)];
            case 2:
                return FootstepSounds[2].Clips[Random.Range(0, FootstepSounds[2].Clips.Length)];
            default:
                return FootstepSounds[0].Clips[Random.Range(0, FootstepSounds[0].Clips.Length)];
        }
    }



}
