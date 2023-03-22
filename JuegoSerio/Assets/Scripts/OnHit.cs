using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Con este codigo se desactiva la deteccion de planos para que el estante no se mueva mas 
public class OnHit : MonoBehaviour
{
    //Variable para guardar el "Plane Finder" de vuforia 
    public GameObject plano;

    [SerializeField] private InputField usuario;
    //Variable para guadar el nombre de usuario
    public static string nicks;

    public void iniciales()
    {
        //Guarda el nombre de usuario ingresado en la variable "nicks"
        nicks = usuario.text;
        StartCoroutine(delay());
    }
    public void OnMouseDown()
    {
        //Desactivar Plane Finder
        plano.SetActive(false);

    }
    // Delay de 2 segundos
    IEnumerator delay()
    {
        yield return new WaitForSeconds(2);
    }
}

