using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShuffle : MonoBehaviour
{
    public GameObject Deck;
    public GameObject It;
    void Update()
    {
        Deck = GameObject.Find("Deck Panel");
        It.transform.SetParent(Deck.transform);
        It.transform.localScale = new Vector3(1,1);
        It.transform.position = new Vector3(transform.position.x, transform.position.y);
        It.transform.eulerAngles = new Vector3(0,0,0);
    }
}
