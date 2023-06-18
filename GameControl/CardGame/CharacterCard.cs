using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCard : MonoBehaviour
{
    List<Player> players = new List<Player>();
    public List<CardData> listCard = new List<CardData>();
    public GameObject[] playerCard;
    private int cardIdData;
    public int cardIdPlayer;
    public GameObject cardBack;
    private string cardName;
    private string cardDescription;
    public Text nameText;
    public Text descriptionText;
    public Sprite thisSprite;
    public Image img;
    public bool IsCardBack = false;
    public int CardOfPlayer;
    public bool isInUse ;
    public GameObject canBeUse;
    void Awake()
    {
        cardIdData = Random.Range(1, 7);
    }
    void Start()
    {
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
        cardIdPlayer = listCard[0].cardId;
        nameText.text = listCard[0].cardName;
        descriptionText.text = listCard[0].cardDescription;
        thisSprite = Resources.Load<Sprite>(listCard[0].img);
        cardName = listCard[0].cardName;
        cardDescription = listCard[0].cardDescription;
        img.sprite = thisSprite;

        if (this.tag == "Player")
        {
           // CardOfPlayer = 1;
            playerCard = GameObject.FindGameObjectsWithTag("Player");
            playerCard[0].name = listCard[0].cardName;
            IsCardBack = false;
            
        }
        if (isInUse == true)
        {
            canBeUse.SetActive(true);
        }
        else
        {
            canBeUse.SetActive(false);
        }    
    }
  
}
