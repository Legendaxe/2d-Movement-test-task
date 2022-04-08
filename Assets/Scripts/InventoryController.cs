using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    

    // item sturct
    [System.Serializable]
    private struct InventoryItem
    {
        public string name;
        public float weight;
    }

 
    private Dictionary<int, InventoryItem> inventory = new Dictionary<int, InventoryItem>();

    
    [HideInInspector] private float weightTotal;

    // Other 
    [SerializeField] GameObject Player;
    private Weight weightControler;



    private void Awake()
    {
        weightControler = Player.GetComponent<Weight>();
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
        weightControler.WeightModChange(weightTotal);
    }
    public void RemoveItem(int order)
    {
        weightTotal -= inventory[order].weight;
        inventory.Remove(order);
        weightControler.WeightModChange(weightTotal);
    }

}



