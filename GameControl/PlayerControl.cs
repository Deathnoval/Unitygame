using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerControl : MonoBehaviour
{
  
    public GameObject playerCard;
    public GameObject enemyCard;
    public List<Player> players = new List<Player>();
    public List<CharacterCard> playerCards = new List<CharacterCard>();
    public List<CharacterCard> enemyCards = new List<CharacterCard>();


    void Start()
    {
        players = GetComponent<GridManager>().GetAllPlayers();
        SetPlayerCard();
        SetEnemyCard();
    }

    void Update()
    {
        CheckInUse();
    }

    void SetPlayerCard()
    {
        CharacterCard[] cardsToAdd = playerCard.GetComponentsInChildren<CharacterCard>();
        foreach (CharacterCard card in cardsToAdd)
        {
            if (!playerCards.Contains(card))
            {
                playerCards.Add(card);
            }
        }
     
    }

    void SetEnemyCard()
    {
        CharacterCard[] cardsToAdd = enemyCard.GetComponentsInChildren<CharacterCard>();
        foreach (CharacterCard card in cardsToAdd)
        {
            if (!enemyCards.Contains(card))
            {
                enemyCards.Add(card);
            }
        }

    }

    public void ApplyPlayerCardEffect()
    {
        foreach (CharacterCard card in playerCards)
        {
            switch (card.cardIdPlayer)
            {
                case 1:
       
                    break;
                case 2:
            
                    break;
                case 3:
         
                    break;
                case 4:
           
                    break;
                case 5:
           
                    break;
                case 6:
            
                    break;
                case 7:
          
                    break;
                case 8:
         
                    break;
            }
        }
    }

    public void ApplyEnemyCardEffect()
    {
        foreach (CharacterCard card in enemyCards)
        {
            switch (card.cardIdPlayer)
            {
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;
                case 4:

                    break;
                case 5:

                    break;
                case 6:

                    break;
                case 7:

                    break;
                case 8:

                    break;
            }
        }

    }
   public void CheckInUse()
    {
        if (players[0].playerTurn == true)
        {
            playerCards[0].isInUse = true;
        }
        else 
        {
            playerCards[0].isInUse = false;
        }
       if (players[1].playerTurn == true)
        {
            enemyCards[0].isInUse = true;
        }
        else
        {
            enemyCards[0].isInUse = false;
        }
    }
}
