using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ItemEnemyCard : MonoBehaviour
{
    public List<CardData> listCard = new List<CardData>();
    //####18/6
   /* public static List<CardData> CardsInHandStatic=new List<CardData>();
    public List<CardData> CardsInHand=new List<CardData>();
    public static int CardsInHandNumber;*/
    //

    public List<Player> players;

    public GameObject[] enemyItems;
    public GameObject cardBack;
    private int cardIdData = 7;
    private string cardName;
    private string cardDescription;
    public GameObject dice;
    public Dice diceroll;
    private int cardPower;
    public Text nameText;
    public int cardIdGame;
    public Text descriptionText;
    public Sprite thisSprite;
    public Image img;
    public bool IsCardBack = false;
    public int CardOfPlayer;
    public bool isInUse;
    public GameObject zone;
    public bool canBeDrag;
    public GameObject turn;
    void Awake()
    {
        turn = GameObject.Find("Main Camera");
        players = turn.GetComponent<GameControl>().players;
        dice = GameObject.Find("Dice");
        diceroll = dice.GetComponent<Dice>();
    }
    void Start()
    {
        //###18/6
        
/*        CardsInHandStatic = CardsInHand;
*/        
        ///////
        string jsonFilePath = Path.Combine(Application.streamingAssetsPath, "cardData.json");
        if (File.Exists(jsonFilePath))
        {
            string jsonContent = File.ReadAllText(jsonFilePath);
            CardManager.MyList cardList = JsonUtility.FromJson<CardManager.MyList>(jsonContent);
            List<CardData> listCardItem = cardList.cardList.FindAll(card => card.cardId == cardIdData);

            if (listCardItem != null)
            {
                listCard = listCardItem;
            }
        }
    }
    void Update()
    {
        cardIdGame = listCard[0].cardId;
        nameText.text = listCard[0].cardName;
        descriptionText.text = listCard[0].cardDescription;
        thisSprite = Resources.Load<Sprite>(listCard[0].img);
        cardName = listCard[0].cardName;
        cardDescription = listCard[0].cardDescription;
        cardPower = listCard[0].cardPower;
        img.sprite = thisSprite;
        if (this.tag == "Item2")
        {
            //##18/6
            //CardsInHand[CardsInHandNumber] = Deck.staticDeck[Deck.deckSize - 1];
            //CardsInHandNumber++;
            //Debug.Log("CardsInHandNumber=" + CardsInHandNumber);
            //
            Debug.Log(Deck.deckSize - 1);
            listCard[0] = Deck.staticDeck[Deck.deckSize - 1];
            Deck.deckSize -= 1;
            enemyItems = GameObject.FindGameObjectsWithTag("Item2");
            for (int j = 0; j < enemyItems.Length; j++)
            {
                enemyItems[j].name = listCard[0].cardName;
            }
            this.tag = "Untagged";
        }
         //####18/6
        /*for(int i=0;i<90;i++)
        {
            if (CardsInHand[i].cardId!=0)
            {
                CardsInHandStatic[i] = CardsInHand[i];
                //Debug.Log("i of itemEnemycard=" + i);
            }
        }*/
         //////
        if (IsCardBack)
        {
            cardBack.SetActive(true);
        }
        else
        {
            cardBack.SetActive(false);

        }
    }
}
