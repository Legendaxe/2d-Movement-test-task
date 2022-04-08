using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InjurityDropDown : MonoBehaviour
{
    [SerializeField] private GameObject injCntrl;
    [SerializeField] private Dropdown injDropdown;

    private InjurityController injurityController;

    private void Awake()
    {
        injurityController = injCntrl.GetComponent<InjurityController>();
    }

    public void ChangeInjurityStatus()
    {
        if (injDropdown.value == 0)
            injurityController.FullHeal();
        else
            injurityController.NewLimbStatus("Legs", 0, true);
    }
    
}
