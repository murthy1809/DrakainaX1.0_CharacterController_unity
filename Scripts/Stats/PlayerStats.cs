using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] internal InputController inputController;
    public float stamina, maxStamina, minStamina, staminaDrainRate, staminaFillRate, hunger, thirst,
               health, energy;
    public float speed, walkSpeed, combatSpeed, climbSpeed,crouchSpeed, speedModifier, maxSpeedModifier, minSpeedModifer;
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        Stamina();
    }

    protected virtual void Stamina()
    {
        
    }
}
