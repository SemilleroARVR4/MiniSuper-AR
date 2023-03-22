using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GifMove : MonoBehaviour
{
    public Texture2D [] frames;

    public int fps = 10;

    void Update(){

        float index = (Time.time * fps) % frames.Length;
        GetComponent<RawImage>().texture = frames[(int)index];

    }

}
