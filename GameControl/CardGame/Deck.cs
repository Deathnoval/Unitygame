
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{

    public List<CardData> deck = new List<CardData>();
    public List<CardData> myList = new List<CardData>();
    public List<CardData> container = new List<CardData>();
    public static List<CardData> staticDeck = new List<CardData>();

    //####18/6
    public List<CardData>CardsInHand= new List<CardData>();
    [SerializeField] Dice dice;
    [SerializeField] GameControl gameControl;
    public GameObject Hand;
    public GameObject Zone; 
    public int count = 1;
    public bool[] AiCanSummon;
    public int[] cardsId;
    public int SummonThisId;
    public ItemEnemyCard itemEnemyCard;
    public int summonID;
    public int howManyCards;
    /*    public bool Player2CanPlay;
    */    //
    public GameObject cardIndex1;
    public GameObject cardIndex2;
    public GameObject cardIndex3;
    public GameObject cardIndex4;

    public GameObject CardToHand;
    public GameObject CardToHand2;

    public GameObject cardShuffle;

    public GameObject DeckPanel;
  
    public GameObject[] Clones;

    public static int deckSize = 90;


    void Start()
    {
        //####18/6
        StartCoroutine(WaitFiveSecond());
        //


        string jsonFilePath = Path.Combine(Application.streamingAssetsPath, "cardData.json");
        if (File.Exists(jsonFilePath))
        {
            string jsonContent = File.ReadAllText(jsonFilePath);
            CardManager.MyList cardList = JsonUtility.FromJson<CardManager.MyList>(jsonContent);
           List<CardData> cardToDisplay = cardList.cardList.FindAll(card => card.cardType == 2);
            if(cardToDisplay!=null)
            {
                myList.AddRange(cardToDisplay);
            }
            CreateDeck();
 
        }
    }

     void Update()
    {
        staticDeck = deck;
        if(deckSize<20)
        {
            cardIndex1.SetActive(false);
        }    
         if(deckSize<15)
        {
            cardIndex2.SetActive(false);
        }
         if (deckSize<10)
        {
            cardIndex3.SetActive(false);
        }
         if (deckSize<2)
        {
            cardIndex4.SetActive(false);
        }    
        
        if (deckSize <= 0)
        {
            ResetDeck();
        }
       /* //##18/6
        if(Player2CanPlay==true)
        {
            for (int i = 0; i < 90; i++)
            {
                //Debug.Log(i);
                if (ItemEnemyCard.CardsInHandStatic[i].cardId!=0)
                {

                    CardsInHand[i] = ItemEnemyCard.CardsInHandStatic[i];
                    
                }
            }
        }*/
        //

        if(0==0)
        {
            int j = 0;
            howManyCards = 0;
            foreach(Transform child in Hand.transform)
            {
                howManyCards++;
            }
            foreach(Transform child in Hand.transform)
            {
                CardsInHand[j] = child.GetComponent<ItemEnemyCard>().listCard[0];
                j++;
            }
            for(int i=0; i<90;i++)
            {

                if(i>=howManyCards)
                {
                    CardsInHand[i] = myList[0];
                }
            }
            j = 0;
        }
        if (gameControl.players[1].playerTurn==true)
        {
            for(int i=0; i<90;i++)
            {
                if (CardsInHand[i].cardId!=0)
                {
                    AiCanSummon[i] = true;
                }
            }
        }
        else
        {
            for(int i=0; i<90;i++)
            {
                AiCanSummon[i]=false;
            }
        }
        if (gameControl.players[1].playerTurn == true && dice.hasRolledDice == true)
        {
            summonID = 0;
            SummonThisId = 0;
            int index = 0;
            for (int i = 0; i < 90; i++)
            {
                if (AiCanSummon[i] == true)
                {
                    cardsId[index] = CardsInHand[i].cardId;
                    index++;
                }
            }
            for (int i = 0; i < 90; i ++)
            {
                if(cardsId[i]!=0)
                {
                    if (cardsId[i]>summonID)
                    {
                        summonID = cardsId[i];
                    }
                }
            }
            SummonThisId=summonID;
            foreach(Transform child in Hand.transform) { 
                if(child.GetComponent<ItemEnemyCard>().cardIdGame==SummonThisId && count<2 && dice.hasRolledDice==true)
                {
                    Debug.Log(dice.hasRolledDice);
                    child.transform.SetParent(Zone.transform);
                    count++;
                    break;
                }

             }
        }
        if (gameControl.players[0].playerTurn==true)
        {
            count = 1;
        }
    }
    void CreateDeck()
    {
   
        for (int i = 0; i < deckSize; i++)
        {
            int x = Random.Range(0,4);
            deck[i] = myList[x];
        }
    }
    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Clones = GameObject.FindGameObjectsWithTag("Clone");
        foreach (GameObject Clone in Clones)
        {
            if (Clone != null)
            {
                Destroy(Clone);
            }
        }
    }
    public IEnumerator StartGame()
    {
        for (int i = 0; i <= 4; i++)
        {
            yield return new WaitForSeconds(0.4f);
            Instantiate(CardToHand, transform.position, transform.rotation);
        }
        yield return new WaitForSeconds(0.4f);
        Shuffle();
    }
    public IEnumerator DrawCard()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.4f);
            CardToHand.GetComponent<DragCard>().enabled = false;
            Instantiate(CardToHand, transform.position, transform.rotation);
           
        }
    }
    public void Draw()
    { 
            StartCoroutine(DrawCard());
    }

    public void Shuffle()
    {
        for (int i = 0; i < deckSize; i++)
        {
            container[0] = deck[i];
            int random = Random.Range(i, deckSize);
            deck[i] = deck[random];
            deck[random] = container[0];
        }
        Instantiate(cardShuffle, transform.position, transform.rotation);
        StartCoroutine(DelayedDestroy());
    }

   

    public IEnumerator StartGame2()
    {
        for (int i = 0; i <= 4; i++)
        {
            yield return new WaitForSeconds(0.4f);
            Instantiate(CardToHand2, transform.position, transform.rotation);
        }
        yield return new WaitForSeconds(0.4f);
    }
    public IEnumerator DrawCard2()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.4f);
            Instantiate(CardToHand2, transform.position, transform.rotation);
        }
    }
    public void Draw2()
    {
           StartCoroutine(DrawCard2());
    }
    void ResetDeck()
    {
        
        deckSize = 25;
        deck = new List<CardData>();
        CreateDeck();
    }
    //###18/6
    IEnumerator WaitFiveSecond()
    {
        yield return new WaitForSeconds(5);
/*        Player2CanPlay = true;
*/    }
    //

}

