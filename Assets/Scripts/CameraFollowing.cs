using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [HideInInspector] public float posY;
    public float speed;
    void Start()
    {
        posY = transform.position.y;
    }


    void Update()
    {
        if (transform.position.y > posY)
            return;

        float newPos = transform.position.y + Time.deltaTime * speed;
        transform.position = new Vector3(transform.position.x, newPos, transform.position.z);
    }
}
