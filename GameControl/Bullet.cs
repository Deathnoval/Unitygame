using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int count;
    public GameObject hitEffect;
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (count == 1)
        {
            GameObject hit = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(hit, 3f);
            Destroy(gameObject);
        }
        count++;
    }
}
