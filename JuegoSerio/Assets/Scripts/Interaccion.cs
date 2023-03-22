using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Codigo general de interaccion con objetos 
public class Interaccion : MonoBehaviour
{
    //Declaracion de objeto a interactuar y del sonido que se le asignara al boton 
    [SerializeField] private GameObject item;
    [SerializeField] private GameObject sonidoSelect;
    public GameObject sonidoBoton;
    //inicialización de la variable que va a contener el tag del objeto
    string tipoObjeto = "Nada";


    void Start()
    {
        //Asignacion del tag del objeto a la variable "tipoObjeto" 
        tipoObjeto = item.tag;
    }
    //Si se presiona el click derecho del mouse o se pulsa en la pantalla se ejecutara esta funcion 
    private void OnMouseDown()
    {

        //Desactivar la gravedad del objeto y activa  la objcion "iskinematic" 
        item.GetComponent<Rigidbody>().useGravity = false;
        item.GetComponent<Rigidbody>().isKinematic = true;


        //Switch case que aumenta el contador de los obejetos dependiendo del tag que este en la variable "tipoObjeto"
        switch (tipoObjeto)
        {
            case "Banana":
                Timer.Banana = Timer.Banana + 1;
                break;
            case "Manzana":
                Timer.Manzana = Timer.Manzana + 1;
                break;
            case "Pera":
                Timer.Pera = Timer.Pera + 1;
                break;
            case "Uvas":
                Timer.Uvas = Timer.Uvas + 1;
                break;
            case "Naranja":
                Timer.Naranja = Timer.Naranja + 1;
                break;
            case "Agua":
                Timer.Agua = Timer.Agua + 1;
                break;
            case "Cereal":
                Timer.Cereal = Timer.Cereal + 1;
                break;
            case "Crema":
                Timer.Crema = Timer.Crema + 1;
                break;
            case "Vinagre":
                Timer.Vinagre = Timer.Vinagre + 1;
                break;
            case "SalsaTomate":
                Timer.SalsaTomate = Timer.SalsaTomate + 1;
                break;
            case "SalsaRosada":
                Timer.SalsaRosada = Timer.SalsaRosada + 1;
                break;
            case "Pocky":
                Timer.Pocky = Timer.Pocky + 1;
                break;
            case "Rosquillas":
                Timer.Rosquillas = Timer.Rosquillas + 1;
                break;
            case "Leche":
                Timer.Leche = Timer.Leche + 1;
                break;
            case "JuNaranja":
                Timer.JuNaranja = Timer.JuNaranja + 1;
                break;
            case "Soda":
                Timer.Soda = Timer.Soda + 1;
                break;
            case "BananaVari":
                Timer.BananaVari += 1;
                break;
            case "ManzanaVari":
                Timer.ManzanaVari += 1;
                break;
            case "PeraVari":
                Timer.PeraVari += 1;
                break;
            case "UvasVari":
                Timer.UvasVari += 1;
                break;
            case "NaranjaVari":
                Timer.NaranjaVari += 1;
                break;
            case "AguaVari":
                Timer.AguaVari += 1;
                break;
            case "CerealVari":
                Timer.CerealVari += 1;
                break;
            case "CremaVari":
                Timer.CremaVari += 1;
                break;
            case "VinagreVari":
                Timer.VinagreVari += 1;
                break;
            case "SalsaTomateVari":
                Timer.SalsaTomateVari += 1;
                break;
            case "SalsaRosadaVari":
                Timer.SalsaRosadaVari += 1;
                break;
            case "PockyVari":
                Timer.PockyVari += 1;
                break;
            case "RosquillasVari":
                Timer.RosquillasVari += 1;
                break;
            case "LecheVari":
                Timer.LecheVari += 1;
                break;
            case "JuNaranjaVari":
                Timer.JuNaranjaVari += 1;
                break;
            case "SodaVari":
                Timer.SodaVari += 1;
                break;
            case "Monster":
                Timer.Monster += 1;
                break;


        }
        //Destruir el objeto 
        for (int i = 0; i < SeleccionDeOjeto.mNombres.Length; i++)
        {
            //Compara el tag del objeto con los nombres de los objetos de la mision para saber si selecciono el objeto correcto 
            if ((tipoObjeto == SeleccionDeOjeto.mNombres[i] || tipoObjeto == "Monster"))
            {
                //Llama a la fucnion de condicion de victoria y aumenta el numero de acieros 
                SeleccionDeOjeto.condicionVictoria();
                Timer.acierto += 1; 

                //Llama al sonido de interacion con el objeto y los destruye para que no siga sumando aciertos con ese objeto
                Instantiate(sonidoSelect);
                Destroy(item.gameObject);
                //Como es un acierto, le resta la cantidad de veces que va el ciclo for a los errores para evitar falsos conteos,
                // ademas de romper el ciclo ya que el objeto se encontro dentro de la lista de busqueda
                Timer.conteoErrores = Timer.conteoErrores - i;
                break;
            }
            //Condicion cuando el objeto no es el correcto 
            else if (tipoObjeto !=SeleccionDeOjeto.mNombres[i] && tipoObjeto!= "Monster")
            {
                //Aumenta en 1 el conteo de errores e instancia el objeto de sonido para la interaccion con el objeto
                Timer.conteoErrores += 1;
                Instantiate(sonidoBoton);
                
            }

        }

        //Guarda el conteo de errores en la variable fallas del script de SeleccionDeOjeto
        //y se la divide entre el numero de objetos de la mision, para descontar los conteos extra del ciclo for 
        SeleccionDeOjeto.fallas = Timer.conteoErrores / SeleccionDeOjeto.mNombres.Length;

    }
    //Se ejecuta esta fucnion cuando se deja de pulsar el click derecho o deja de pulsar la pantalla 
    private void OnMouseUp()
    {
        item.GetComponent<Rigidbody>().isKinematic = false;

    }
    
}

