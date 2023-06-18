using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUseCards : MonoBehaviour
{
    public List<CardData> CardsInHand= new List<CardData>();
    public GameObject Deck;
    public GameObject turnbase;


    public Deck getCardsInHand;
    // Start is called before the first frame update
    void Start()
    {
        Deck = GameObject.Find("Deck Panel");
        getCardsInHand=Deck.GetComponent<Deck>();
        CardsInHand = getCardsInHand.CardsInHand;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
