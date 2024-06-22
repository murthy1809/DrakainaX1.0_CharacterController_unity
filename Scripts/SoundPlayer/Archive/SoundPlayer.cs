using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public List<SoundObjects> SoundCards = new List<SoundObjects>();
    [SerializeField] internal AudioSource audioSource1, audioSource2;
    internal AudioClip clip;
    internal TerrainDetector terrainDetector;
    protected virtual void Awake()
    {
        terrainDetector = new TerrainDetector();
    }
    protected virtual void Start()
    {
        
    }


}
