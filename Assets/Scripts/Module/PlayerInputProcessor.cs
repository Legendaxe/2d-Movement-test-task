using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static MyCharacterControler;

public class PlayerInputProcessor : MonoBehaviour, IMovementModifier
{

    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private InjurityController injurityController;
    private MyCharacterControler characterController;
    private Endurance enduranceController;
    private MoveHandler handler;



    // Movement staff
    private Vector2 direction;
    private int stand;
    private float runCurrentSpeed;
    private float runMaxSpeed;
    private float runMinSpeed;
    private float runAccelorationTime;
    private float runAcceloration;

    private float runDashBoost = 1;
    private MoveType moveType;


    // Lift staff
    private float liftHeight;
    private float liftSpeed;





    // Input staff
    private bool capsLock;
    private float inputHorizontal;
    private bool changeDirection;
    private float LeftDashTimer;
    private float RightDashTimer;
    private float inputAcceloration = .05f;

    private InputControl Control;



    public Vector2 Value { get; private set; }
    public float RunTimer { set { runCurrentSpeed = value; } }
    public float RunTimerModify { get { return runDashBoost; }  set { runDashBoost = value; } }


    private void Awake()
    {
        Control = new InputControl();
        characterController = player.GetComponent<MyCharacterControler>();
        enduranceController= player.GetComponent<Endurance>();
        handler = player.GetComponent<MoveHandler>();

    }

    private void OnEnable()
    {
        Control.Player.Enable();
        handler.AddAddModifier(this);
    }

    
    private void OnDisable()
    {
        Control.Player.Disable();
        handler.RemoveAddModifier(this);
    }


    private void Start()
    {
        liftSpeed = characterController.LiftSpeed;

        moveType = MoveType.Idle;
        direction = Vector2.right;
        
        LeftDashTimer = 0;
        RightDashTimer = 0;

        runAccelorationTime = characterController.RunAccelorationTime;
        runMaxSpeed = characterController.RunMaxSpeed;
        runAcceloration = runMaxSpeed / runAccelorationTime;
        runMinSpeed = runMaxSpeed * 0.6f;
        runCurrentSpeed = runMinSpeed;
        capsLock = false;
    }


    private void Update()
    {


        inputHorizontal = Mathf.MoveTowards(inputHorizontal, Control.Player.InputHorizontal.ReadValue<float>(), inputAcceloration);
        InputHorizontal(inputHorizontal);
    }




    public void Capslock() => capsLock = !capsLock;


    private bool InputPossibility()
    {
        if ((moveType != MoveType.Dash) && (moveType != MoveType.ReDash) && (moveType != MoveType.SoloLifting) && (moveType != MoveType.OverJump))
        {
            if (characterController.IsGrounded())
            {
                return true;
            }
            else
                moveType = MoveType.Fall;
        }
        return false;
    }
    public void InputHorizontal(float inputHorizontal )
    {
        moveType = characterController.Movetype;
        if (InputPossibility())
        {
            if (inputHorizontal != 0)
            {
                Value = Vector2.right * inputHorizontal;
                if (inputHorizontal * direction.x < 0)
                    changeDirection = true;
                direction.x = inputHorizontal > 0 ? 1 : -1;
                if ((Control.Player.RunButton.phase == InputActionPhase.Performed)&& (enduranceController.CurrentEndurance > 5))
                {
                    if (changeDirection)
                    {
                        moveType = MoveType.ReDash;
                        changeDirection = false;
                    }
                    else
                    {
                        if (runCurrentSpeed < runMaxSpeed * 0.8f)
                        {
                            moveType = MoveType.Ready;
                            runCurrentSpeed += Time.deltaTime * runAcceloration * runDashBoost;
                            stand = 0;
                        }
                        else if (runCurrentSpeed < runMaxSpeed)
                        {
                            moveType = MoveType.Steady;
                            runCurrentSpeed += Time.deltaTime * runAcceloration * runDashBoost;
                        }
                        else
                        {
                            runDashBoost = 1;
                            moveType = MoveType.GO;
                        }
                        Value = new Vector2(runCurrentSpeed * inputHorizontal, 0);
                    }

                }
                else if (Control.Player.ObstecleInteractionKey.phase == InputActionPhase.Performed)
                {
                    if (characterController.WallCheck(direction, 3f, -characterController.HalfOfCharacterHeight) && characterController.GrabCheck())
                        moveType = MoveType.Grab;
                    else
                        moveType = MoveType.Walk;
                }
                // Crouch
                else if (capsLock)
                    moveType = MoveType.Crouch;
                // Walk
                else
                    moveType = MoveType.Walk;

                if (changeDirection)
                {
                    characterController.RotateCharacter(0, 180, 0);
                    changeDirection = false;
                }
                if (Control.Player.RunButton.phase != InputActionPhase.Performed)
                    runCurrentSpeed = runMinSpeed;
                if (injurityController.InjurityCheck("Legs"))
                    moveType = MoveType.Crouch;
            }
            else
            {
                moveType = MoveType.Idle;
                runCurrentSpeed = runMinSpeed;
            }
        }
        characterController.Movetype = moveType;
        characterController.Stand = stand;
    }


