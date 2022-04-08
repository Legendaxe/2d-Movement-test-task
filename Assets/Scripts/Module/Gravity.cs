using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour, IMovementModifier
{
    [Header("References")]
    [SerializeField] MyCharacterControler characterController;
    [SerializeField] MoveHandler handler;

    [Header("Settings")]
    [SerializeField] private float groundedPullMagnitude;

    private float gravityMagnitude;

    private bool wasGroundedLastFrame;



    public Vector2 Value { get; private set; }

    private void OnEnable() => handler.AddModifier(this);
    private void OnDisable() => handler.RemoveModifier(this);


    private void Awake()
    {
        gravityMagnitude = Physics2D.gravity.y;
    }

    private void Update() => ProcessGravity();

    private void ProcessGravity()
    {
        if (characterController.IsGrounded())
        {
            Value = new Vector2(Value.x, -groundedPullMagnitude);
        }
        else if (wasGroundedLastFrame)
        {
            Value = Vector2.zero;
        }
        else
            Value = new Vector2(Value.x, Value.y + gravityMagnitude * Time.deltaTime);
        wasGroundedLastFrame = characterController.IsGrounded();

    }




}
