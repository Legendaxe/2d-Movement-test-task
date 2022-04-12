using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjurityController : MonoBehaviour
{

    // Limb sturct
    [System.Serializable]
    private struct Limb
    {
        public string name;
        public bool injurityStatus;
        public int healthPoint;
    }

    // Staff for import Limbs;
    private Dictionary<string, Limb> limbs = new Dictionary<string, Limb>();
    [SerializeField] private Limb[] limbsImport = new Limb[5];


    // Other 
    [SerializeField] GameObject player;
    private MyCharacterControler myCharacterControler;

    

    private void Awake()
    {
        myCharacterControler = player.GetComponent<MyCharacterControler>();
        FullHeal();
    }

    public bool InjurityCheck(string limbName)
    {
        if (limbs[limbName].injurityStatus)
        {
            return true;
        }
        return false;
    }
    // Full heal
    public void FullHeal()
    {
        limbs.Clear();
        foreach (var item in limbsImport)
        {
            limbs.Add(item.name, item);
        }
    }

    public void NewLimbStatus(string limbname, int hpChange, bool status )
    {
        Limb temp = limbs[limbname];
        temp.healthPoint += hpChange;
        temp.injurityStatus = status;
        limbs[limbname] = temp;
        
    }

}
