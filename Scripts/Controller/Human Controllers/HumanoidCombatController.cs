using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidCombatController : CombatController
{
    [SerializeField] internal PlayerController controller;
    [SerializeField] internal InputController inputController;
    //[SerializeField] float ClickDelay = 0.2f;
    private float clickTime = 0.0f;
    [SerializeField] float clickDelay = 0.2f;
    private int clickCount = 0;
    string button;
   
    public string clicks;
    public GameObject singleHandSword;

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
            Debug.Log("Mouse button pressed down");
        }

        // Detect mouse button held down
        if (Input.GetMouseButton(0))
        {          
            clicks = "PressDown";
            Debug.Log(clicks);
        }

        // Detect mouse button released
        if (Input.GetMouseButtonUp(0))
        {
            clicks = "PressUp";
            Debug.Log(clicks);
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
        else if (clickCount >= 2)
        {
            clicks = "Double";
            Debug.Log("double");
        }
        //else if (clickCount == 3)
        //{
        //    clicks = "Triple";
        //    Debug.Log("triple");
        //}##
        clickCount = 0;
    }
}
