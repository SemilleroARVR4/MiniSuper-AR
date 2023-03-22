// Este codigo genera la animacion de mover el panel del menu desplegable del progreso en el juego 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menudesplegable : MonoBehaviour
{

    //Varaibles de posicion y tiempo para las interacciones del menu desplegable 
    public RectTransform Menu;
    public float tiempo = 0.5f;

    float posFinal;
    bool abrirMenu = true;


    private void Start()
    {
        //Posicion final donde esta el menu 
        posFinal = Screen.width / 4;
        Menu.position = new Vector3(-posFinal, Menu.position.y, 0);
    }
    //Funcion para mover la ventana del menu
    void MoverMenu(float time, Vector3 posIni, Vector3 posFin)
    {
        StartCoroutine( Mover(time, posIni, posFin));
    }
    //Corrutina para mover la ventana del menu 
    IEnumerator Mover(float time, Vector3 posIni, Vector3 posFin)
    {
        float elapsedTime = 0;
        //Movimiento de la posicion del menu y el tiempo en que tarda en hacer el movimiento 
        while (elapsedTime < time)
        {
            Menu.position = Vector3.Lerp(posIni, posFin, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Menu.position = posFin;
    }
    //Funcion para activar el movimiento del menu 
    public void BOTTON_Menu() 
    {
        //Condicional para activar el movimiendo del menu
        int signo = 1;
        if (!abrirMenu)
        {
            signo = -1;
        }
        //Llamado a la funcion de movimiento con los parametros del tiempo en el que tarda en hacer el movimiento 
        //y un vector con la posicion incial y final del menu desplegable
        MoverMenu(tiempo, Menu.position, new Vector3(signo * posFinal, Menu.position.y, 0));
        //Variale boleana para hacer la interaccion de movimiento 
        abrirMenu = !abrirMenu;

    }


  }
