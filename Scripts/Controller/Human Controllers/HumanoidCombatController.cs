using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidCombatController : CombatController
{
    [SerializeField] internal PlayerController controller;
    float ClickDelay = 0.2f;
    int _currClicks;
    float _clickTime;
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
        CheckForClicks("PrimaryAttack", 0.5f);
        attackLevel = controller.playerAnimator.combatAnimator.j;

    }

    private void SelectWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponType = "Fists";
            singleHandSword.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponType = "SingleHandedSword";
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

    private void CheckForClicks(string buttonName, float clickDelay)
    {

        if (Input.GetButtonDown(buttonName))
        {
            _currClicks++;


            _clickTime = 0;
        }

        if (_currClicks == 0) return;


        if (_clickTime < ClickDelay)
        {
            _clickTime += Time.deltaTime;
            return;
        }


        HandleClicks(_currClicks);
        _currClicks = 0;
        _clickTime = 0;

    }

    private void HandleClicks(int amountOfClicks)
    {
        if(weaponType == "SingleHandedSword")
        {
            if (amountOfClicks == 1)
            {
                clicks = "Single";
            }
            else if (amountOfClicks == 2)
            {
                clicks = "Double";
            }
            else if (amountOfClicks == 3)
            {
                clicks = "Triple";
            }
            else if (amountOfClicks > 3)
            {
                clicks = "Single";
            }
            else
            {
                clicks = "None";
            }
        }

        if(weaponType == "Fists")
        {
            if (amountOfClicks == 1)
            {
                clicks = "Single";
            }
            else if (amountOfClicks > 1)
            {
                clicks = "Single";
            }
            else
            {
                clicks = "None";
            }
        }
     
    }
}
