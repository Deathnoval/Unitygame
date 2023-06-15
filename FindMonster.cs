using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMonster : MonoBehaviour
{
    public GameObject Map;
    public GameObject It;

    // Update is called once per frame
    void Awake()
    {
        Map = GameObject.Find("Map");
        It.transform.SetParent(Map.transform);
        It.transform.localScale = new Vector3(1, 1);
        It.transform.position = new Vector3(transform.position.x, transform.position.y);
        It.transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
