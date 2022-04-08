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

    [Header("UI")]
    [SerializeField] private Slider enduranceSlider;

    private float currentEndurance;
    private float enduranceTimer;

    private void Start()
    {
        currentEndurance = endurance;
        enduranceSlider.value = currentEndurance;
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
        enduranceSlider.value = currentEndurance;
        enduranceTimer -= Time.deltaTime;
    }

    public float CurrentEndurance { get { return currentEndurance; } } 

}
