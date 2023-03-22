using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Cargador 
{

    public static string nextlevel;

    public static void cargarNivel(string nombre)
    {
        nextlevel = nombre;

        SceneManager.LoadScene("Escena de carga");

    }
  
}
