using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadoutDropDown : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject invCntrl;
    [SerializeField] private Dropdown invDropdown;

    private InventoryController inventoryController;

    private void Awake()
    {
        inventoryController = invCntrl.GetComponent<InventoryController>();
    }

    public void ChangeInventoryStatus()
    {
        if (invDropdown.value == 0)
        { 
            inventoryController.InventoryClear();
            inventoryController.AddItem("Light Loadout", 20, inventoryController.GetFreeSlot());
        }
        else if(invDropdown.value == 1)
        {
            inventoryController.InventoryClear();
            inventoryController.AddItem("Medium Loadout", 40, inventoryController.GetFreeSlot());
        }
        else
        {
            inventoryController.InventoryClear();
            inventoryController.AddItem("Heavy Loadout", 50, inventoryController.GetFreeSlot());
        }
    }
}
