using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CargaNivel : MonoBehaviour
{

    public Slider Barra;
    float Progreso = 0f;
    public void Start()
    {
        string nivelaCargar = Cargador.nextlevel;
        StartCoroutine(CargaAsync(nivelaCargar));
        Debug.Log(nivelaCargar);
    }

    IEnumerator CargaAsync(string Nivel)
    {
        yield return new WaitForSeconds(1);

        AsyncOperation Operacion = SceneManager.LoadSceneAsync(Nivel);

        while (Operacion.isDone == false)
        {

            Progreso = Mathf.Clamp01(Operacion.progress / .9f);
            Barra.value = Progreso;
            Debug.Log(Progreso);

            yield return null;
        }

        
    }

}
