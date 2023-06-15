using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICardToHand : MonoBehaviour
{
    public GameObject Hand;
    public GameObject It;

    void Start()
    {
        Hand = GameObject.Find("Hand Player 2");
        It.transform.SetParent(Hand.transform);
        It.transform.localScale = new Vector3(1, 1);
        It.transform.position = new Vector3(transform.position.x, transform.position.y);
        It.transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
