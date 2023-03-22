using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminarSonido : MonoBehaviour
{
    //Tiempo que dura el sonido
    public float tiempoVida;

    void Start()
    {
        //Destruye el objeto que contienen el sonido para detenerlo
        Destroy(gameObject, tiempoVida);
    }


}
