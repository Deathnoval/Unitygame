using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public float speed = 10;
    Camera cam;
    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        float hori = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
       

        if (scroll < 0)
        {
            cam.orthographicSize -= scroll * 10;
        }
        else if (scroll > 0 && cam.orthographicSize > 2)
        {
            cam.orthographicSize -= scroll * 10;
        }
        if (hori != 0 || vert != 0)
        {
            pos.x += hori * Time.deltaTime * speed;
            pos.y += vert * Time.deltaTime * speed;
        }
        transform.position = pos;
    }
}
