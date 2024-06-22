//using System.Collections;
//using System.Collections.Generic;
//using System;
//using UnityEngine;
//using Animancer;
//using Animancer.FSM;

//public class CharacterMovement__ : MonoBehaviour
//{
//    [SerializeField] private ThirdPersonController TPS;
//    [SerializeField] private AnimancerComponent _Animancer;
//    [SerializeField] private ClipTransition _Idle;
//    [SerializeField] private float playerVelocity;
//    [SerializeField] private bool  onGround;
//    [SerializeField] private bool isMoving;
//    [SerializeField] private bool isModified;
//    [SerializeField] private bool isFalling;
//    [SerializeField] private bool isJumpPressed;
  


//    [Serializable]
//    public class Movement
//    {
//        public string name;
//        public ClipTransition clip;
//        public bool isPlaying;
//        public bool rootmotion;
//        public CharacterMovement CA;
//        public string NAME 
//        {
//            get
//            {
//                return name;
//            }
//            set
//            {
//                 NAME = name;
//            }
//        }
//    }

//    [SerializeField] public List<Movement> Animations = new List<Movement>();

//    private void PlayAnim(string clipname, bool isMoving, bool onGround, bool isModified,bool isJump)
//    {
//        if (Animations.Exists(x => x.NAME == clipname))
//        {
//            int i = (Animations.FindIndex(i => i.NAME == clipname));
//            if (Animations[i].CA.isMoving == isMoving && 
//                Animations[i].CA.onGround == onGround &&
//                Animations[i].CA.isModified == isModified &&
//                Animations[i].CA.isJumpPressed == isJump)
//            {
//                 Animations[i].CA.isMoving = false;
//                 Animations[i].CA.onGround = false;
//                 Animations[i].CA.isModified = false;
//                 Animations[i].CA.isJumpPressed = false;
//                _Animancer.Play(Animations[i].clip);
//                Animations[i].isPlaying = true;
//            }
//            else
//            {
//                Animations[i].clip.Events.OnEnd = Idle;
//                Animations[i].isPlaying = false;
//            }
//        }
//    }

//    private void Awake()
//    {
//        Idle();     
//    }

//    private void Update()
//    {
//        PlayAnim("Walk", true, true, false, false);
//        PlayAnim("Run", true, true, true, false);
//        PlayAnim("Jump", false, false, false, true);
//        PlayAnim("Jump", true, false, false, true);
//        PlayAnim("Jump", true, false, true, true);
//        FreeFall();
//        AnimancerLayer.SetMaxStateDepth(1000);
//        onGround = TPS.isgrounded;
//        isFalling = TPS.isfreeFall;
//        playerVelocity = TPS.playerVelocity.y;

//        ControlInputs();
//    }

//    private void ControlInputs()
//    {
//        if (Input.GetButtonDown("Jump"))
//        {
//            isJumpPressed = true;
//        }
//        if (Input.GetButton("Shift"))
//        {
//            isModified = true;
//        }
//        else
//        {
//            isModified = false;
//        }
//        if (Input.GetAxisRaw("Vertical") != 0)
//        {
//            isMoving = true;
//        }
//        else
//        {
//            isMoving = false;
//        }
//    }

//    private void Idle()
//    {
//        _Animancer.Play(_Idle);
//    }
//    private void FreeFall()
//    {
//        for (int i = 0; i < Animations.Count; i++)
//        {
//            if (Animations[i].NAME == "FreeFall")
//            {
//                if (!onGround  && !isJumpPressed && !isModified && playerVelocity < -2.5)
//                {
//                    isModified = false;
//                    isMoving = false;
//                    isFalling = true;
//                    _Animancer.Play(Animations[i].clip);
//                    Animations[i].isPlaying = true;
//                }
//                else
//                {
//                    Animations[i].clip.Events.OnEnd = Idle;
//                    Animations[i].isPlaying = false;
//                }
//            }
//        }
//    }  

//}










