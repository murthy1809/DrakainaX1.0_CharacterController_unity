using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanStats : PlayerStats
{
    public UIHumanStats humanStats;
    void Start()
    {
        
    }

    protected override void Update()
    {
        base.Update();
        humanStats.CurrentStamina(stamina);
        humanStats.SetMaxStamina(maxStamina);
        humanStats.SetMinStamina(minStamina);

    }

    protected override void Stamina()
    {
        if (inputController.isCombatMode)
        {
            if (inputController.bowEquip)
            {
                if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
                {
                    stamina = stamina - staminaDrainRate;
                }
                else
                {
                    stamina = stamina + staminaFillRate;
                }

                if (stamina >= maxStamina)
                {
                    stamina = maxStamina;
                }
                else if (stamina <= minStamina)
                {
                    stamina = minStamina;
                }
            }
        }
        else
        {
            if (inputController.isMoving && inputController.isModified)
            {
                stamina = stamina - staminaDrainRate;
            }
            else
            {
                stamina = stamina + staminaFillRate;
            }

            if (stamina >= maxStamina)
            {
                stamina = maxStamina;
            }
            else if (stamina <= minStamina)
            {
                stamina = minStamina;
            }
        }
    }
}
