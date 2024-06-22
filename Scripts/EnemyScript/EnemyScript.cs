using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int playerDamage;
    bool isHit;
    void Start()
    {
        
    }

    void Update()
    {
        TakeHit(playerDamage);

    }

    void TakeHit( int playerDamage)
    {
        health = health - playerDamage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Melee")
        {
            if (GlobalCombatVariables.primaryAttack)
            {
                playerDamage = GlobalCombatVariables.meleeDamage;
            }            
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Melee")
        {
            playerDamage = 0;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Melee")
        {
            playerDamage = 0;
        }
    }
}
