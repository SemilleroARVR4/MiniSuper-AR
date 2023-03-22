using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagerEstante : MonoBehaviour
{

    public GameObject ScreenBotones;
    public GameObject ScreenGeneral;
    public GameObject ScreenBotonesInicio;
    public GameObject ScreenReto;

    [Header("Botones estantes")]
    public Button Frontal;
    public Button Posterior;
    public Button LateralAmarillo;
    public Button LateralRojo;
    public Button Back;

    public Button salirReto;

    public Button BackInicio;

    public Button Reto;

    [Header("Botones interfaz selectora")]

    public Button irBotonesEstante;

    [Header("Screens")]
    public GameObject SFrontal;
    public GameObject SPosterior;
    public GameObject SLR;
    public GameObject SLA;

    // Start is called before the first frame update
    void Start()
    {
        irBotonesEstante.GetComponent<Button>().onClick.AddListener(()=>{
            ScreenBotonesInicio.SetActive(false);ScreenBotones.SetActive(true); 
        });
        BackInicio.GetComponent<Button>().onClick.AddListener(()=>{
            ScreenBotonesInicio.SetActive(true);ScreenBotones.SetActive(false);
        });
        Reto.GetComponent<Button>().onClick.AddListener(()=>{
            ScreenBotonesInicio.SetActive(false);ScreenReto.SetActive(true);});
        salirReto.GetComponent<Button>().onClick.AddListener(()=>{
            ScreenReto.SetActive(false); ScreenBotonesInicio.SetActive(true);
        });
        controlScreenBotones();
    }

    
    public void controlScreenBotones(){
        Frontal.GetComponent<Button>().onClick.AddListener(()=>cambiarPantalla(0));
        Posterior.GetComponent<Button>().onClick.AddListener(()=>cambiarPantalla(1));
        LateralAmarillo.GetComponent<Button>().onClick.AddListener(()=>cambiarPantalla(2));
        LateralRojo.GetComponent<Button>().onClick.AddListener(()=>cambiarPantalla(3));
        Back.GetComponent<Button>().onClick.AddListener(()=>cambiarPantalla(4));
    }


    private void cambiarPantalla(int x){
        if(x == 0){
            SFrontal.SetActive(true);
            ScreenBotones.SetActive(false);
            ScreenGeneral.SetActive(true);
        }
        else if(x == 1){
            SPosterior.SetActive(true);
            ScreenBotones.SetActive(false);
            ScreenGeneral.SetActive(true);
        }
        else if(x == 2){
            SLA.SetActive(true);
            ScreenBotones.SetActive(false);
            ScreenGeneral.SetActive(true);
        }
        else if(x == 3){
            SLR.SetActive(true);
            ScreenBotones.SetActive(false);
            ScreenGeneral.SetActive(true);
        }
        else if(x == 4){
            SFrontal.SetActive(false);
            SPosterior.SetActive(false);
            SLA.SetActive(false);
            SLR.SetActive(false);
            ScreenGeneral.SetActive(false);
            ScreenBotones.SetActive(true);
        }
        else{
            Debug.Log("No es ningun boton valido");
        }
    }
}
