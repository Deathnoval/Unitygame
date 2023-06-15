using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] GridManager gridManager;
    public List<Player> players=new List<Player>();
    public Text victoryText;
    public GameObject panel;
    void Start()
    {
        panel.SetActive(false);
        players = gridManager.GetAllPlayers();
    }
    void Update()
    {
        if (players[0].heal <=0)
        {
            victoryText.text = "You Lose";
            panel.SetActive(true);
           
        }   
        if(players[1].heal <= 0)
        {
            victoryText.text = "You Win";
            panel.SetActive(true);
         
            return;
        }    
    }
}