    public void DoubleTapRightDash(InputAction.CallbackContext obj)
    {
        if (obj.canceled)
        {
            RightDashTimer = Time.time;
        }
        else if (obj.performed)
        {
            if (Time.time - RightDashTimer < 0.2f)
            {
                moveType = characterController.Movetype;
                if (InputPossibility())
                {
                    if (((moveType == MoveType.Idle) || (moveType == MoveType.Walk) || (moveType == MoveType.Crouch)) && (stand != 2) && (direction.x > 0) && (!injurityController.InjurityCheck("Legs")))
                    {
                        moveType = MoveType.Dash;
                        characterController.DashTimer = Time.time;
                        runDashBoost = 1.5f;
                        stand = 0;
                        Value = Vector2.right * inputHorizontal;
                    }
                }
                characterController.Movetype = moveType;
                characterController.Stand = stand;
            }
        }

    }

    public void DoubleTapLeftDash(InputAction.CallbackContext obj)
    {
        if (obj.canceled)
        {
            LeftDashTimer = Time.time;
        } 
        else if (obj.performed)
        {
            if (Time.time - LeftDashTimer < 0.2f)
            {
                moveType = characterController.Movetype;
                if (InputPossibility())
                {
                    if (((moveType == MoveType.Idle) || (moveType == MoveType.Walk) || (moveType == MoveType.Crouch)) && (stand != 2) && (direction.x < 0) &&(!injurityController.InjurityCheck("Legs")))
                    {
                        moveType = MoveType.Dash;
                        characterController.DashTimer = Time.time;
                        runDashBoost = 1.5f;
                        stand = 0;
                        Value = Vector2.right * inputHorizontal;
                    }
                }
                characterController.Movetype = moveType;
                characterController.Stand = stand;
            }
        }
    }


    public void KeySit()
    {
        moveType = characterController.Movetype;
        if (InputPossibility())
            stand = stand == 1 ? (characterController.WallCheck(transform.up, 3f, characterController.HalfOfCharacterHeight) ? 0 : 1) : 1;
        characterController.Movetype = moveType;
        characterController.Stand = stand;
    }
    public void KeyLie()
    {
        moveType = characterController.Movetype;
        if (InputPossibility())
        {
            stand = stand == 2 ? 0 : ((characterController.WallCheck(transform.right, 3f, -characterController.HalfOfCharacterHeight)
            && characterController.WallCheck(-transform.right, 3f, -characterController.HalfOfCharacterHeight)) ? 2 : 0);
        }
        characterController.Movetype = moveType;
        characterController.Stand = stand;
    }
   
    public void KeyObstacleInteraction()
    {
        moveType = characterController.Movetype;

        if ((InputPossibility()) && (moveType != MoveType.Grab))
        {
            liftHeight = characterController.HeightOfObstecle();
            if (liftHeight != 0)
            {
                characterController.LiftDuration = (liftHeight + 0.5f) / liftSpeed;
                characterController.LiftTimer = Time.time;
                if (liftHeight < characterController.HeightOfCharacter)
                    moveType = MoveType.SoloLifting;

            }   
        }
        characterController.Movetype = moveType;
        characterController.Stand = stand;
    }
    public void KeyOverJump()
    {
        moveType = characterController.Movetype;
        if (InputPossibility())
        {
            if (characterController.OverJumpCheck())
            {
                characterController.LiftDuration = characterController.HeightOfObstecle() / liftSpeed;
                characterController.LiftTimer = Time.time;
                moveType = MoveType.OverJump;
            }
        }
        characterController.Movetype = moveType;
        characterController.Stand = stand;
    }


    
}
