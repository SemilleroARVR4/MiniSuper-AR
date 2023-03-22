using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlScroll : MonoBehaviour
{
    public static ControlScroll instance;
    [SerializeField] int numeroUsuarios;

    [SerializeField] int pivote;

    [SerializeField] FirebaseManager firebaseManager;
    [SerializeField] GameObject botonUsuarioPrefab;

    [SerializeField] Transform botonParent;

    [SerializeField] CreacionUsuarios creacionUsuarios;

    [HideInInspector]public string [] huish;
    [HideInInspector]public string [] huish2;
    [HideInInspector]public string [] huish3;

    public TMP_InputField usUsuario;

    public TMP_InputField docUsuario;

    public TMP_InputField nomUsuario;

    [HideInInspector]public int numero;

    public TMP_InputField usersField;



    void Start(){
        firebaseManager.cargar();
        Debug.Log("CARGA LOS DATOS");
        numero = int.Parse(firebaseManager.valor);
        huish = new string [numero];
        huish2 = new string [numero];
        huish3 = new string [numero];
        huish = FirebaseManager.NombresUsuarios;
        huish2 = FirebaseManager.DocumentosUsuarios;
        huish3 = FirebaseManager.UsUsuarios;
        

     }

     public void refresh(){

        firebaseManager.cargar();
        
        numero = int.Parse(firebaseManager.valor);
        huish = new string [numero];
        huish2 = new string [numero];
        huish3 = new string [numero];
        huish = FirebaseManager.NombresUsuarios;
        huish2 = FirebaseManager.DocumentosUsuarios;
        huish3 = FirebaseManager.UsUsuarios;

     }


    public void LoadBotones(int a){
        for (int i = 0; i < a; i++)
        {
            GameObject botonObj = Instantiate(botonUsuarioPrefab,botonParent) as GameObject;
            botonObj.GetComponent<BotonUsuario>().usuarioCode = i;
            botonObj.GetComponent<BotonUsuario>().controlScroll = this;
            usersField.SetTextWithoutNotify(a.ToString());
        }
        //usersField.SetTextWithoutNotify(a.ToString());
        
    }


    public void BotonClick(string name, int a)
    {

        nomUsuario.text = name;
        docUsuario.text = huish2[a];
        usUsuario.text = huish3[a];

    }



}
