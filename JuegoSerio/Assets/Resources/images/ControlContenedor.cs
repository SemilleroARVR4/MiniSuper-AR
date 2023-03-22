using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ControlContenedor : MonoBehaviour
{
    private bool[] state = new bool [8];
    private int contador;
    private bool magic;
    public Button botonMagico;
    public Color wantedColor;
    public Color desireColor;

    private string[] nombresObjetos = {"Manzana","Banana","Cereal","CocaCola", "Leche","Crema","JuManzana","Pera",
    "Pocky","Naranja","Rosquillas","SalsaRosada","Uvas","SalsaTomate","Agua"};

    public TMP_InputField[] entradas = new TMP_InputField [8];

    public GameObject contenedor1;
    private Image equis;
    public Sprite[] imagenes = new Sprite[15];

    public Button[] botones = new Button[8];
    public Button[] objetos = new Button[15];
    
    // Start is called before the first frame update
    void Start()
    {
        contador = 0;
        botones[0].GetComponent<Button>().onClick.AddListener(()=>TaskOnClick(0));
        botones[1].GetComponent<Button>().onClick.AddListener(()=>TaskOnClick(1));
        botones[2].GetComponent<Button>().onClick.AddListener(()=>TaskOnClick(2));
        botones[3].GetComponent<Button>().onClick.AddListener(()=>TaskOnClick(3));
        botones[4].GetComponent<Button>().onClick.AddListener(()=>TaskOnClick(4));
        botones[5].GetComponent<Button>().onClick.AddListener(()=>TaskOnClick(5));
        botones[6].GetComponent<Button>().onClick.AddListener(()=>TaskOnClick(6));
        botones[7].GetComponent<Button>().onClick.AddListener(()=>TaskOnClick(7));
        botonMagico.GetComponent<Button>().onClick.AddListener(ActivarMagic);

        
        /*for (int s = 0; s < botones.Length; s++)
        {
            botones[s].GetComponent<Button>().onClick.AddListener(()=>TaskOnClick(s));
        }*/
        /*
        for (int i = 0; i < state.Length; i++)
        {
            state[i] = false;
        }*/

        state[0] = false;
        state[1] = false;
        state[2] = false;
        state[3] = false;
        state[4] = false;
        state[5] = false;
        state[6] = false;
        state[7] = false;
        
        magic = false;

        contenedor1.SetActive(false);

        PresionarObjeto();
        
        
    }

    private void TaskOnClick(int x){
        state[x] = true;
        contenedor1.SetActive(true);
        contador = contador + 1;
        Debug.Log("Presiono el boton" + x);

    }

    private void SeleccionarObjeto(int m){ //Numero del 0 al 14
        for (int d = 0; d < botones.Length; d++)
        {
            if(state[d] == true){
                equis = botones[d].GetComponent<Button>().image;
                equis.sprite = imagenes[m];
                contenedor1.SetActive(false);
                state[d] = false;
                contador = contador - 1;
                Debug.Log("Contenedor apagado state: " + d);
                PonerEnInput(m,d);
            }
        }
    }
    
    void ActivarMagic(){
        magic ^= true;
        cambiarColor();

        Debug.Log("El boton esta en modo"+ magic);
    }

    void cambiarColor(){
        if(magic == true){
            ColorBlock cb = botonMagico.colors;
            cb.normalColor = wantedColor;
            cb.selectedColor = wantedColor;
            cb.pressedColor = wantedColor;
            cb.highlightedColor = wantedColor;
            botonMagico.colors = cb; 
        }
        else{
            ColorBlock cb = botonMagico.colors;
            cb.normalColor = desireColor;
            cb.selectedColor = desireColor;
            cb.pressedColor = desireColor;
            cb.highlightedColor = desireColor;
            botonMagico.colors = cb; 

        }
        
        
    }
    

    private void PresionarObjeto(){
        /*for (int o = 0; o < objetos.Length; o++)
        {
            objetos[o].onClick.AddListener(()=>SeleccionarObjeto(o));
        }*/
        objetos[0].onClick.AddListener(()=>SeleccionarObjeto(0));
        objetos[1].onClick.AddListener(()=>SeleccionarObjeto(1));
        objetos[2].onClick.AddListener(()=>SeleccionarObjeto(2));
        objetos[3].onClick.AddListener(()=>SeleccionarObjeto(3));
        objetos[4].onClick.AddListener(()=>SeleccionarObjeto(4));
        objetos[5].onClick.AddListener(()=>SeleccionarObjeto(5));
        objetos[6].onClick.AddListener(()=>SeleccionarObjeto(6));
        objetos[7].onClick.AddListener(()=>SeleccionarObjeto(7));
        objetos[8].onClick.AddListener(()=>SeleccionarObjeto(8));
        objetos[9].onClick.AddListener(()=>SeleccionarObjeto(9));
        objetos[10].onClick.AddListener(()=>SeleccionarObjeto(10));
        objetos[11].onClick.AddListener(()=>SeleccionarObjeto(11));
        objetos[12].onClick.AddListener(()=>SeleccionarObjeto(12));
        objetos[13].onClick.AddListener(()=>SeleccionarObjeto(13));
        objetos[14].onClick.AddListener(()=>SeleccionarObjeto(14));


    }

    void PresionarVariosBotones(){
        if(contador >= 2 && magic==false){
            contenedor1.SetActive(false);
            state[0] = false;
            state[1] = false;
            state[2] = false;
            state[3] = false;
            state[4] = false;
            state[5] = false;
            state[6] = false;
            state[7] = false;
            Debug.Log("Se presionaron dos botones a la vez");
            contador = 0;
        }
    }

    void PonerEnInput(int a, int b){
        int objeto = a;
        int ubicacion = b;

        switch (ubicacion)
        {
            case 0:
            entradas[0].SetTextWithoutNotify(textoObjeto(objeto));
            break;

            case 1:
            entradas[1].SetTextWithoutNotify(textoObjeto(objeto));
            break;

            case 2:
            entradas[2].SetTextWithoutNotify(textoObjeto(objeto));
            break;

            case 3:
            entradas[3].SetTextWithoutNotify(textoObjeto(objeto));
            break;

            case 4:
            entradas[4].SetTextWithoutNotify(textoObjeto(objeto));
            break;

            case 5:
            entradas[5].SetTextWithoutNotify(textoObjeto(objeto));
            break;

            case 6:
            entradas[6].SetTextWithoutNotify(textoObjeto(objeto));
            break;

            case 7:
            entradas[7].SetTextWithoutNotify(textoObjeto(objeto));
            break;
                    
            default:
            break;
        }

    }

    private string textoObjeto(int obj){

        string nombre = nombresObjetos[obj]; 
        return nombre;
    }

    void FixedUpdate(){
       PresionarVariosBotones();
       
    }


    
}
