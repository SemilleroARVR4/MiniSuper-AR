using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ControlRetos : MonoBehaviour
{
    [Header("Fake inputs")]

    public TMP_InputField [] inputObjeto = new TMP_InputField [10];
    [HideInInspector]
    public int [] inputCantidad = new int [10];

    public TMP_InputField espacioObjeto;
    public TMP_InputField espacioCantidad;


    [Header("Boton unico")]

    public Button Guardar;

    [Header("Botones objetos")]
    public Button [] BotonObjetos = new Button [30];
    [Header("Botones cantidad")]
    public Button [] BotonCantidad = new Button [12];

    [HideInInspector]
    public int posicion;
    private int Cantidad;
    private string cosa;

    // Start is called before the first frame update
    void Start()
    {
        /* "Manzana","Banana","Cereal","CocaCola", "Leche","Crema","JuManzana","Pera",
        "Pocky","Naranja","Rosquillas","SalsaRosada","Uvas","SalsaTomate","Agua"
            i++
            Debug.Log("Estante"+i)

            Estante1

        */
        /*/
        posicion = 0;
        Cantidad = 0;
        cosa = "";/*/

        BotonObjetos[0].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("Manzana");});
        BotonObjetos[1].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("Banana");});
        BotonObjetos[2].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("Cereal");});
        BotonObjetos[3].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("CocaCola");});
        BotonObjetos[4].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("Leche");});
        BotonObjetos[5].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("Crema");});
        BotonObjetos[6].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("JuNaranja");});
        BotonObjetos[7].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("Pera");});
        BotonObjetos[8].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("Pocky");});
        BotonObjetos[9].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("Naranja");});
        BotonObjetos[10].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("Rosquillas");});
        BotonObjetos[11].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("SalsaRosada");});
        BotonObjetos[12].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("Uvas");});
        BotonObjetos[13].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("SalsaTomate");});
        BotonObjetos[14].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("Agua");});
        BotonObjetos[15].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("ManzanaVari");});
        BotonObjetos[16].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("BananaVari");});
        BotonObjetos[17].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("CerealVari");});
        BotonObjetos[18].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("CocaColaVari");});
        BotonObjetos[19].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("LecheVari");});
        BotonObjetos[20].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("CremaVari");});
        BotonObjetos[21].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("JuNaranjaVari");});
        BotonObjetos[22].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("PeraVari");});
        BotonObjetos[23].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("PockyVari");});
        BotonObjetos[24].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("NaranjaVari");});
        BotonObjetos[25].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("RosquillasVari");});
        BotonObjetos[26].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("SalsaRosadaVari");});
        BotonObjetos[27].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("UvasVari");});
        BotonObjetos[28].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("SalsaTomateVari");});
        BotonObjetos[29].onClick.AddListener(()=> {espacioObjeto.SetTextWithoutNotify("AguaVari");});

        BotonCantidad[0].onClick.AddListener(()=>{espacioCantidad.SetTextWithoutNotify("1");});
        BotonCantidad[1].onClick.AddListener(()=>{espacioCantidad.SetTextWithoutNotify("2");});
        BotonCantidad[2].onClick.AddListener(()=>{espacioCantidad.SetTextWithoutNotify("3");});
        BotonCantidad[3].onClick.AddListener(()=>{espacioCantidad.SetTextWithoutNotify("4");});
        BotonCantidad[4].onClick.AddListener(()=>{espacioCantidad.SetTextWithoutNotify("5");});
        BotonCantidad[5].onClick.AddListener(()=>{espacioCantidad.SetTextWithoutNotify("6");});
        BotonCantidad[6].onClick.AddListener(()=>{espacioCantidad.SetTextWithoutNotify("7");});
        BotonCantidad[7].onClick.AddListener(()=>{espacioCantidad.SetTextWithoutNotify("8");});
        BotonCantidad[8].onClick.AddListener(()=>{espacioCantidad.SetTextWithoutNotify("9");});
        BotonCantidad[9].onClick.AddListener(()=>{espacioCantidad.SetTextWithoutNotify("10");});
        BotonCantidad[10].onClick.AddListener(()=>{espacioCantidad.SetTextWithoutNotify("11");});
        BotonCantidad[11].onClick.AddListener(()=>{espacioCantidad.SetTextWithoutNotify("12");});

        //Guardar.onClick.AddListener(()=>EscribirEnElVector(posicion,cosa,Cantidad));
        
    }

    public void LimpiarEspacioReto(){
        espacioCantidad.text = "";
        espacioObjeto.text = "";
    }
    /*/
    void x){
        cosa = x;
        inputObjeto[posicion].SetTextWithoutNotify(x);
    }

    void  y){
        Cantidad = y;
        inputCantidad[posicion] = y;
   
    }

    void EscribirEnElVector(int lugar, string objeto, int cuanto){
        Debug.Log("El lugar es " + lugar + ". El bojeto es: " + objeto + " con una cantidad de: " + cuanto);
        posicion ++;
        
    }
    /*/


}
  