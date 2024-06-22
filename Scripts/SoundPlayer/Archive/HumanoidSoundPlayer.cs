using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidSoundPlayer :SoundPlayer
{
    [SerializeField] int terrainTextureIndex;
    protected virtual void FootSteps()
    {
        AudioClip clip = GetFootStepClip();
        audioSource1.PlayOneShot(clip);

    }

    protected virtual AudioClip GetFootStepClip()
    {
        terrainTextureIndex = terrainDetector.GetActiveTerrainTextureIdx(transform.position);

        switch (terrainTextureIndex)
        {
            case 0:
                return SoundCards[0].Clips[UnityEngine.Random.Range(0, SoundCards[0].Clips.Length)];
            case 1:
                return SoundCards[1].Clips[UnityEngine.Random.Range(0, SoundCards[1].Clips.Length)];
            case 2:
                return SoundCards[2].Clips[UnityEngine.Random.Range(0, SoundCards[2].Clips.Length)];
            default:
                return SoundCards[0].Clips[UnityEngine.Random.Range(0, SoundCards[0].Clips.Length)];
        }
    }




}

