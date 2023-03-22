using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreacionUsuarios : MonoBehaviour
{
    /*[SerializeField]
    private Button Guardar;*/

    [SerializeField]
    private TMP_InputField usuario;

    [SerializeField]
    private TMP_InputField nombreCompleto;

    [SerializeField]
    private TMP_InputField Tarjeta;

    [HideInInspector]
    public string textUsuario;

    [HideInInspector]
    public string textNombre;

    [HideInInspector]
    public string TI;

    [HideInInspector]
    public int newUser;
    [HideInInspector]
    public int numero;

    public void presionarBoton(){
        ExtraerDatos();

    }
    public void ExtraerDatos(){

        textUsuario = usuario.text;
        textNombre = nombreCompleto.text;
        TI = Tarjeta.text;
        //numero = 1;
        //newUser ++;

    }

    public void Limpiar(){
        usuario.text = "";
        nombreCompleto.text = "";
        Tarjeta.text = "";
    }



}
