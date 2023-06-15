using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyUseCard : MonoBehaviour
{

    [SerializeField] GridManager gridManager;
    [SerializeField] Dice dice;
    public GameObject playerCard;
    public List<Player> players = new List<Player>();
    public EnemyCard card;
    public int count = 0;
    void Start()
    {
        players = gridManager.GetAllPlayers();
        card = GetComponent<EnemyCard>();
    }
    void Update()
    {
        if (players[1].playerTurn == true && count == 0)
        {
            card.isInUse = true;
        }
        else
        {
            card.isInUse = false;
        }
        if (card.cardIdEnemy == 3)
        {
            players[1].maxHeal = 25;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (card.isInUse)
        {
            switch (card.cardIdEnemy)
            {
                case 1:
                    break;
                case 2:
                    players[0].dame = 2;
                    break;
                case 4:
                    Debug.Log("the 4");
                    break;
                case 5:
                    if (dice.temp != 0)
                    {
                        dice.temp = dice.temp + 1;

                    }
                    players[1].moveDistance = dice.temp;

                    Debug.Log("the 5");
                    break;
                case 6:
                    Debug.Log("the 6");
                    break;
                case 7:
                    Debug.Log("the 7");
                    break;
                case 8:
                    Debug.Log("the 8");
                    break;
            }
        }
        count++;
    }
}
