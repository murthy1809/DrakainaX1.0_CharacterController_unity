using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SoundCard", menuName = "SoundCards/SoundCard")]
public class SoundObjects : ScriptableObject
{
    public string AudioGroup;
    public AudioClip[] Clips;
    public int TerrainIndex;
    [Range(0, 1)]
    public float Volume;
    [Range(-3 , 3)]
    public float Pitch;
    [Range(-1, 1)]
    public float SteroPan;
    [Range(0, 1)]
    public float SpatialBlend;

    /* 
     * 
     * public audio duration
     * puublic bool startaudio when to
     * public bool stop audio when to
     */

}
