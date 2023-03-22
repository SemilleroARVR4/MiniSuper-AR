using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumeroEjercicio : MonoBehaviour
{
    //Variables para objeto y nombre de ejercicios 
    [SerializeField] private Button boton;
    public string ejercicio;

    public void ButtonClick()
    {
        // Nonmbra el boton con el mismo nombre que el ejercicio asignado 
        SeleccionDeOjeto.Ejercicio = boton.transform.GetChild(0).GetComponent<Text>().text;
        StartCoroutine(dealy());
        Debug.Log("el ejercicio es : " + SeleccionDeOjeto.Ejercicio);
    }
    // Delay de 2 segundos
    IEnumerator dealy()
    {
        yield return new WaitForSeconds(2);
    }
}
