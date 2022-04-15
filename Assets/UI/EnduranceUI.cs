using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnduranceUI : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Endurance endurance;
    private Slider slider;
    
    void Start()
    {
        slider = this.GetComponent<Slider>();   
    }

    private void FixedUpdate()
    {
        slider.value = endurance.CurrentEndurance;
    }
}
