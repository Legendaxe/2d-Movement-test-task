using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MyCharacterControler;

public class MoveHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MyCharacterControler characterControler;
    [SerializeField] private Weight weight;


    private readonly List<IMovementModifier> MovementModifiers = new List<IMovementModifier>();

    public void AddModifier(IMovementModifier modifier) => MovementModifiers.Add(modifier);
    public void RemoveModifier(IMovementModifier modifier) => MovementModifiers.Remove(modifier);


    private MoveType moveType;
    private int stand;

    public MoveType MoveType { set { moveType = value; } }
    public int Stand { set { stand = value; } }


    private void FixedUpdate() => Move();


    public void Move()
    {
        moveType = characterControler.Movetype;
        stand = characterControler.Stand;
        Vector2 movement = Vector2.zero;
        foreach(IMovementModifier modifier in MovementModifiers)
        {
            movement += modifier.Value;
            
        }
        DoAction( movement * weight.Value);

    }
    public void DoAction(Vector2 inputHorizontal)
    {
        if (stand != 2) // If character liyng we can't do anything
        {
            switch (moveType) // Check move 
            {
                case MoveType.Idle: // Standing here... (or sitting)
                    {
                        characterControler.StandOrSit();
                        break;
                    }
                case MoveType.Walk: // walking or crawling
                    {
                        characterControler.WalkOrCrawl(inputHorizontal);
                        break;
                    }
                case MoveType.Crouch: // slow walk or slow crawling
                    {
                        characterControler.SlowWalkOrSlowCrawl(inputHorizontal);
                        break;
                    }
                case MoveType.Ready: // First run stage
                    {
                        characterControler.FirstRunStage(inputHorizontal);
                        break;
                    }
                case MoveType.Steady: // Second run stage
                    {
                        characterControler.SecondRunStage(inputHorizontal);
                        break;
                    }
                case MoveType.GO: // Third run stage
                    {
                        characterControler.ThirdRunStage(inputHorizontal);
                        break;
                    }
                case MoveType.Dash: // Dash
                    {
                        characterControler.Dash(inputHorizontal);
                        break;
                    }
                case MoveType.Fall: // Fall
                    {
                        characterControler.Fall();
                        break;
                    }
                case MoveType.Grab: // Grab
                    {
                        characterControler.Grab(inputHorizontal * Vector2.right);
                        break;
                    }
                case MoveType.SoloLifting: // Lifting
                    {
                        characterControler.Lift();
                        break;
                    }
                case MoveType.OverJump: // Over jump
                    {
                        characterControler.JumpOver();
                        break;
                    }
            }
        }
        else
        {
            characterControler.Lying();
        }
    }


}
