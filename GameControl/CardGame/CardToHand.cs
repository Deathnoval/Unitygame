using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardToHand : MonoBehaviour
{
    public GameObject Hand;
    public GameObject It;

    // Update is called once per frame
    void Start()
    {
        Hand = GameObject.Find("Hand Player 1");
        It.transform.SetParent(Hand.transform);
        It.transform.localScale = new Vector3(1,1);
        It.transform.position = new Vector3(transform.position.x, transform.position.y);
        It.transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
