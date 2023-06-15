
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

}

