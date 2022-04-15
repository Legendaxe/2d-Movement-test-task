using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Endurance : MonoBehaviour
{


    [SerializeField] MyCharacterControler controller;


    [Header("Settings")]
    [SerializeField] private float endurance;
    [SerializeField] private int enduranceRegenSpeed;
    [SerializeField] private int enduranceWasteSpeed;


    private float currentEndurance;
    private float enduranceTimer;

    public float CurrentEndurance { get { return currentEndurance; } }


    private void Start()
    {
        currentEndurance = endurance;
        enduranceTimer = 10;
    }


    private void Update() => EnduranceRefresh();


    private void EnduranceRefresh()
    {
        if ((controller.Movetype == MyCharacterControler.MoveType.Idle) && (enduranceTimer < 1))
        {
            currentEndurance += enduranceRegenSpeed;
            enduranceTimer = 10;
        }
        else if ((controller.Movetype == MyCharacterControler.MoveType.GO) && (enduranceTimer < 7))
        {
            currentEndurance -= enduranceWasteSpeed;
            enduranceTimer = 10;
        }
        enduranceTimer -= Time.deltaTime;
    }
}
