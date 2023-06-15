using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridControl : MonoBehaviour
{
    [SerializeField] Tilemap targetTilemap; 
    [SerializeField] GridManager gridManager;


    Player selectedPlayer;

    private void Update()
    {
       // MouseInput();
    
    }
   private void MouseInput()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int clickPositon = targetTilemap.WorldToCell(worldPoint);
        if (Input.GetMouseButtonDown(0))
        {
             Debug.Log(clickPositon);
        }
        //if (Input.GetMouseButtonDown(1))
        //{
        //    selectedPlayer = gridManager.GetPlayer(clickPositon.x, clickPositon.y);
        //    if(selectedPlayer != null)
        //    {
        //        Debug.Log(selectedPlayer.Name + " " + clickPositon);
        //    }    
        //}
    }    
}
