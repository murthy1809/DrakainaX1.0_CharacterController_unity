using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Animancer;
using Animancer.FSM;

public class CharacterAnimatorNew : MonoBehaviour
{
    [SerializeField] private ThirdPersonController TPS;
    [SerializeField] private AnimancerComponent _Animancer;
    [SerializeField] private ClipTransition _Idle;
    [SerializeField] private float playerVelocity;
    [SerializeField] private bool  onGround;
    [SerializeField] private bool isMoving;
    [SerializeField] private bool isModified;
    [SerializeField] private bool isFalling;
    [SerializeField] private bool isJumpPressed;

    [Serializable]
    public class Movement
    {
        public string name;
        public ClipTransition clip;
        public bool isPlaying;
        public bool rootmotion;
        public string NAME 
        {
            get
            {
                return name;
            }
            set
            {
                 NAME = name;
            }
        }
    }

    [SerializeField] public List<Movement> Animations = new List<Movement>();

    public void Play(Movement motion)
    {

    }

    private void Awake()
    {
        Idle();     
    }

    private void Update()
    {
        Walk();
        Jump();
        Run();
        FreeFall();
       // AnimancerLayer.SetMaxStateDepth(1000);
        onGround = TPS.isgrounded;
        isFalling = TPS.isfreeFall;
        playerVelocity = TPS.playerVelocity.y;
        
        if (Input.GetButtonDown("Jump"))
        {
            isJumpPressed = true;
        }
        if (Input.GetButton("Shift"))
        {
            isModified = true;
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            isMoving = true;
        }
    }

    private void Idle()
    {
        _Animancer.Play(_Idle);
    }

    private void Walk()
    {
        for (int i = 0; i < Animations.Count; i++)
        {
            if (Animations[i].NAME == "Walk")
            {
                if (isMoving && onGround && !isModified)
                {
                    isMoving = false;
                    _Animancer.Play(Animations[i].clip);
                    Animations[i].isPlaying = true;
                }
                else
                {
                    Animations[i].clip.Events.OnEnd = Idle;
                    Animations[i].isPlaying = false;
                }
            }
        }
    }


    private void Jump()
    {
        for (int i = 0; i < Animations.Count; i++)
        {
            if (Animations[i].NAME == "Jump")
            {
                if (isJumpPressed && !onGround)
                {
                    isJumpPressed = false;
                    _Animancer.Play(Animations[i].clip);
                    Animations[i].isPlaying = true;
                }
            }
        }
    }


    private void Run()
    {
        for (int i = 0; i < Animations.Count; i++)
        {
            if (Animations[i].NAME == "Run")
            {
                if (isMoving && isModified && onGround)
                {
                    isMoving = false;
                    isModified = false;
                    _Animancer.Play(Animations[i].clip);
                    Animations[i].isPlaying = true;
                }
                else
                {
                    Animations[i].clip.Events.OnEnd = Idle;
                    Animations[i].isPlaying = false;
                }
            }
        }
    }

    private void FreeFall()
    {
        for (int i = 0; i < Animations.Count; i++)
        {
            if (Animations[i].NAME == "FreeFall")
            {
                if (!onGround  && !isJumpPressed && !isModified && playerVelocity < 0)
                {
                    isModified = false;
                    isMoving = false;
                    isFalling = true;
                    _Animancer.Play(Animations[i].clip);
                    Animations[i].isPlaying = true;
                }
                else
                {
                    Animations[i].clip.Events.OnEnd = Idle;
                    Animations[i].isPlaying = false;
                }
            }
        }
    } 

}










