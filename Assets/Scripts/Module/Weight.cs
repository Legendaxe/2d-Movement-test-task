using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weight : MonoBehaviour, IMovementModifier
{

    [System.Serializable]
    private struct WeightLimit
    {
        public int limit;
        public float modifier;
    }


    [Header("Settings")]
    [SerializeField] private WeightLimit[] weightLimit = new WeightLimit[3];



    public Vector2 Value { get; private set; }



    private void Start()
    {
        Value = Vector2.one;
    }



    public void WeightModChange(float weight)
    {
        if (weight > weightLimit[weightLimit.Length - 1].limit)
        {
            Value = Vector2.one * weightLimit[weightLimit.Length - 1].modifier;
            return;
        }
        foreach (var item in weightLimit)
        {
            if (weight < item.limit)
            {
                Value = Vector2.one * item.modifier;
                break;
            }

        }
    }
}
