using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ControlPosterior : MonoBehaviour
{
    private bool[] state = new bool [12];
    private int contador;
    private bool magic;
    private bool variante;
    public Button botonMagico;
    public Button botonVari;
    public Color wantedColor;
    public Color desireColor;

    private string[] nombresObjetos = {"Manzana","Banana","Cereal","Soda", "Leche","Crema","JuNaranja","Pera",
    "Pocky","Naranja","Rosquillas","SalsaRosada","Uvas","SalsaTomate","Agua","ManzanaVari","BananaVari","CerealVari","SodaVari", "LecheVari",
    "CremaVari","JuNaranjaVari","PeraVari", "PockyVari","NaranjaVari","RosquillasVari","SalsaRosadaVari","UvasVari","SalsaTomateVari",
    "AguaVari","Sin objeto"};

    public TMP_InputField[] entradasP = new TMP_InputField [12];

    public GameObject contenedor1;
    public GameObject contenedor2;
    private Image equis;
    public Sprite[] imagenes = new Sprite[31];

    public Button[] botones = new Button[12];
    public Button[] objetos = new Button[30];

   
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
        botones[8].GetComponent<Button>().onClick.AddListener(()=>TaskOnClick(8));
        botones[9].GetComponent<Button>().onClick.AddListener(()=>TaskOnClick(9));
        botones[10].GetComponent<Button>().onClick.AddListener(()=>TaskOnClick(10));
        botones[11].GetComponent<Button>().onClick.AddListener(()=>TaskOnClick(11));
        botonMagico.GetComponent<Button>().onClick.AddListener(ActivarMagic);
        botonVari.GetComponent<Button>().onClick.AddListener(ActivarVariante);

        state[0] = false;
        state[1] = false;
        state[2] = false;
        state[3] = false;
        state[4] = false;
        state[5] = false;
        state[6] = false;
        state[7] = false;
        state[8] = false;
        state[9] = false;
        state[10] = false;
        state[11] = false;


        magic = false;
        variante = false;

        contenedor1.SetActive(false);
        contenedor2.SetActive(false);

        botonVari.interactable = false;

        PresionarObjeto();
        
    }

    private void TaskOnClick(int x){
        state[x] = true;
        botonVari.interactable = true;
        if (variante == true)
        {
            contenedor1.SetActive(false);
            contenedor2.SetActive(true);

        }
        else{
            contenedor1.SetActive(true);
            contenedor2.SetActive(false);
        }
        
        contador = contador + 1;
        Debug.Log("Presiono el boton" + x);

    }

    void ActivarMagic(){
        magic ^= true;
        cambiarColor();

        Debug.Log("El boton esta en modo"+ magic);
    }

    void ActivarVariante(){
        variante ^= true;
        if (variante == true){
            contenedor2.SetActive(true);
            contenedor1.SetActive(false);
        }
        else{
            contenedor2.SetActive(false);
            contenedor1.SetActive(true);
        }
       
        cambiarColorVari();
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

    void cambiarColorVari(){
        if(variante == true){
            ColorBlock cb = botonVari.colors;
            cb.normalColor = wantedColor;
            cb.selectedColor = wantedColor;
            cb.pressedColor = wantedColor;
            cb.highlightedColor = wantedColor;
            botonVari.colors = cb; 
        }
        else{
            ColorBlock cb = botonVari.colors;
            cb.normalColor = desireColor;
            cb.selectedColor = desireColor;
            cb.pressedColor = desireColor;
            cb.highlightedColor = desireColor;
            botonVari.colors = cb; 

        }
        
        
    }

    private void PresionarObjeto(){
        
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
        objetos[15].onClick.AddListener(()=>SeleccionarObjeto(15));
        objetos[16].onClick.AddListener(()=>SeleccionarObjeto(16));
        objetos[17].onClick.AddListener(()=>SeleccionarObjeto(17));
        objetos[18].onClick.AddListener(()=>SeleccionarObjeto(18));
        objetos[19].onClick.AddListener(()=>SeleccionarObjeto(19));
        objetos[20].onClick.AddListener(()=>SeleccionarObjeto(20));
        objetos[21].onClick.AddListener(()=>SeleccionarObjeto(21));
        objetos[22].onClick.AddListener(()=>SeleccionarObjeto(22));
        objetos[23].onClick.AddListener(()=>SeleccionarObjeto(23));
        objetos[24].onClick.AddListener(()=>SeleccionarObjeto(24));
        objetos[25].onClick.AddListener(()=>SeleccionarObjeto(25));
        objetos[26].onClick.AddListener(()=>SeleccionarObjeto(26));
        objetos[27].onClick.AddListener(()=>SeleccionarObjeto(27));
        objetos[28].onClick.AddListener(()=>SeleccionarObjeto(28));
        objetos[29].onClick.AddListener(()=>SeleccionarObjeto(29));


    }

    private void SeleccionarObjeto(int m){ //Numero del 0 al 14
        for (int d = 0; d < botones.Length; d++)
        {
            if(state[d] == true){
                equis = botones[d].GetComponent<Button>().image;
                equis.sprite = imagenes[m];
                contenedor1.SetActive(false);
                contenedor2.SetActive(false);
                botonVari.interactable = false;
                state[d] = false;
                contador = contador - 1;
                Debug.Log("Contenedor apagado state: " + d);
                PonerEnInput(m,d);
            }
        }
    }

    void PonerEnInput(int a, int b){
        int objeto = a;
        int ubicacion = b;

        switch (ubicacion)
        {
            case 0:
            entradasP[0].SetTextWithoutNotify(textoObjeto(objeto));
            break;

            case 1:
            entradasP[1].SetTextWithoutNotify(textoObjeto(objeto));
            break;

            case 2:
            entradasP[2].SetTextWithoutNotify(textoObjeto(objeto));
            break;

            case 3:
            entradasP[3].SetTextWithoutNotify(textoObjeto(objeto));
            break;

            case 4:
            entradasP[4].SetTextWithoutNotify(textoObjeto(objeto));
            break;

            case 5:
            entradasP[5].SetTextWithoutNotify(textoObjeto(objeto));
            break;

            case 6:
            entradasP[6].SetTextWithoutNotify(textoObjeto(objeto));
            break;

            case 7:
            entradasP[7].SetTextWithoutNotify(textoObjeto(objeto));
            break;

            case 8:
            entradasP[8].SetTextWithoutNotify(textoObjeto(objeto));
            break;

            case 9:
            entradasP[9].SetTextWithoutNotify(textoObjeto(objeto));
            break;

            case 10:
            entradasP[10].SetTextWithoutNotify(textoObjeto(objeto));
            break;

            case 11:
            entradasP[11].SetTextWithoutNotify(textoObjeto(objeto));
            break;

                    
            default:
            break;
        }

    }

    private string textoObjeto(int obj){

        string nombre = nombresObjetos[obj]; 
        return nombre;
    }

    void PresionarVariosBotones(){
        if(contador >= 2 && magic==false){
            contenedor1.SetActive(false);
            contenedor2.SetActive(false);
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

    public void CargarImagenBoton(){

        for (int i = 0; i < entradasP.Length; i++)
        {
            int loop = 0;
            foreach (string item in nombresObjetos)
            {
                if(entradasP[i].text == item){
                    equis = botones[i].GetComponent<Button>().image;
                    equis.sprite = imagenes[loop];
                }
                else{
                    loop ++;
                }
            }
        }
    }

    void FixedUpdate(){
       PresionarVariosBotones();
       
    }
}
