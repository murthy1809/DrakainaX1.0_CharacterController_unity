using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidCombatController : CombatController
{
    [SerializeField] internal PlayerController controller;
    [SerializeField] internal InputController inputController;
    [SerializeField] internal float downTimeRight;
    [SerializeField] internal float keydowntime;
    [SerializeField] float bowtimesetting;
    private bool butOnlyOnce = false;
    private float clickTime = 0.0f;
    [SerializeField] float clickDelay = 0.2f;
    private int clickCount = 0;   
    public string clicks;
    int _currClicks;
    float _clickTime;
    float ClickDelay = 0.2f;
    public GameObject singleHandSword;
    [SerializeField] int bowCount = 10;
    public bool arrowLoad= false;

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
        // MouseClickCounter();
        CheckForClicks("PrimaryAttack",0.2f);
        BowCount();
        KeyPresstime();
        attackLevel = controller.playerAnimator.combatAnimator.j;

        if (Input.GetMouseButton(0))
        {
            clicks = "PressDown";
        }
        if (Input.GetMouseButtonUp(0))
        {
            clicks = "PressUp";
        }

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

    private void KeyPresstime()
    {
        if (clicks == "PressDown")
        {
            downTimeRight += Time.deltaTime;
            butOnlyOnce = true;

        }
        if (clicks == "PressUp" && butOnlyOnce)
        {
            butOnlyOnce = false;
            keydowntime = downTimeRight; // okay, now set

            downTimeRight = 0; // and reset downTimeRight
        }
    }
    /*GetComponent<HumanoidCombatAnimator>().eventFunctionName == "BowFire"*/
    private void BowCount()
    {
        if (weaponType == "Bow")
        {
            if (bowCount > 0)
            {
                if(inputController.isSecondaryAttack)
                {
                    if (!arrowLoad && clicks == "Double")
                    {
                        arrowLoad = true;
                    }
                    if (arrowLoad && keydowntime >= bowtimesetting && clicks == "PressUp")
                    {
                        bowCount -= 1;
                        keydowntime = 0;
                        arrowLoad = false;
                    }
                }
                else
                {
                    arrowLoad = false;
                }
            }

            if (bowCount <=1 && inputController.isSecondaryAttack)
            {
                inputController.isPrimaryAttack = false;
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
    public void HandleClicks(int amountOfClicks)
    {

        if (amountOfClicks == 1)
        {
            clicks = "Single";
        }
        else if (amountOfClicks >= 2)
        {
            clicks = "Double";
        }
        //else if (amountOfClicks == 3)
        //{
        //    clicks = "Triple";
        //}        
    }
    //private void MouseClickCounter()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        clickCount++;
    //        clickTime = Time.time;
    //    }

    //    // Detect mouse button held down
    //    if (Input.GetMouseButton(0))
    //    {          
    //        clicks = "PressDown";
    //    }

    //    // Detect mouse button released
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        clicks = "PressUp";
    //        if (Time.time - clickTime < clickDelay)
    //        {
    //            Invoke("CheckClicks", clickDelay);               
    //        }
    //    }
    //}

    //void CheckClicks()
    //{
    //    if (clickCount == 1)
    //    {
    //        clicks = "Single";
    //        Debug.Log("single");
    //    }
    //    else if (clickCount == 2)
    //    {
    //        clicks = "Double";
    //        Debug.Log("double");
    //    }
    //    else if (clickCount > 2)
    //    {
    //        return;
    //    }
    //    clickCount = 0;
    //}
}


