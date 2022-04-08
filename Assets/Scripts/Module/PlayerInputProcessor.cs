using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static MyCharacterControler;

public class PlayerInputProcessor : MonoBehaviour, IMovementModifier
{

    [Header("References")]
    [SerializeField] MyCharacterControler characterController;
    [SerializeField] InjurityController injurityController;
    [SerializeField] Endurance enduranceController;
    [SerializeField] MoveHandler handler;



    // Movement staff
    private Vector2 direction;
    private int stand;
    private float runTimer;
    private MoveType moveType;


    // Lift staff
    private float liftHeight;
    private float liftSpeed;





    // Input staff
    private bool capsLock;
    private float inputHorizontal;
    private float acceloratopn = 0.05f;
    private InputControl Control;



    public Vector2 Value { get; private set; }
    public float RunTimer { set { runTimer = value; } }


    private void Awake() => Control = new InputControl();


    private void OnEnable()
    {
        Control.Player.Enable();
        Control.Player.DoubleTapLeftDash.performed += DoubleTapLeftDash_performed;
        Control.Player.DoubleTapRightDash.performed += DoubleTapRightDash_performed;
        handler.AddModifier(this);
    }

    
    private void OnDisable()
    {
        Control.Player.Disable();
        handler.RemoveModifier(this);
    }


    private void Start()
    {
        liftSpeed = characterController.LiftSpeed;
        moveType = MoveType.Idle;
        direction = Vector2.right;
        capsLock = false;
    }


    private void Update()
    {


        inputHorizontal = Mathf.MoveTowards(inputHorizontal, Control.Player.InputHorizontal.ReadValue<float>(), acceloratopn);
        InputHorizontal(inputHorizontal);
    }




    public void Capslock()
    {
        capsLock = !capsLock;
    }
    

    public void InputHorizontal(float inputHorizontal )
    {
        moveType = characterController.Movetype;
        if ((moveType != MoveType.Dash) && (moveType != MoveType.SoloLifting) && (moveType != MoveType.OverJump))
        {
            if (characterController.IsGrounded())
            {
                if (inputHorizontal != 0)
                {
                    Value = Vector2.right * inputHorizontal;
                    // Rotate character if he changed direction
                    if (inputHorizontal * direction.x < 0)
                        characterController.Rotate();
                    direction.x = inputHorizontal > 0 ? 1 : -1;
                    if (Control.Player.RunButton.phase == InputActionPhase.Performed  )
                    {
                        if (enduranceController.CurrentEndurance > 5)
                        {
                            if (runTimer == 0)
                            {
                                moveType = MoveType.Ready;
                                stand = 0;
                            }
                            else if (runTimer < 2)
                                moveType = MoveType.Steady;
                            else
                                moveType = MoveType.GO;
                            runTimer += Time.deltaTime;
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

                    if (injurityController.InjurityCheck("Legs"))
                        moveType = MoveType.Crouch;
                }
                else
                    moveType = MoveType.Idle;
            }
            else
                moveType = MoveType.Fall;
        }
        characterController.Movetype = moveType;
        characterController.Stand = stand;
    }


    private void DoubleTapRightDash_performed(InputAction.CallbackContext obj)
    {
        moveType = characterController.Movetype;

        if ((moveType != MoveType.Dash) && (moveType != MoveType.SoloLifting) && (moveType != MoveType.OverJump))
        {
            if (characterController.IsGrounded())
            {
                if (((moveType == MoveType.Idle) || (moveType == MoveType.Walk) || (moveType == MoveType.Crouch)) && (stand != 2) && (direction.x > 0))
                    if (enduranceController.CurrentEndurance > 30)
                    {
                        moveType = MoveType.Dash;
                        characterController.DashTimer = Time.time;
                        stand = 0;
                    }
            }
            else
                moveType = MoveType.Fall;
        }
        characterController.Movetype = moveType;
        characterController.Stand = stand;

    }

    private void DoubleTapLeftDash_performed(InputAction.CallbackContext obj)
    {
        moveType = characterController.Movetype;
        if ((moveType != MoveType.Dash) && (moveType != MoveType.SoloLifting) && (moveType != MoveType.OverJump))
        {
            if (characterController.IsGrounded())
            {
                if (((moveType == MoveType.Idle) || (moveType == MoveType.Walk) || (moveType == MoveType.Crouch)) && (stand != 2) && (direction.x < 0))
                    if (enduranceController.CurrentEndurance > 30)
                    {
                        moveType = MoveType.Dash;
                        characterController.DashTimer = Time.time;
                        stand = 0;
                        Value = Vector2.right * inputHorizontal;
                        Debug.Log(moveType);
                    }
            }
            else
                moveType = MoveType.Fall;
        }
        characterController.Movetype = moveType;
        characterController.Stand = stand;
    }


    public void KeySit()
    {
        moveType = characterController.Movetype;

        if ((moveType != MoveType.Dash) && (moveType != MoveType.SoloLifting) && (moveType != MoveType.OverJump))
        {
            if (characterController.IsGrounded())
            {
                stand = stand == 1 ? (characterController.WallCheck(transform.up, 3f, characterController.HalfOfCharacterHeight) ? 0 : 1) : 1;
            }
            else
                moveType = MoveType.Fall;
        }
        characterController.Movetype = moveType;
        characterController.Stand = stand;
    }
    public void KeyLie()
    {
        moveType = characterController.Movetype;

        if ((moveType != MoveType.Dash) && (moveType != MoveType.SoloLifting) && (moveType != MoveType.OverJump))
        {
            if (characterController.IsGrounded())
            {
                stand = stand == 2 ? 0 : ((characterController.WallCheck(transform.right, 3f, -characterController.HalfOfCharacterHeight)
                && characterController.WallCheck(-transform.right, 3f, -characterController.HalfOfCharacterHeight)) ? 2 : 0);
            }
            else
                moveType = MoveType.Fall;
        }
        characterController.Movetype = moveType;
        characterController.Stand = stand;
    }
   
    public void KeyObstacleInteraction()
    {
        moveType = characterController.Movetype;

        if ((moveType != MoveType.Dash) && (moveType != MoveType.SoloLifting) && (moveType != MoveType.OverJump) && (moveType != MoveType.Grab))
        {
            if (characterController.IsGrounded())
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
            else
                moveType = MoveType.Fall;
        }
        characterController.Movetype = moveType;
        characterController.Stand = stand;
    }
    public void KeyOverJump()
    {
        moveType = characterController.Movetype;

        if ((moveType != MoveType.Dash) && (moveType != MoveType.SoloLifting) && (moveType != MoveType.OverJump))
        {
            if (characterController.IsGrounded())
            {
                
                if (characterController.OverJumpCheck())
                {
                    characterController.LiftDuration = characterController.HeightOfObstecle() / liftSpeed;
                    characterController.LiftTimer = Time.time;
                    moveType = MoveType.OverJump;
                }
            }
            else
                moveType = MoveType.Fall;
        }
        characterController.Movetype = moveType;
        characterController.Stand = stand;
    }


    
}
