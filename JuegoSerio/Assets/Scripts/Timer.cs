using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [Header("Imagenes")] 
    public Image imagen;
    [Header("Tiempo")]
    //Variables para el timepo
    public Text tiempoText;
    public float tiempo;
    public float tiempoReal;
    public static float currentTime;
    [Header("Objetos")]
    //Boton del menu desplegable
    [SerializeField] private GameObject boton;
    //Declaracion de los GameObjects
    [SerializeField] private GameObject E_Manzana;
    [SerializeField] private GameObject E_Banana;
    [SerializeField] private GameObject E_Pera;
    [SerializeField] private GameObject E_Uvas;
    [SerializeField] private GameObject E_Naranja;
    [SerializeField] private GameObject E_Pocky;
    [SerializeField] private GameObject E_Agua;
    [SerializeField] private GameObject E_SalsaTomate;
    [SerializeField] private GameObject E_SalsaRosada;
    [SerializeField] private GameObject E_Vinagre;
    [SerializeField] private GameObject E_Soda;
    [SerializeField] private GameObject E_Leche;
    [SerializeField] private GameObject E_JuManzana;
    [SerializeField] private GameObject E_Rosquillas;
    [SerializeField] private GameObject E_Crema;
    [SerializeField] private GameObject E_Cereal;
    [SerializeField] private GameObject E_Monster;

    //Declaracion de los GameObjects
    private GameObject T_Manzana;
    private GameObject T_Banana;
    private GameObject T_Pera;
    private GameObject T_Uvas;
    private GameObject T_Naranja;
    private GameObject T_Pocky;
    private GameObject T_Agua;
    private GameObject T_SalsaTomate;
    private GameObject T_SalsaRosada;
    private GameObject T_Vinagre;
    private GameObject T_Soda;
    private GameObject T_Leche;
    private GameObject T_JuManzana;
    private GameObject T_Rosquillas;
    private GameObject T_Crema;
    private GameObject T_Cereal;
    private GameObject T_Monster;

    //Gameobject para las variantes de los objetos
    [SerializeField] private GameObject Variantes;


    //Declacion de contadores para cada objeto 
    public static int Monster;
    public static int Banana;
    public static int Pera;
    public static int Uvas;
    public static int Manzana;
    public static int Naranja;
    public static int Pocky;
    public static int Agua;
    public static int SalsaTomate;
    public static int SalsaRosada;
    public static int Vinagre;
    public static int Soda;
    public static int Leche;
    public static int JuNaranja;
    public static int Rosquillas;
    public static int Crema;
    public static int Cereal;
    public static int BananaVari;
    public static int PeraVari;
    public static int UvasVari;
    public static int ManzanaVari;
    public static int NaranjaVari;
    public static int PockyVari;
    public static int AguaVari;
    public static int SalsaTomateVari;
    public static int SalsaRosadaVari;
    public static int VinagreVari;
    public static int SodaVari;
    public static int LecheVari;
    public static int JuNaranjaVari;
    public static int RosquillasVari;
    public static int CremaVari;
    public static int CerealVari;
    //Vector para guardar los valores de objetos 
    public static int[] conObjetos = new int[30];
    //Variable para contar las misiones que se vayan realizando 
    public static int conteo        = 0;
    //Varaibles para envio de errores y aciertos
    public static int conteoErrores = 0;
    public static int[] val;
    public static int acierto = 0;


    void Start()
    {
        //Se iguala la variable currentTime al valor de la valiable tiempo
        currentTime = tiempo;

    }
    

    void Update()
    {


        //Timer 
        currentTime -= Time.deltaTime;
        tiempoText.text = "" + currentTime.ToString("f0");

        //Condicion de derrota, si el tiempo proporcionado para hacer el ejercicio es igual o menor a 0
        if (currentTime <= 0)
        {
            //se actualiza la variable de aciertos para enviarla a firebase
            SeleccionDeOjeto.aciertos = acierto;
            //Cambia el valor de bandera para poder enviar datos a firebase
            SeleccionDeOjeto.bandera = true;
            //Se cambia el valor del texto de tiempoa 0
            tiempoText.text = "0";
            //Se actualiza la varaible para enviar el tiempo que se utilizo en el ejercicio a firebase
            SeleccionDeOjeto.TiempoUtilizado = (int.Parse(SeleccionDeOjeto.tiempoL) - int.Parse(tiempoText.text)).ToString();
            //delay de 2 segundos
            Saltos();
            //Cambia a la escena "Derrota"
            SceneManager.LoadScene("Derrota");
            //Reseteo de varaiables
            Banana = 0; Pera = 0; Uvas = 0; Manzana = 0; Naranja = 0; Pocky = 0; Agua = 0; SalsaTomate = 0; SalsaRosada = 0; Vinagre = 0;
            Soda = 0; Leche = 0; Monster = 0; JuNaranja = 0; Rosquillas = 0; Crema = 0; Cereal = 0; BananaVari = 0; PeraVari = 0; UvasVari = 0;
            ManzanaVari = 0; NaranjaVari = 0; PockyVari = 0; AguaVari = 0; SalsaTomateVari = 0; SalsaRosadaVari = 0; VinagreVari = 0; SodaVari = 0;
            LecheVari = 0; JuNaranjaVari = 0; RosquillasVari = 0; CremaVari = 0; CerealVari = 0;
        }


        // Condiciones de victoria, Cuando la variable conteo (Tiene el numeor de misisones completadas) sea mayor al numero de misiones del ejercicio
        // y la mision del monster este compelta (encuentre un monster, Monster = 1)
       if (conteo >= SeleccionDeOjeto.mNombres.Length && Monster >= 1)
        {
            //Cambia el valor de bandera para poder enviar datos a firebase
            SeleccionDeOjeto.bandera         = true;
            //Se actualiza los aciertos y el tiempo utilizado para enviarlos a firebase
            SeleccionDeOjeto.aciertos        = acierto;
            SeleccionDeOjeto.TiempoUtilizado = (int.Parse(SeleccionDeOjeto.tiempoL) - int.Parse(tiempoText.text)).ToString();
            //Cambio de escena a victoria 
            SceneManager.LoadScene("Victoria");
            //reseteo de variables
            Banana      = 0; Pera        = 0; Uvas        = 0; Manzana    = 0; Naranja  = 0; Pocky = 0;
            Agua        = 0; SalsaTomate = 0; SalsaRosada = 0; Vinagre    = 0; Soda = 0; conteo = 0;
            Leche       = 0; Monster     = 0; JuNaranja   = 0; Rosquillas = 0; Crema    = 0; Cereal = 0;
            BananaVari = 0; PeraVari = 0; UvasVari = 0; ManzanaVari = 0; NaranjaVari = 0; PockyVari = 0;
            AguaVari = 0; SalsaTomateVari = 0; SalsaRosadaVari = 0; VinagreVari = 0; SodaVari = 0;
            LecheVari = 0; JuNaranjaVari = 0; RosquillasVari = 0; CremaVari = 0; CerealVari = 0;

        }




    }
    //Resetea el timer y activa los objetos una ves se genere el estante en el plano 

    public void reset()
    {
        //Asigna a la variable currentTime el valor del tiempo asignado al ajercicio
        currentTime = int.Parse(SeleccionDeOjeto.tiempoL);
        //Activa la imagen del reloj y el tex donde se muestra el valor del tiempo
        imagen.gameObject.SetActive(true);
        tiempoText.gameObject.SetActive(true);


    }
    //Fucnion para instanciar los objetos en el estante en las posiciones correctas tomadas de firebase
    public void InstanciarObjetos()
    {
        
        //For para recorrer la parte frontal del estante (Con objetos normales)
        for (int i = 0; i < 12; i++)
        {
            //dependiendo del objeto guardado en el vector Estante1(el cual tiene los objetos en las pociones de la parte frontal del estante) se activara el objeto correcto
            switch (SeleccionDeOjeto.Estante1[i])
            {
                case "Banana":
                    T_Banana = E_Banana.transform.GetChild(i).gameObject;
                    T_Banana.SetActive(true);
                    break;
                case "Manzana":
                    T_Manzana = E_Manzana.transform.GetChild(i).gameObject;
                    T_Manzana.SetActive(true);
                    break;
                case "Pera":
                    T_Pera = E_Pera.transform.GetChild(i).gameObject;
                    T_Pera.SetActive(true);
                    break;
                case "Uvas":
                    T_Uvas = E_Uvas.transform.GetChild(i).gameObject;
                    T_Uvas.SetActive(true);
                    break;
                case "Naranja":
                    T_Naranja = E_Naranja.transform.GetChild(i).gameObject;
                    T_Naranja.SetActive(true);
                    break;
                case "Agua":
                    T_Agua = E_Agua.transform.GetChild(i).gameObject;
                    T_Agua.SetActive(true);
                    break;
                case "Cereal":
                    T_Cereal = E_Cereal.transform.GetChild(i).gameObject;
                    T_Cereal.SetActive(true);
                    break;
                case "Crema":
                    T_Crema = E_Crema.transform.GetChild(i).gameObject;
                    T_Crema.SetActive(true);
                    break;;
                case "SalsaTomate":
                    T_SalsaTomate = E_SalsaTomate.transform.GetChild(i).gameObject;
                    T_SalsaTomate.SetActive(true);
                    break;
                case "SalsaRosada":
                    T_SalsaRosada = E_SalsaRosada.transform.GetChild(i).gameObject;
                    T_SalsaRosada.SetActive(true);
                    break;
                case "Pocky":
                    T_Pocky = E_Pocky.transform.GetChild(i).gameObject;
                    T_Pocky.SetActive(true);
                    break;
                case "Rosquillas":
                    T_Rosquillas = E_Rosquillas.transform.GetChild(i).gameObject;
                    T_Rosquillas.SetActive(true);
                    break;
                case "Leche":
                    T_Leche = E_Leche.transform.GetChild(i).gameObject;
                    T_Leche.SetActive(true);
                    break;
                case "JuNaranja":
                    T_JuManzana = E_JuManzana.transform.GetChild(i).gameObject;
                    T_JuManzana.SetActive(true);
                    break;
                case "Soda":
                    T_Soda =E_Soda.transform.GetChild(i).gameObject;
                    T_Soda.SetActive(true);
                    break;
                default:
                    Debug.Log("No recibio ningun dato");
                    break;
            }
        }
        //For para recorrer la parte posterior del estante (Con objetos normales)
        for (int i = 0; i < 12; i++)
        {
            //dependiendo del objeto guardado en el vector Estante2 (el cual tiene los objetos en las pociones de la parte posterior del estante) se activara el objeto correcto
            switch (SeleccionDeOjeto.Estante2[i])
            {
                case "Banana":
                    T_Banana = E_Banana.transform.GetChild(i + 12).gameObject;
                    T_Banana.SetActive(true);
                    break;
                case "Manzana":
                    T_Manzana = E_Manzana.transform.GetChild(i + 12).gameObject;
                    T_Manzana.SetActive(true);
                    break;
                case "Pera":
                    T_Pera = E_Pera.transform.GetChild(i + 12).gameObject;
                    T_Pera.SetActive(true);
                    break;
                case "Uvas":
                    T_Uvas = E_Uvas.transform.GetChild(i + 12).gameObject;
                    T_Uvas.SetActive(true);
                    break;
                case "Naranja":
                    T_Naranja = E_Naranja.transform.GetChild(i + 12).gameObject;
                    T_Naranja.SetActive(true);
                    break;
                case "Agua":
                    T_Agua = E_Agua.transform.GetChild(i + 12).gameObject;
                    T_Agua.SetActive(true);
                    break;
                case "Cereal":
                    T_Cereal = E_Cereal.transform.GetChild(i + 12).gameObject;
                    T_Cereal.SetActive(true);
                    break;
                case "Crema":
                    T_Crema = E_Crema.transform.GetChild(i + 12).gameObject;
                    T_Crema.SetActive(true);
                    break;
                case "SalsaTomate":
                    T_SalsaTomate = E_SalsaTomate.transform.GetChild(i + 12).gameObject;
                    T_SalsaTomate.SetActive(true);
                    break;
                case "SalsaRosada":
                    T_SalsaRosada = E_SalsaRosada.transform.GetChild(i + 12).gameObject;
                    T_SalsaRosada.SetActive(true);
                    break;
                case "Pocky":
                    T_Pocky = E_Pocky.transform.GetChild(i + 12).gameObject;
                    T_Pocky.SetActive(true);
                    break;
                case "Rosquillas":
                    T_Rosquillas = E_Rosquillas.transform.GetChild(i + 12).gameObject;
                    T_Rosquillas.SetActive(true);
                    break;
                case "Leche":
                    T_Leche = E_Leche.transform.GetChild(i + 12).gameObject;
                    T_Leche.SetActive(true);
                    break;
                case "JuNaranja":
                    T_JuManzana = E_JuManzana.transform.GetChild(i + 12).gameObject;
                    T_JuManzana.SetActive(true);
                    break;
                case "Soda":
                    T_Soda = E_Soda.transform.GetChild(i + 12).gameObject;
                    T_Soda.SetActive(true);
                    break;
                default:
                    Debug.Log("No recibio ningun dato");
                    break;
            }
        }
        //For para recorrer la parte el lateral amarillo del estante (Con objetos normales)
        for (int i = 0; i < 8; i++)
        {
            //dependiendo del objeto guardado en el vector EstanteLA (el cual tiene los objetos en las pociones del lado amarillo del estante) se activara el objeto correcto
            switch (SeleccionDeOjeto.EstanteLA[i])
            {
                case "Banana":
                    T_Banana = E_Banana.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Banana.SetActive(true);
                    break;
                case "Manzana":
                    T_Manzana = E_Manzana.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Manzana.SetActive(true);
                    break;
                case "Pera":
                    T_Pera = E_Pera.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Pera.SetActive(true);
                    break;
                case "Uvas":
                    T_Uvas = E_Uvas.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Uvas.SetActive(true);
                    break;
                case "Naranja":
                    T_Naranja = E_Naranja.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Naranja.SetActive(true);
                    break;
                case "Agua":
                    T_Agua = E_Agua.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Agua.SetActive(true);
                    break;
                case "Cereal":
                    T_Cereal = E_Cereal.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Cereal.SetActive(true);
                    break;
                case "Crema":
                    T_Crema = E_Crema.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Crema.SetActive(true);
                    break;
                case "SalsaTomate":
                    T_SalsaTomate = E_SalsaTomate.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_SalsaTomate.SetActive(true);
                    break;
                case "SalsaRosada":
                    T_SalsaRosada = E_SalsaRosada.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_SalsaRosada.SetActive(true);
                    break;
                case "Pocky":
                    T_Pocky = E_Pocky.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Pocky.SetActive(true);
                    break;
                case "Rosquillas":
                    T_Rosquillas = E_Rosquillas.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Rosquillas.SetActive(true);
                    break;
                case "Leche":
                    T_Leche = E_Leche.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Leche.SetActive(true);
                    break;
                case "JuNaranja":
                    T_JuManzana = E_JuManzana.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_JuManzana.SetActive(true);
                    break;
                case "Soda":
                    T_Soda = E_Soda.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Soda.SetActive(true);
                    break;
                default:
                    Debug.Log("No recibio ningun dato");
                    break;

            }
        }
        //For para recorrer la parte el lateral rojo del estante (Con objetos normales) 
        for (int i = 0; i < 6; i++)
        {
            //dependiendo del objeto guardado en el vector EstanteLR (el cual tiene los objetos en las pociones del lado rojo del estante) se activara el objeto correcto
            switch (SeleccionDeOjeto.EstanteLR[i])
            {
                case "Banana":
                    T_Banana = E_Banana.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                    T_Banana.SetActive(true);
                    break;
                case "Manzana":
                    T_Manzana = E_Manzana.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                    T_Manzana.SetActive(true);
                    break;
                case "Pera":
                    T_Pera = E_Pera.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                    T_Pera.SetActive(true);
                    break;
                case "Uvas":
                    T_Uvas = E_Uvas.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                    T_Uvas.SetActive(true);
                    break;
                case "Naranja":
                    T_Naranja = E_Naranja.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                    T_Naranja.SetActive(true);
                    break;
                case "Agua":
                    T_Agua = E_Agua.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                    T_Agua.SetActive(true);
                    break;
                case "Cereal":
                    T_Cereal = E_Cereal.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                    T_Cereal.SetActive(true);
                    break;
                case "Crema":
                    T_Crema = E_Crema.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                    T_Crema.SetActive(true);
                    break;
                case "SalsaTomate":
                    T_SalsaTomate = E_SalsaTomate.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                    T_SalsaTomate.SetActive(true);
                    break;
                case "SalsaRosada":
                    T_SalsaRosada = E_SalsaRosada.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                    T_SalsaRosada.SetActive(true);
                    break;
                case "Pocky":
                    T_Pocky = E_Pocky.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                    T_Pocky.SetActive(true);
                    break;
                case "Rosquillas":
                    T_Rosquillas = E_Rosquillas.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                    T_Rosquillas.SetActive(true);
                    break;
                case "Leche":
                    T_Leche = E_Leche.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                    T_Leche.SetActive(true);
                    break;
                case "JuNaranja":
                    T_JuManzana = E_JuManzana.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                    T_JuManzana.SetActive(true);
                    break;
                case "Soda":
                    T_Soda = E_Soda.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                    T_Soda.SetActive(true);
                    break;
                default:
                    Debug.Log("No recibio ningun dato");
                    break;

            }
        }

        //For para recorrer la parte frontal del estante (Con variantes de los objetos)
        for (int i = 0; i < 12; i++)
        {
            //dependiendo del objeto guardado en el vector Estante1 (el cual tiene los objetos en las pociones de la parte frontal del estante) se activara el objeto correcto
            switch (SeleccionDeOjeto.Estante1[i])
            {
                case "BananaVari":
                    T_Banana = Variantes.transform.GetChild(0).gameObject.transform.GetChild(i).gameObject;
                    T_Banana.SetActive(true);
                    break;
                case "ManzanaVari":
                    T_Manzana = Variantes.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject;
                    T_Manzana.SetActive(true);
                    break;
                case "PeraVari":
                    T_Pera = Variantes.transform.GetChild(2).gameObject.transform.GetChild(i).gameObject;
                    T_Pera.SetActive(true);
                    break;
                case "UvasVari":
                    T_Uvas = Variantes.transform.GetChild(3).gameObject.transform.GetChild(i).gameObject;
                    T_Uvas.SetActive(true);
                    break;
                case "NaranjaVari":
                    T_Naranja = Variantes.transform.GetChild(4).gameObject.transform.GetChild(i).gameObject;
                    T_Naranja.SetActive(true);
                    break;
                case "AguaVari":
                    T_Agua = Variantes.transform.GetChild(5).gameObject.transform.GetChild(i).gameObject;
                    T_Agua.SetActive(true);
                    break;
                case "CerealVari":
                    T_Cereal = Variantes.transform.GetChild(6).gameObject.transform.GetChild(i).gameObject;
                    T_Cereal.SetActive(true);
                    break;
                case "CremaVari":
                    T_Crema = Variantes.transform.GetChild(7).gameObject.transform.GetChild(i).gameObject;
                    T_Crema.SetActive(true);
                    break;
                case "SalsaTomateVari":
                    T_SalsaTomate = Variantes.transform.GetChild(8).gameObject.transform.GetChild(i).gameObject;
                    T_SalsaTomate.SetActive(true);
                    break;
                case "SalsaRosadaVari":
                    T_SalsaRosada = Variantes.transform.GetChild(9).gameObject.transform.GetChild(i).gameObject;
                    T_SalsaRosada.SetActive(true);
                    break;
                case "PockyVari":
                    T_Pocky = Variantes.transform.GetChild(10).gameObject.transform.GetChild(i).gameObject;
                    T_Pocky.SetActive(true);
                    break;
                case "RosquillasVari":
                    T_Rosquillas = Variantes.transform.GetChild(11).gameObject.transform.GetChild(i).gameObject;
                    T_Rosquillas.SetActive(true);
                    break;
                case "LecheVari":
                    T_Leche = Variantes.transform.GetChild(12).gameObject.transform.GetChild(i).gameObject;
                    T_Leche.SetActive(true);
                    break;
                case "JuNaranjaVari":
                    T_JuManzana = Variantes.transform.GetChild(13).gameObject.transform.GetChild(i).gameObject;
                    T_JuManzana.SetActive(true);
                    break;
                case "SodaVari":
                    T_Soda = Variantes.transform.GetChild(14).gameObject.transform.GetChild(i).gameObject;
                    T_Soda.SetActive(true);
                    break;
                default:
                    Debug.Log("No recibio ningun dato");
                    break;
            }
        }
        //For para recorrer la parte posterior del estante (Con variantes de los objetos)
        for (int i = 0; i < 12; i++)
        {
            //dependiendo del objeto guardado en el vector Estante2 (el cual tiene los objetos en las pociones de la parte posterior del estante) se activara el objeto correcto
            switch (SeleccionDeOjeto.Estante2[i])
            {
                case "BananaVari":
                    T_Banana = Variantes.transform.GetChild(0).gameObject.transform.GetChild(i+12).gameObject;
                    T_Banana.SetActive(true);
                    break;
                case "ManzanaVari":
                    T_Manzana = Variantes.transform.GetChild(1).gameObject.transform.GetChild(i + 12).gameObject;
                    T_Manzana.SetActive(true);
                    break;
                case "PeraVari":
                    T_Pera = Variantes.transform.GetChild(2).gameObject.transform.GetChild(i + 12).gameObject;
                    T_Pera.SetActive(true);
                    break;
                case "UvasVari":
                    T_Uvas = Variantes.transform.GetChild(3).gameObject.transform.GetChild(i + 12).gameObject;
                    T_Uvas.SetActive(true);
                    break;
                case "NaranjaVari":
                    T_Naranja = Variantes.transform.GetChild(4).gameObject.transform.GetChild(i + 12).gameObject;
                    T_Naranja.SetActive(true);
                    break;
                case "AguaVari":
                    T_Agua = Variantes.transform.GetChild(5).gameObject.transform.GetChild(i + 12).gameObject;
                    T_Agua.SetActive(true);
                    break;
                case "CerealVari":
                    T_Cereal = Variantes.transform.GetChild(6).gameObject.transform.GetChild(i + 12).gameObject;
                    T_Cereal.SetActive(true);
                    break;
                case "CremaVari":
                    T_Crema = Variantes.transform.GetChild(7).gameObject.transform.GetChild(i + 12).gameObject;
                    T_Crema.SetActive(true);
                    break;
                case "SalsaTomateVari":
                    T_SalsaTomate = Variantes.transform.GetChild(8).gameObject.transform.GetChild(i + 12).gameObject;
                    T_SalsaTomate.SetActive(true);
                    break;
                case "SalsaRosadaVari":
                    T_SalsaRosada = Variantes.transform.GetChild(9).gameObject.transform.GetChild(i + 12).gameObject;
                    T_SalsaRosada.SetActive(true);
                    break;
                case "PockyVari":
                    T_Pocky = Variantes.transform.GetChild(10).gameObject.transform.GetChild(i + 12).gameObject;
                    T_Pocky.SetActive(true);
                    break;
                case "RosquillasVari":
                    T_Rosquillas = Variantes.transform.GetChild(11).gameObject.transform.GetChild(i + 12).gameObject;
                    T_Rosquillas.SetActive(true);
                    break;
                case "LecheVari":
                    T_Leche = Variantes.transform.GetChild(12).gameObject.transform.GetChild(i + 12).gameObject;
                    T_Leche.SetActive(true);
                    break;
                case "JuNaranjaVari":
                    T_JuManzana = Variantes.transform.GetChild(13).gameObject.transform.GetChild(i + 12).gameObject;
                    T_JuManzana.SetActive(true);
                    break;
                case "SodaVari":
                    T_Soda = Variantes.transform.GetChild(14).gameObject.transform.GetChild(i + 12).gameObject;
                    T_Soda.SetActive(true);
                    break;
                default:
                    Debug.Log("No recibio ningun dato");
                    break;
            }
        }
        //For para recorrer la parte el lateral amarillo del estante (Con variantes de los objetos)
        for (int i = 0; i < 8; i++)
        {
            //dependiendo del objeto guardado en el vector EstanteLA (el cual tiene los objetos en las pociones del lado amarillo del estante) se activara el objeto correcto
            switch (SeleccionDeOjeto.EstanteLA[i])
            {
                case "BananaVari":
                    T_Banana = Variantes.transform.GetChild(0).gameObject.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Banana.SetActive(true);
                    break;
                case "ManzanaVari":
                    T_Manzana = Variantes.transform.GetChild(1).gameObject.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Manzana.SetActive(true);
                    break;
                case "PeraVari":
                    T_Pera = Variantes.transform.GetChild(2).gameObject.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Pera.SetActive(true);
                    break;
                case "UvasVari":
                    T_Uvas = Variantes.transform.GetChild(3).gameObject.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Uvas.SetActive(true);
                    break;
                case "NaranjaVari":
                    T_Naranja = Variantes.transform.GetChild(4).gameObject.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Naranja.SetActive(true);
                    break;
                case "AguaVari":
                    T_Agua = Variantes.transform.GetChild(5).gameObject.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Agua.SetActive(true);
                    break;
                case "CerealVari":
                    T_Cereal = Variantes.transform.GetChild(6).gameObject.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Cereal.SetActive(true);
                    break;
                case "CremaVari":
                    T_Crema = Variantes.transform.GetChild(7).gameObject.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Crema.SetActive(true);
                    break;
                case "SalsaTomateVari":
                    T_SalsaTomate = Variantes.transform.GetChild(8).gameObject.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_SalsaTomate.SetActive(true);
                    break;
                case "SalsaRosadaVari":
                    T_SalsaRosada = Variantes.transform.GetChild(9).gameObject.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_SalsaRosada.SetActive(true);
                    break;
                case "PockyVari":
                    T_Pocky = Variantes.transform.GetChild(10).gameObject.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Pocky.SetActive(true);
                    break;
                case "RosquillasVari":
                    T_Rosquillas = Variantes.transform.GetChild(11).gameObject.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Rosquillas.SetActive(true);
                    break;
                case "LecheVari":
                    T_Leche = Variantes.transform.GetChild(12).gameObject.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Leche.SetActive(true);
                    break;
                case "JuNaranjaVari":
                    T_JuManzana = Variantes.transform.GetChild(13).gameObject.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_JuManzana.SetActive(true);
                    break;
                case "SodaVari":
                    T_Soda = Variantes.transform.GetChild(14).gameObject.transform.GetChild(24).gameObject.transform.GetChild(i).gameObject;
                    T_Soda.SetActive(true);
                    break;
                default:
                    Debug.Log("No recibio ningun dato");
                    break;

            }
        }
        //For para recorrer la parte el lateral rojo del estante (Con variantes de los objetos)
        for (int i = 0; i < 6; i++)
            {
            //dependiendo del objeto guardado en el vector EstanteLR (el cual tiene los objetos en las pociones del lado rojo del estante) se activara el objeto correcto
            switch (SeleccionDeOjeto.EstanteLR[i])
                {
                    case "BananaVari":
                        T_Banana = Variantes.transform.GetChild(0).gameObject.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                        T_Banana.SetActive(true);
                        break;
                    case "ManzanaVari":
                        T_Manzana = Variantes.transform.GetChild(1).gameObject.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                        T_Manzana.SetActive(true);
                        break;
                    case "PeraVari":
                        T_Pera = Variantes.transform.GetChild(2).gameObject.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                        T_Pera.SetActive(true);
                        break;
                    case "UvasVari":
                        T_Uvas = Variantes.transform.GetChild(3).gameObject.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                        T_Uvas.SetActive(true);
                        break;
                    case "NaranjaVari":
                        T_Naranja = Variantes.transform.GetChild(4).gameObject.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                        T_Naranja.SetActive(true);
                        break;
                    case "AguaVari":
                        T_Agua = Variantes.transform.GetChild(5).gameObject.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                        T_Agua.SetActive(true);
                        break;
                    case "CerealVari":
                        T_Cereal = Variantes.transform.GetChild(6).gameObject.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                        T_Cereal.SetActive(true);
                        break;
                    case "CremaVari":
                        T_Crema = Variantes.transform.GetChild(7).gameObject.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                        T_Crema.SetActive(true);
                        break;
                    case "SalsaTomateVari":
                        T_SalsaTomate = Variantes.transform.GetChild(8).gameObject.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                        T_SalsaTomate.SetActive(true);
                        break;
                    case "SalsaRosadaVari":
                        T_SalsaRosada = Variantes.transform.GetChild(9).gameObject.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                        T_SalsaRosada.SetActive(true);
                        break;
                    case "PockyVari":
                        T_Pocky = Variantes.transform.GetChild(10).gameObject.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                        T_Pocky.SetActive(true);
                        break;
                    case "RosquillasVari":
                        T_Rosquillas = Variantes.transform.GetChild(11).gameObject.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                        T_Rosquillas.SetActive(true);
                        break;
                    case "LecheVari":
                        T_Leche = Variantes.transform.GetChild(12).gameObject.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                        T_Leche.SetActive(true);
                        break;
                    case "JuNaranjaVari":
                        T_JuManzana = Variantes.transform.GetChild(13).gameObject.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                        T_JuManzana.SetActive(true);
                        break;
                    case "SodaVari":
                        T_Soda = Variantes.transform.GetChild(14).gameObject.transform.GetChild(25).gameObject.transform.GetChild(i).gameObject;
                        T_Soda.SetActive(true);
                        break;
                    default:
                        Debug.Log("No recibio ningun dato");
                        break;

                }
            }

        //Switch case para activar el monster en una de las 4 pociciones predeterminadas que tienen, dependiendo del valor de posicion obtenido de firebase
        switch (SeleccionDeOjeto.Monster)
        {
            case 1:
                T_Monster = E_Monster.transform.GetChild(0).gameObject;
                T_Monster.SetActive(true);
                break;
            case 2:
                T_Monster = E_Monster.transform.GetChild(1).gameObject;
                T_Monster.SetActive(true);
                break;
            case 3:
                T_Monster = E_Monster.transform.GetChild(2).gameObject;
                T_Monster.SetActive(true);
                break;
            case 4:
                T_Monster = E_Monster.transform.GetChild(3).gameObject;
                T_Monster.SetActive(true);
                break;
            default:
                Debug.Log("No recibio ningun dato");
                break;
        }

        
    }


    //Delay de 2 segundos
    IEnumerator Saltos()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Salto de scena");
    }
}


