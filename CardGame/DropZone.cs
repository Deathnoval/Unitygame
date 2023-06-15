using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        uint player = 0;  
        DragCard d= eventData.pointerDrag.GetComponent<DragCard>();
        ItemCard c = eventData.pointerDrag.GetComponent<ItemCard>();
       // ItemEnemyCard e= eventData.pointerDrag.GetComponent<ItemEnemyCard>();
        string a = Regex.Replace(this.transform.ToString(), "[^0-9]+", "");
        if(a!="")
        {
            player = Convert.ToUInt32(a);
        }    
        if (d != null /*&& (c.CardOfPlayer == player || player==0)*/)
        {
            if (player == 0)
            {
                c.isInUse = true;
            }
            else
            {
                c.isInUse = false;
            }
            d.parentToReturnTo = this.transform;
       
        }
    }
}
