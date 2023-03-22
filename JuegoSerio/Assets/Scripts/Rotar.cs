using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotar : MonoBehaviour
{
    public int speed;

    void Update()
    {
        transform.RotateAround(transform.position,Vector3.down, speed * Time.deltaTime);
        //transform.Rotate(new Vector3(1f, 0f, 0f) * speed * Time.deltaTime);
    }
}
