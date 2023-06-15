
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UsePlayerCard : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GridManager gridManager;
    [SerializeField] Dice dice;
    [SerializeField] TurnBase turnBase;
    [SerializeField] Deck deck;
    public GameObject playerCard;
    public List<Player> players = new List<Player>();
    public  CharacterCard card;
    private int count = 0;
    public bool checkHeal;
    public GameObject healEffect;
    public GameObject rangeEffect;
    public GameObject dameUpEffect;
    public GameObject resultText;
    public Text result;
    void Start()
    {
        resultText.SetActive(false);
        players = gridManager.GetAllPlayers();
        card = GetComponent<CharacterCard>();
    }
     void Update()
    {
        if (players[0].playerTurn==true && count == 0)
        {
            card.isInUse = true;
        }
        else
        {
            card.isInUse = false;
        }
        if (card.cardIdPlayer == 2)
        {
            players[0].maxHeal = 25;
        }
        //else if(card.cardIdPlayer == 6)
        //{
        //    result.text = "Chua lam xong";
        //    resultText.SetActive(true);
        //    StartCoroutine(delay());
        //}    
        if(turnBase.currentPhase==TurnBase.PHASE.DRAW)
        {
            count = 0;
        }    
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (card.isInUse)
        {
           switch(card.cardIdPlayer)
            {
                case 1:
                    if (dice.temp != 0)
                    {
                        if (players[0].range < dice.temp)
                        {
                          //  GameObject rangeUp = Instantiate(rangeEffect, players[0].transform.position, Quaternion.identity);
                            players[0].range += 1;
                            //  Destroy(rangeUp, 3f);
                            result.text = "Da su dung chuc nang tam tan cong tang: " + players[0].range.ToString();
                            resultText.SetActive(true);
                            StartCoroutine(delay());

                        }
                    }
                    break;
                case 2:
                    result.text = "Mau toi da tang: " + players[0].maxHeal;
                    resultText.SetActive(true);
                    StartCoroutine(delay());
                    break;
                  
                case 3:
                   // GameObject dameUp = Instantiate(dameUpEffect, players[0].transform.position, Quaternion.identity);
                    players[0].dame += 1;
                    // Destroy(dameUp, 3f);
                    result.text = "Da su dung chuc nang dame tang: " + players[0].dame.ToString();
                    resultText.SetActive(true);
                    StartCoroutine(delay());

                    break;
                case 4:
                    if (dice.temp != 0)
                    {
                        dice.temp = ++dice.temp;
                        players[0].moveDistance = dice.temp;
                        result.text = "Da su dung chuc nang tam di chuyen tang: " + players[0].moveDistance.ToString();
                        resultText.SetActive(true);
                        StartCoroutine(delay());
                    }
                    break;
                case 5:
                    //GameObject healing = Instantiate(healEffect, players[0].transform.position, Quaternion.identity);
                    checkHeal = true;
                    // Destroy(healing, 3f);
                    result.text = "Su dung go hoi 2 mau";
                    resultText.SetActive(true);
                    StartCoroutine(delay());
                    break;
                case 6:
                    result.text = "Chua lam xong";
                    resultText.SetActive(true);
                    StartCoroutine(delay());
                    break;
            }    
        }
        count++;
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.75f);
        resultText.SetActive(false);
    }    
}
