using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonIK : InverseKinematics
{
    [SerializeField] GameObject head;

    void Update()
    {

        if (input.isSecondaryAttack)
        {
            rig.enabled = true;

        }
        else
        {
            rig.enabled = false;
        }
    }
}
