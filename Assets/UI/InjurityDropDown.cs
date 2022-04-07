using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InjurityDropDown : MonoBehaviour
{
    [SerializeField] protected GameObject injCntrl;
    [SerializeField] protected Dropdown injDropdown;

    protected InjurityController injurityController;

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
