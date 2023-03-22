using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class cambioscena : MonoBehaviour
{
    // Declaración de objetos
    [SerializeField] private GameObject sonidoBoton;
    [SerializeField] private Text TMonster;
    [SerializeField] private Text[] Nombres = new Text[12];
    [SerializeField] public  Image[] Checks = new Image[6];
    [SerializeField] private Image[] Imagenes = new Image[12];
    [SerializeField] private GameObject tareaz;
    [SerializeField] private Button siguientes;

    // Variables para inicio de sesión 
    private string emailLoginField;
    private string passwordLoginField;

    void Start()
    {
        //Usuario y contraseña principal de firebase
        emailLoginField = "king@gmail.com";
        passwordLoginField = "123456";

        //Condicional que asegura que no se ejecute la sentencia a menos que haya un objeto en "tareaz"
        if (tareaz != null)
        {
            //Recorre el numero de retos para colocar el objeto y cuantos de estos requiere buscar
            for (int i = 0; i < SeleccionDeOjeto.numeroRetos; i++)
            {
                //Coloca los valores de cuantos y cual objeto en un cuadro de texto en la escena "Objetivos"
                //tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " " + SeleccionDeOjeto.NomMostrar[i]; 

                switch (SeleccionDeOjeto.NomMostrar[i])
                {
                    case "Banana":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Bananas ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " " + SeleccionDeOjeto.NomMostrar[i];
                        }
                        break;
                    case "Manzana":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Manzanas ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " " + SeleccionDeOjeto.NomMostrar[i];
                        }                       

                        break;
                    case "Pera":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Peras ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " " + SeleccionDeOjeto.NomMostrar[i];
                        }
                        
                        break;
                    case "Uvas":

                        tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " " + SeleccionDeOjeto.NomMostrar[i];

                        break;
                    case "Naranja":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Naranjas ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " " + SeleccionDeOjeto.NomMostrar[i];
                        }
                        
                        break;
                    case "Agua":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Botellas de agua ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Botella de agua ";
                        }
                        
                        break;
                    case "Cereal":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Cajas de cereal ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Caja de cereal ";
                        }
                        
                        break;
                    case "Crema":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Cremas ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " " + SeleccionDeOjeto.NomMostrar[i];
                        }                    

                        break;
                    case "SalsaTomate":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Salsas de tomate ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Salsa de tomate ";
                        }                        

                        break;
                    case "SalsaRosada":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Salsas rosada ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Salsa rosada ";
                        }

                        break;
                    case "Pocky":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Pockys ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " " + SeleccionDeOjeto.NomMostrar[i];
                        }

                        break;
                    case "Rosquillas":
                        tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " " + SeleccionDeOjeto.NomMostrar[i];

                        break;
                    case "Leche":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Cajas de leche ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Caja de leche ";
                        }

                        break;
                    case "JuNaranja":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Jugos de naranja ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Jugo de naranja ";
                        }

                        break;
                    case "Soda":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Sodas ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " " + SeleccionDeOjeto.NomMostrar[i];
                        }

                        break;
                    case "BananaVari":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Bananas vari ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Banana vari ";
                        }

                        break;
                    case "ManzanaVari":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Manzanas vari ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Manzana vari ";
                        }

                        break;
                    case "PeraVari":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Peras vari ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Pera vari ";
                        }

                        break;
                    case "UvasVari":
                        tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Uvas vari ";

                        break;
                    case "NaranjaVari":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Naranjas vari ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Naranja vari ";
                        }

                        break;
                    case "AguaVari":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Botellas de agua vari ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Botella de agua vari ";
                        }

                        break;
                    case "CerealVari":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Cajas de cereal vari ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Caja de cereal vari";
                        }

                        break;
                    case "CremaVari":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Cremas vari ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Crema vari";
                        }

                        break;
                    case "SalsaTomateVari":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Salsas de tomate vari ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Salsa de tomate vari ";
                        }

                        break;
                    case "SalsaRosadaVari":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Salsas rosada vari ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Salsa rosada vari ";
                        }

                        break;
                    case "PockyVari":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Pockys vari ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Pocky vari ";
                        }

                        break;
                    case "RosquillasVari":

                        tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Rosquillas vari ";

                        break;
                    case "LecheVari":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Cajas de leche vari ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Caja de leche vari ";
                        }

                        break;
                    case "JuNaranjaVari":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Jugos de naranja vari ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Jugo de naranja vari ";
                        }

                        break;
                    case "SodaVari":
                        if (SeleccionDeOjeto.misiones[i] > 1)
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Sodas vari ";
                        }
                        else
                        {
                            tareaz.transform.GetChild(i).GetComponent<Text>().text = " " + SeleccionDeOjeto.misiones[i] + " Soda vari ";
                        }

                        break;
                }

            }
        }
        // Obtener la fecha actual del sistema 
        DateTime fechaactual = DateTime.Now;
        // Obtencion de los datos de fecha inicio y fecha fin, del script de SeleccionDeOjeto 
        DateTime FI = SeleccionDeOjeto.fecha1;
        DateTime FF = SeleccionDeOjeto.fecha2;
        //Condicional para saber si existe el boton de "siguiente" en la escena para poder activarlo o desactivarlo 
        if (siguientes != null)
        {
            //Condicional para validar si la fecha esta dentro del rango asiganado para poder activar el boton "siguiente"
            if (fechaactual >= FI && fechaactual <= FF)
            {
                //Si la condicion se cumple se activa el boton "siguiente" para poder continuear al ejercicio 
                siguientes.gameObject.SetActive(true);

            }
        }

    }

    //Cambia a la escena "Juego"
    public void Juego()
    {
        Cargador.cargarNivel("Juego");
        //SceneManager.LoadScene();
    }
    //Cambia a la escena "Victoria"
    public void Victoria()
    {
        //Aumenta en uno el conteo de intentos del ejercicio
        SeleccionDeOjeto.intentos = SeleccionDeOjeto.intentos + 1;
        Cargador.cargarNivel("Victoria");
        //SceneManager.LoadScene("Victoria");
    }
    //Cambia a la escena "Derrota"
    public void Derrota()
    {
        //Aumenta en uno el conteo de intentos del ejercicio
        SeleccionDeOjeto.intentos = SeleccionDeOjeto.intentos + 1;
        Cargador.cargarNivel("Derrota");
        //SceneManager.LoadScene("Derrota");
    }
    //Cambia a la escena "Menu" y resetea todas las variables
    public void Menu()
    {
        StartCoroutine(delays());
        Cargador.cargarNivel("Menu");
        //SceneManager.LoadScene("Menu");
    }
    //Cambia a la escena "Objetivo"
    public void Objetivos()
    {
        SceneManager.LoadScene("Objetivos");
    }
    //Cambia a la escena "Login"
    public void login()
    {
  
        SceneManager.LoadScene("login");
    }
    //Cambia a la escena perfil y resetea todas las variables 
    public void Perfil()
    {
        StartCoroutine(delays());
        SeleccionDeOjeto.bandera = false;
        Cargador.cargarNivel("Perfil");
        //SceneManager.LoadScene("Perfil");
    }
    //Instancia el objeto de sonido para agregar a los botones 
    public void SonidoBoton()
    {
        Instantiate(sonidoBoton);
    }
    //Sale de la aplicacion 
    public void salir()
    {
        Application.Quit();
        Debug.Log("Se ha salido del juego");
    }
    //Funcion para mostrar el progreso del juego en el menu desplegable 
    public void Mostrar()
    {
        //Condicion para completar la tarea del monster
        if (Timer.Monster == 1)
        {
            TMonster.text = "Completado";
        }
        //Ciclo for para llenar un vector con las misiones a completar 
        for (int i = 0; i < SeleccionDeOjeto.numeroRetos; i++)
        {
            Nombres[i].text = SeleccionDeOjeto.misiones[i].ToString();

        }

        for (int i = 0; i < SeleccionDeOjeto.NomMostrar.Length; i++)
        {
            //Swich case para cargar las imagnes que correspondan a los objetos del reto 
            switch (SeleccionDeOjeto.NomMostrar[i])
            {
                case "Banana":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/BananaR");

                    break;
                case "Manzana":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/ManzanaR");

                    break;
                case "Pera":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/PeraR");

                    break;
                case "Uvas":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/UvasR");

                    break;
                case "Naranja":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/NaranjaR");

                    break;
                case "Agua":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/Agua");

                    break;
                case "Cereal":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/Cereal");

                    break;
                case "Crema":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/Crema");

                    break;
                case "Vinagre":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/BananaR");

                    break;
                case "SalsaTomate":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/SalsaTomate");

                    break;
                case "SalsaRosada":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/SalsaRosada");

                    break;
                case "Pocky":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/Pocky");

                    break;
                case "Rosquillas":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/Rosquillas");

                    break;
                case "Leche":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/Leche");

                    break;
                case "JuNaranja":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/JugoNaranja");

                    break;
                case "Soda":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/Soda");

                    break;
                case "BananaVari":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/BananaVari");

                    break;
                case "ManzanaVari":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/ManzanaVari");

                    break;
                case "PeraVari":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/PeraVari");

                    break;
                case "UvasVari":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/UvasVari");

                    break;
                case "NaranjaVari":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/NaranjaVari");

                    break;
                case "AguaVari":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/AguaVari");

                    break;
                case "CerealVari":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/CerealVari");

                    break;
                case "CremaVari":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/CremaVari");

                    break;
                case "VinagreVari":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/BananaVari");

                    break;
                case "SalsaTomateVari":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/SalsaTomateVari");

                    break;
                case "SalsaRosadaVari":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/SalsaRosadaVari");

                    break;
                case "PockyVari":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/PockyVari");

                    break;
                case "RosquillasVari":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/RosquillasVari");

                    break;
                case "LecheVari":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/LecheVari");

                    break;
                case "JuNaranjaVari":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/JugoNaranjaVari");

                    break;
                case "SodaVari":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/SodaVari");

                    break;
                case "Monster":
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/MonsterR");

                    break;
                default:
                    Imagenes[i].sprite = Resources.Load<Sprite>("images/Vacio");
                    break;
            }

        }

        //Condición para marcar como completado la busqueda de un objeto de las misiones 
        for (int i = 0; i < SeleccionDeOjeto.numeroRetos; i++)
        {
            //Como se va restando el numero del objetos de un tipo a buscar en la mision con el numero de objetos de ese tipo encontrados,
            //al llegar a -1 indicara que ya a encontrado todos los objetos de la mision 
            if (Nombres[i].text == "-1")
            {
                Nombres[i].text = "Completado";
            }
            //Hace la resta del numero de objetos de la mision y el numero de objetos que va encontrando
            else if( Timer.val[i] > 0)
            {
                Nombres[i].text = (SeleccionDeOjeto.misiones[i] - Timer.val[i]).ToString();
            }
        }     
    }

    //Reseteo de variables y delay de 2 segundos
    IEnumerator delays()
    {
        for (int i = 0; i < Timer.val.Length; i++)
        {
            Timer.val[i] = 0;
        }
        Timer.Banana = 0;
        Timer.Pera = 0;
        Timer.Uvas = 0;
        Timer.Manzana = 0;
        Timer.Naranja = 0;
        Timer.Pocky = 0;
        Timer.Agua = 0;
        Timer.SalsaTomate = 0;
        Timer.SalsaRosada = 0;
        Timer.Vinagre = 0;
        Timer.Soda = 0;
        Timer.Leche = 0;
        Timer.Monster = 0;
        Timer.JuNaranja = 0;
        Timer.Rosquillas = 0;
        Timer.Crema = 0;
        Timer.Cereal = 0;
        Timer.BananaVari = 0;
        Timer.PeraVari = 0;
        Timer.UvasVari = 0;
        Timer.ManzanaVari = 0;
        Timer.NaranjaVari = 0;
        Timer.PockyVari = 0;
        Timer.AguaVari = 0;
        Timer.SalsaTomateVari = 0;
        Timer.SalsaRosadaVari = 0;
        Timer.VinagreVari = 0;
        Timer.SodaVari = 0;
        Timer.LecheVari = 0;
        Timer.JuNaranjaVari = 0;
        Timer.RosquillasVari = 0;
        Timer.CremaVari = 0;
        Timer.CerealVari = 0;
        Timer.conteo = 0;
        Timer.acierto = 0;
        Timer.conteoErrores = 0;
        Timer.Monster = 0;
        SeleccionDeOjeto.fallas = 0;
        yield return new WaitForSeconds(2);

    }

}
