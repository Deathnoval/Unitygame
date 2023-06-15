using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LimitCardInHand : MonoBehaviour
{
    public GameObject Hand;

    public int CountCard;
    void Update()
    {
        int x = 0;
        foreach(Transform child in Hand.transform)
        {
            x++;
        }    
        if(x!= CountCard)
        {
            CountCard = x;
        }    
    }
}
