using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EjerciciosControl : MonoBehaviour
{
    public TMP_InputField [] NombresEjer = new TMP_InputField [7];
    public TMP_InputField [] FechasIniEjer = new TMP_InputField [7];
    public TMP_InputField [] FechasFiniEjer = new TMP_InputField [7];
    public Button [] Activador = new Button [7];

    public GameObject [] Botones = new GameObject [7];

    public Color wantedColor;
    public Color desireColor;

    private bool comparador;

    public string espacio;

    public int dato;

    void Start(){
        Activador[0].GetComponent<Button>().onClick.AddListener(()=>Activar(0));
        Activador[1].GetComponent<Button>().onClick.AddListener(()=>Activar(1));
        Activador[2].GetComponent<Button>().onClick.AddListener(()=>Activar(2));
        Activador[3].GetComponent<Button>().onClick.AddListener(()=>Activar(3));
        Activador[4].GetComponent<Button>().onClick.AddListener(()=>Activar(4));
        Activador[5].GetComponent<Button>().onClick.AddListener(()=>Activar(5));
        Activador[6].GetComponent<Button>().onClick.AddListener(()=>Activar(6));

        comparador = false;
        espacio = "Sin Activar";
        dato = 0;
        
        
    }

    public void Activar(int val){

        comparador ^= true;
        cambiarColor(val);

    }

    void cambiarColor(int val){
        if(comparador == true){
            ColorBlock cb = Activador[val].colors;
            cb.normalColor = wantedColor;
            cb.selectedColor = wantedColor;
            cb.pressedColor = wantedColor;
            cb.highlightedColor = wantedColor;
            Activador[val].colors = cb; 
            espacio = "Activo";
            dato = val;
            Debug.Log("El boton " + val + " se encuentra en estado " + espacio + " y el valor de dato es " + dato);
            FechasIniEjer[val].interactable = true;
            FechasFiniEjer[val].interactable = true;
            DesaparecerBotones(val);
            
        }
        else{
            ColorBlock cb = Activador[val].colors;
            cb.normalColor = desireColor;
            cb.selectedColor = desireColor;
            cb.pressedColor = desireColor;
            cb.highlightedColor = desireColor;
            Activador[val].colors = cb;
            espacio = "Sin Activar";
            dato = 0;
            Debug.Log("El boton " + val + " se encuentra en estado " + espacio + " y el valor de dato es " + dato);
            FechasIniEjer[val].interactable = false;
            FechasFiniEjer[val].interactable = false;
            AparecerBotones();


        }
        
        
    }

    public void DesaparecerBotones(int val){

        for (int i = 0; i < Botones.Length; i++)
        {
            if(i == val){
                Botones[i].SetActive(true);
            }
            else{
                Botones[i].SetActive(false);
            }
            
        }
    }

    public void AparecerBotones(){

        for (int i = 0; i < Botones.Length; i++)
        {
            
            Botones[i].SetActive(true);
            
        }

    }

    public void DesactivarAuto(int val){
        ColorBlock cb = Activador[val].colors;
            cb.normalColor = desireColor;
            cb.selectedColor = desireColor;
            cb.pressedColor = desireColor;
            cb.highlightedColor = desireColor;
            Activador[val].colors = cb;
            espacio = "Sin Activar";
            dato = 0;
            Debug.Log("El boton " + val + " se encuentra en estado " + espacio + " y el valor de dato es " + dato);
            FechasIniEjer[val].interactable = false;
            FechasFiniEjer[val].interactable = false;
            comparador = false;
            AparecerBotones();
    }
    
}
