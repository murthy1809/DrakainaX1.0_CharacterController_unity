using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidCombatController : CombatController
{
    [SerializeField] internal PlayerController controller;
    [SerializeField] internal InputController inputController;
    private float clickTime = 0.0f;
    [SerializeField] float clickDelay = 0.2f;
    private int clickCount = 0;   
    public string clicks;
    public GameObject singleHandSword;
    [SerializeField] int bowCount = 10;

    void Start()
    {
        weaponType = "Fists";
        singleHandSword.SetActive(false);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        SelectWeapon();
        WeaponDraw();
        WeaponDamage();
        MouseClickCounter();
        BowCount();
        attackLevel = controller.playerAnimator.combatAnimator.j;

    }


    private void SelectWeapon()
    {
        if (inputController.fistEquip)
        {
            weaponType = "Fists";
            singleHandSword.SetActive(false);
        }
        else if (inputController.swordEquip)
        {
            weaponType = "SingleHandedSword";
        }
        else if (inputController.bowEquip)
        {
            weaponType = "Bow";
        }
    }
    private void BowCount()
    {
        if (weaponType == "Bow")
        {
            if (clickCount ==1 && Input.GetMouseButton(1))
            {
                
                if (bowCount>0)
                {
                    bowCount = bowCount - 1;
                }
                
            }
        }
    }

    private void WeaponDraw()
    {
        if(weaponType == "SingleHandedSword")
        {
            if (playerController.inputController.isCombatMode)
            {
                singleHandSword.SetActive(true);
            }
            else if (!playerController.inputController.isCombatMode && !playerController.inputController.isSheating)
            {
                singleHandSword.SetActive(false);
            }
        }
    }

    private int WeaponDamage()
    {
        if(attackLevel > 1)
        {
            return attackPoints = 2;
        }
        else
        {
            return attackPoints = 1;
        }
    }

    private void MouseClickCounter()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickCount++;
            clickTime = Time.time;
        }

        // Detect mouse button held down
        if (Input.GetMouseButton(0))
        {          
            clicks = "PressDown";
        }

        // Detect mouse button released
        if (Input.GetMouseButtonUp(0))
        {
            clicks = "PressUp";
            if (Time.time - clickTime < clickDelay)
            {
                Invoke("CheckClicks", clickDelay);               
            }
        }
    }

    void CheckClicks()
    {
        if (clickCount == 1)
        {
            clicks = "Single";
            Debug.Log("single");
        }
        else if (clickCount == 2)
        {
            clicks = "Double";
            Debug.Log("double");
        }
        else if (clickCount > 2)
        {
            return;
        }
        clickCount = 0;
    }
}
