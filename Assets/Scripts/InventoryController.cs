using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    

    // item sturct
    [System.Serializable]
    protected struct InventoryItem
    {
        public string name;
        public float weight;
    }

 
    protected Dictionary<int, InventoryItem> inventory = new Dictionary<int, InventoryItem>();

    
    [HideInInspector] public float weightTotal;

    // Other 
    [SerializeField] GameObject Player;
    protected MyCharacterControler myCharacterControler;



    private void Awake()
    {
        myCharacterControler = Player.GetComponent<MyCharacterControler>();
        weightTotal = 0;
    }

    
   

    public int GetFreeSlot()
    {
        return inventory.Count;
    }
    public void InventoryClear()
    {
        inventory.Clear();
        weightTotal = 0;
    }
    public void AddItem(string name, float weight, int order)
    {
        InventoryItem temp;
        temp.name = name ;
        temp.weight = weight;
        inventory.Add(order,temp);
        weightTotal += weight;
        myCharacterControler.WeightModChange(weightTotal);
    }
    public void RemoveItem(int order)
    {
        weightTotal -= inventory[order].weight;
        inventory.Remove(order);
        myCharacterControler.WeightModChange(weightTotal);
    }

}



