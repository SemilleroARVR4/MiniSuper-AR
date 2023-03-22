using System.Collections;
using System;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SeleccionDeOjeto : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;
    //Login principal variables
    [Header("Login")]
    string emailLoginField;
    string passwordLoginField;
    //Variables par alos ejercicios
    [SerializeField] private Button[] exercise = new Button[7];
    [SerializeField] private GameObject Exercise;
    //Objetos para mostrar mensajes de inicio correcto o incorrecto de sesion
    [SerializeField] private GameObject UserInvalido;
    [SerializeField] private GameObject UserValido;

    //Variables de tiempo y posicion del monster
    public static int Monster;
    public static string tiempoL;
    //Variables para los esacios del stante
    public static string espacioF1;
    public static string espacioF2;
    public static string espacioF3;
    public static string espacioF4;
    public static string espacioF5;
    public static string espacioF6;
    public static string espacioF7;
    public static string espacioF8;
    public static string espacioF9;
    public static string espacioF10;
    public static string espacioF11;
    public static string espacioF12;

    public static string espacioP1;
    public static string espacioP2;
    public static string espacioP3;
    public static string espacioP4;
    public static string espacioP5;
    public static string espacioP6;
    public static string espacioP7;
    public static string espacioP8;
    public static string espacioP9;
    public static string espacioP10;
    public static string espacioP11;
    public static string espacioP12;

    public static string LA_espacio1;
    public static string LA_espacio2;
    public static string LA_espacio3;
    public static string LA_espacio4;
    public static string LA_espacio5;
    public static string LA_espacio6;
    public static string LA_espacio7;
    public static string LA_espacio8;

    public static string LR_espacio1;
    public static string LR_espacio2;
    public static string LR_espacio3;
    public static string LR_espacio4;
    public static string LR_espacio5;
    public static string LR_espacio6;

    public static string[] Estante1  = new string[12];
    public static string[] Estante2  = new string[12];
    public static string[] EstanteLA = new string[8];
    public static string[] EstanteLR = new string[6];

    //Variables de reto
    private static string[] conObjetos = { "Manzana", "Banana", "Leche", "Cereal", "Naranja", "Soda", "SalsaTomate", "Crema", "JuNaranja", "Pera", "Pocky", "Rosquillas", "Uvas", "Agua", "SalsaRosada", "ManzanaVari", "BananaVari", "LecheVari", "CerealVari", "NaranjaVari", "SodaVari", "SalsaTomateVari", "CremaVari", "JuNaranjaVari", "PeraVari", "PockyVari", "RosquillasVari", "UvasVari", "AguaVari", "SalsaRosadaVari" };
    private string[] nombres = { "Manzana", "Banana", "Leche", "Cereal", "Naranja", "Soda", "SalsaTomate", "Crema", "JuNaranja", "Pera", "Pocky", "Rosquillas", "Uvas", "Agua", "SalsaRosada", "ManzanaVari", "BananaVari", "LecheVari", "CerealVari", "NaranjaVari", "SodaVari", "SalsaTomateVari", "CremaVari", "JuNaranjaVari", "PeraVari", "PockyVari", "RosquillasVari", "UvasVari", "AguaVari", "SalsaRosadaVari" };
    public static string[] NomMostrar;
    public static int numeroRetos;
    public static int[] misiones;
    public static string[] mNombres;
    public static int[] posiciones;
    public static int conteo = 0;
    public static int[] misionesMostrar;
    public bool[] reto;
    public int mostrar;
    public bool Verificacion =  true;

    //Variales del perfil y fecha 
    public static DateTime FechaFin;
    public static DateTime FechaIni;
    public static string Userss    = "";
    public static string Userssd   = "";
    public static string Ejercicio;
    public static int NumEjercicio = 0;
    public static string[] Eje;
    public static string Token = "";
    public int numeroDocs = 0;
    public int numeroE = 0;
    public string[] docs;

    //variables para la fecha
    public static string FI;
    public static string FF;
    public static string fechaFinal;
    public static string fechaInicial;
    public static DateTime fecha1;
    public static DateTime fecha2;
    public static int dia1;
    public static int mes1;
    public static int año1;
    public static int dia2;
    public static int mes2;
    public static int año2;


    //Variable andera para activar el envio de datos a firebase
    public static bool bandera = false;
    //Variables a enviar
    public static int fallas;
    public static string TiempoUtilizado;
    public static int intentos;
    public static int aciertos;

    void Start()
    {
        //Datos para el incio de sesión general en firebase
        emailLoginField = "king@gmail.com";
        passwordLoginField = "123456";
        //Iniciar la conexion con la base de datos de firebase
        InitializeFirebase();

        //Condicion para ejecutar la fucnion de obtener ejercicio siempre y cuando el valor del numero de ejercicios sea mayor a 0
        if (NumEjercicio > 0)
        {
            StartCoroutine(Saltos2());
            obtenerEjercicio();
        }
        //Condicion para ejercutar la fucnion de evnio de datos a firebase
        if (bandera == true )
        {
            EnviarDatos();
        }


    }
    //Iniciar conexioncon firebase
    private void InitializeFirebase()
    {
        Debug.Log("Configuración de autenticación de Firebase");

        //Establecer el objeto de la instancia de autenticación
        auth = FirebaseAuth.DefaultInstance;
        FirebaseApp.Create();
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;

    }

    //Funcion para inicio de seccion y obtencion de datos del usuario
    public void LoginButton()
    {
        sacarUsuarios();
        StartCoroutine(Login(emailLoginField, passwordLoginField));
        StartCoroutine(Saltos2());
        otenerFecha();


    }
    //Funcion para inicio de sesión y obtencion de datos del usuario y cambio de escena a "Objetivos" 
    public void LoginButton2()
    {
       
        sacarUsuarios();
        StartCoroutine(Login(emailLoginField, passwordLoginField));
        StartCoroutine(Saltos1());
        otenerFecha();

    }
    //Funcion para cargar los datos de las misiones de los ejercicios
    public void LoadReto()
    {
        StartCoroutine(loadReto(OnHit.nicks, Ejercicio,Token));
        StartCoroutine(Saltos1());
    }
    //Fucnion para enviar los datos a firebase de resultado de los ejercicios
    public void EnviarDatos()
    {
        StartCoroutine(Saltos2());
        StartCoroutine(Enviarfalals(OnHit.nicks, fallas,Token,Ejercicio));
        StartCoroutine(Enviartiempo(OnHit.nicks, TiempoUtilizado,Token,Ejercicio));
        StartCoroutine(Enviaraciertos(OnHit.nicks, aciertos, Token, Ejercicio));
        Debug.Log("Se Han enviado los datos exitosamente");
    }
    //Funcion para obtener las fechas entre las cuales se pueden realizar los ejercicios
    public void otenerFecha()
    {
        if (OnHit.nicks != "" && Ejercicio != "" && Token != "")
        {
            StartCoroutine(CargarFecha(OnHit.nicks, Ejercicio, Token));
            StartCoroutine(Cargarintentos(OnHit.nicks, Ejercicio, Token));
        }      
        StartCoroutine(Saltos2());

    }
    //Funcion para obtener el numero de ejercicios asignados a los usuarios 
    public void obtenerEjercicio()
    {
        sacarEjercicio();
        if (Exercise != null)
        {
            for (int i = 0; i < NumEjercicio; i++)
            {
                Exercise.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = Eje[i];
                Exercise.transform.GetChild(i).gameObject.SetActive(true);

            }
        }
 
    }
    //funcion para sacar datos del nombre de usuario
    private void sacarUsuarios()
    {
        StartCoroutine(SacarUsuarios(Verificacion));
        StartCoroutine(Saltos2());
    }
    //Funcion para obtener los ejercicios asignados por usuario 
    private void sacarEjercicio()
    {
        if (Token != null && OnHit.nicks != null)
        {
            StartCoroutine(SacarEjercicio(Token, OnHit.nicks));          
        }
        StartCoroutine(Saltos2());
    }
    //Limpiar variables de inicio de sesión y datos del usuario
    public void limpiarVariables()
    {
      Userss = "";
      Userssd = "";
      Token = "";
      OnHit.nicks = "";
      StartCoroutine(Saltos2());
    }
    //Funcion para cerrar sesión 
    public void signOut()
    {
        auth.SignOut();
        Debug.Log("Se ha cerrado la sesion");
    }
    //Corrutina para el inicio de sesioón 
    private IEnumerator Login(string _email, string _password)
    {
        //Llame a la función de inicio de sesión de autenticación de Firebase pasando el correo electrónico y la contraseña
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);

        //Espere hasta que la tarea se complete
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //Si hay errores manejarlos
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
        }
        else
        {
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);

            //Se ejecuta la corrutina para obtener el nombre de usuario
            StartCoroutine(Loaduserss(Token));
            //Se ejecutan todas las corrutinas para obtener los datos completos de usuario
            //pero sin antes verificar que haya un nombre de usuario y un token(id de terapeuta encargado) para evitar errores de variables vacias
            if (Token != null && OnHit.nicks != null)
            {
                StartCoroutine(SacarEjercicio(Token, OnHit.nicks));
                StartCoroutine(CargarUserData(OnHit.nicks, Ejercicio, Token));
                StartCoroutine(loadReto(OnHit.nicks, Ejercicio, Token));
                StartCoroutine(Cargarintentos(OnHit.nicks, Ejercicio, Token));
                StartCoroutine(CargarFecha(OnHit.nicks, Ejercicio, Token));
            }

            yield return new WaitForSeconds(2);
        }
    }
    //Corrutina para cargar las misiones asignadas a los ejercicios 
    private IEnumerator loadReto(string paciente, string ejercicio, string _Token)
    {

        var DBTask = DBreference.Child("users").Child(_Token).Child("Usuarios").Child(paciente).Child("Ejercicio").Child(ejercicio).Child("Reto").GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
        Debug.Log("EL USUARIO ES: " + paciente);
        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }

        if (DBTask.Result.Value == null)
        {

        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;
            numeroRetos = int.Parse(snapshot.ChildrenCount.ToString());
            //Se declaran vectores para llenar el numero de retos, le numero de objetos a buscar de las misiones del reto y los nombres de los objetos del reto 
            //Ademas de un vector tipo bool para saber los objetos asignados por reto 
            reto = new bool[nombres.Length];
            misiones = new int[numeroRetos];
            mNombres = new string[numeroRetos];
            int j = 0;
            //Se recorren todos los nombres de los objetos y se guarda en el vector de reto un valor de "true" si el objeto es requerido 
            // en la mision y un valro de "false" si el objeto no es parte de la mision 
            for (int i = 0; i < reto.Length; i++)
            {
                reto[i] = snapshot.Child(nombres[i]).Exists;
            }                      
            //Se recorre el vector de reto para sacar el nombre del objeto y la cantidad de ese objeto que se debe buscar
            for (int i = 0; i < reto.Length; i++)
            {
                    //Si el valor es "true" significa que ese objeto esta dentro de la mision 
                    if (reto[i] == true)
                    {
                      // Se guarda en un vector el nombre del objeto
                       mNombres[j] = nombres[i];
                      //Se guarda en un vector el valor de la cantidad del objeto que debe buscar 
                        misiones[j] = int.Parse(snapshot.Child(nombres[i]).Value.ToString());
                        j++;
                
                    }

            }
            //Se decalran 2 vectores para mostrar los datos de la mision y uno mas para obtener las posiciones de los objetos dentro del vector que los contiene a todos 
            NomMostrar = new string[numeroRetos];
            posiciones = new int[mNombres.Length];
            misionesMostrar = new int[misiones.Length];
            //Se llena el vector de misiones a mostrar con el vector de misiones obtenido anteriormente 
            for (int i = 0; i < misiones.Length; i++)
            {
                misionesMostrar[i] = misiones[i];
            }

            //Ciclo for para recorrer el vector con todos los nombres de los objetos 
            for (int i = 0; i < conObjetos.Length; i++)
            {
                //Ciclo for para recorrer los nombres de los objetos encontrados en la mision 
                for (int s = 0; s < mNombres.Length; s++)
                {
                    //Si el objeto coincide con el objeto de la mision entonces se sacara la posicion de ese objeto en el 
                    // vector que conteiene a todos los objetos y se guardara el nombre de ese objeto en el vector "NomMostrar"
                    if (conObjetos[i] == mNombres[s])
                    {
                        posiciones[s] = i;
                        NomMostrar[s] = conObjetos[i];
                    }
                }

            }
        }


    }
    //Corrutina para obtener las posiciones de los objetos dentro del estante, ademas de la posicion del monster y el tiempo requerido 
    private IEnumerator CargarUserData(string paciente,string ejercicio, string _Token)
    {
        //Obtener los datos del usuario que ha iniciado sesión actualmente
        var DBTask = DBreference.Child("users").Child(_Token).Child("Usuarios").Child(paciente).Child("Ejercicio").Child(ejercicio).Child("Estante").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }

        if (DBTask.Result.Value == null)
        {

        }
        else
        {
            //Los datos han sido recuperados
                        DataSnapshot snapshot = DBTask.Result;
            //Se obtiene de firebase la posicion del monster
            Monster   = int.Parse(snapshot.Child("Monster").Value.ToString());
            //Se obtiene de firebase el tiempo limite para completar la mision
            tiempoL   = snapshot.Child("Tiempo").Value.ToString();
            //Se obtiene de firebase los nombres de los objetos en cada posicion del estante 
            espacioF1  = snapshot.Child("estanteFrontal1").Value.ToString();
            espacioF2  = snapshot.Child("estanteFrontal2").Value.ToString();
            espacioF3  = snapshot.Child("estanteFrontal3").Value.ToString();
            espacioF4  = snapshot.Child("estanteFrontal4").Value.ToString();
            espacioF5  = snapshot.Child("estanteFrontal5").Value.ToString();
            espacioF6  = snapshot.Child("estanteFrontal6").Value.ToString();
            espacioF7  = snapshot.Child("estanteFrontal7").Value.ToString();
            espacioF8  = snapshot.Child("estanteFrontal8").Value.ToString();
            espacioF9  = snapshot.Child("estanteFrontal9").Value.ToString();
            espacioF10 = snapshot.Child("estanteFrontal10").Value.ToString();
            espacioF11 = snapshot.Child("estanteFrontal11").Value.ToString();
            espacioF12 = snapshot.Child("estanteFrontal12").Value.ToString();

            //Se guardan los valores en un vector para poder instanciar luego los objetos en los espacios correctos 
            Estante1[0] = espacioF1;
            Estante1[1] = espacioF2;
            Estante1[2] = espacioF3;
            Estante1[3] = espacioF4;
            Estante1[4] = espacioF5;
            Estante1[5] = espacioF6;
            Estante1[6] = espacioF7;
            Estante1[7] = espacioF8;
            Estante1[8] = espacioF9;
            Estante1[9] = espacioF10;
            Estante1[10] = espacioF11;
            Estante1[11] = espacioF12;

            espacioP1 = snapshot.Child("estantePosterior1").Value.ToString();
            espacioP2  = snapshot.Child("estantePosterior2").Value.ToString();
            espacioP3  = snapshot.Child("estantePosterior3").Value.ToString();
            espacioP4  = snapshot.Child("estantePosterior4").Value.ToString();
            espacioP5  = snapshot.Child("estantePosterior5").Value.ToString();
            espacioP6  = snapshot.Child("estantePosterior6").Value.ToString();
            espacioP7  = snapshot.Child("estantePosterior7").Value.ToString();
            espacioP8  = snapshot.Child("estantePosterior8").Value.ToString();
            espacioP9  = snapshot.Child("estantePosterior9").Value.ToString();
            espacioP10 = snapshot.Child("estantePosterior10").Value.ToString();
            espacioP11 = snapshot.Child("estantePosterior11").Value.ToString();
            espacioP12 = snapshot.Child("estantePosterior12").Value.ToString();

            Estante2[0] = espacioP1;
            Estante2[1] = espacioP2;
            Estante2[2] = espacioP3;
            Estante2[3] = espacioP4;
            Estante2[4] = espacioP5;
            Estante2[5] = espacioP6;
            Estante2[6] = espacioP7;
            Estante2[7] = espacioP8;
            Estante2[8] = espacioP9;
            Estante2[9] = espacioP10;
            Estante2[10] = espacioP11;
            Estante2[11] = espacioP12;

            LA_espacio1 = snapshot.Child("estantelateralAmarillo1").Value.ToString();
            LA_espacio2 = snapshot.Child("estantelateralAmarillo2").Value.ToString();
            LA_espacio3 = snapshot.Child("estantelateralAmarillo3").Value.ToString();
            LA_espacio4 = snapshot.Child("estantelateralAmarillo4").Value.ToString();
            LA_espacio5 = snapshot.Child("estantelateralAmarillo5").Value.ToString();
            LA_espacio6 = snapshot.Child("estantelateralAmarillo6").Value.ToString();
            LA_espacio7 = snapshot.Child("estantelateralAmarillo7").Value.ToString();
            LA_espacio8 = snapshot.Child("estantelateralAmarillo8").Value.ToString();

            EstanteLA[0] = LA_espacio1;
            EstanteLA[1] = LA_espacio2;
            EstanteLA[2] = LA_espacio3;
            EstanteLA[3] = LA_espacio4;
            EstanteLA[4] = LA_espacio5;
            EstanteLA[5] = LA_espacio6;
            EstanteLA[6] = LA_espacio7;
            EstanteLA[7] = LA_espacio8;

            LR_espacio1 = snapshot.Child("estantelateralRojo1").Value.ToString();
            LR_espacio2 = snapshot.Child("estantelateralRojo2").Value.ToString();
            LR_espacio3 = snapshot.Child("estantelateralRojo3").Value.ToString();
            LR_espacio4 = snapshot.Child("estantelateralRojo4").Value.ToString();
            LR_espacio5 = snapshot.Child("estantelateralRojo5").Value.ToString();
            LR_espacio6 = snapshot.Child("estantelateralRojo6").Value.ToString();

            EstanteLR[0] = LR_espacio1;
            EstanteLR[1] = LR_espacio2;
            EstanteLR[2] = LR_espacio3;
            EstanteLR[3] = LR_espacio4;
            EstanteLR[4] = LR_espacio5;
            EstanteLR[5] = LR_espacio6;

        }
    }
    //Corrutina para sacar datos del usuario
    private IEnumerator Loaduserss(string _Token)
    {
        //Obtener los datos del usuario que ha iniciado sesión actualmente
        var DBTask = DBreference.Child("users").Child(_Token).Child("Usuarios").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }

        if (DBTask.Result.Value == null)
        {

        }
        else
        {
            //Los datos han sido recuperados
            DataSnapshot snapshot = DBTask.Result;
            //Se obtiene de firebase el nombre y la identificacion del usuario
            Userss  = snapshot.Child(OnHit.nicks).Child("Nombre Completo").Value.ToString();
            Userssd = snapshot.Child(OnHit.nicks).Child("Documento").Value.ToString();

        }
    }
    //Corrutina para obtener datos del usuario asociados al ejercicio
    private IEnumerator Cargarintentos(string paciente, string ejercicio, string _Token)
    {
        //Obtener los datos del usuario que ha iniciado sesión actualmente
        var DBTask = DBreference.Child("users").Child(_Token).Child("Usuarios").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }

        if (DBTask.Result.Value == null)
        {

        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            //Se obtiene de firebase la cantidad de ejercicios asigandos al usuario
            NumEjercicio = int.Parse(snapshot.Child(paciente).Child("Ejercicio").ChildrenCount.ToString());
            //Se obtiene de firebase el numero de intentos realizados por ejercicio 
            intentos = int.Parse(snapshot.Child(paciente).Child("Errores").Child(ejercicio).ChildrenCount.ToString());

        }
    }

    private IEnumerator CargarFecha(string paciente, string ejercicio, string _Token)
    {
        //Obtener los datos del usuario que ha iniciado sesión actualmente
        var DBTask = DBreference.Child("users").Child(_Token).Child("Usuarios").Child(paciente).Child("Ejercicio").Child(ejercicio).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }

        if (DBTask.Result.Value == null)
        {

        }
        else
        {
            //Los datos han sido recuperados
                        DataSnapshot snapshot = DBTask.Result;
            //Se obtiene de firebase el intervalo de fecha en la que se podra realizar el ejercicio
            FF = snapshot.Child("FechaFin").Value.ToString();
            FI = snapshot.Child("FechaInicio").Value.ToString();
            fechaFinal = FF;
            fechaInicial = FI;
            //Guarda la fecha obtenida de firebase en un archivo "DateTime" con formato dia/mes/año
            DateTime.TryParseExact(fechaInicial, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha1);
            DateTime.TryParseExact(fechaFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha2);


        }
    }

    //Corrutina para enviar el valor del tiempo utilizado al firebase
    private IEnumerator Enviartiempo(string paciente, string tiempo, string _Token, string ejercicio)
    {
        //Establecer la muerte del usuario actualmente conectado
        var DBTask = DBreference.Child("users").Child(_Token).Child("Usuarios").Child(paciente).Child("Errores").Child(ejercicio).Child("Intento " + intentos).Child("Tiempo utilizado").SetValueAsync(tiempo);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }

    }
    //Corrutina para enviar las fallas que se obtubieron por ejercicio
    private IEnumerator Enviarfalals(string paciente, int fallas, string _Token, string ejercicio)
    {
        //Establecer las muertes del usuario actualmente conectado
        var DBTask = DBreference.Child("users").Child(_Token).Child("Usuarios").Child(paciente).Child("Errores").Child(ejercicio).Child("Intento "+ intentos).Child("Fallas").SetValueAsync(fallas);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }
    //Corrutia para enviar los aciertos que se obtuvieron por ejercicio 
    private IEnumerator Enviaraciertos(string paciente, int aciertos, string _Token, string ejercicio)
    {
        //Establecer las muertes del usuario actualmente conectado
        var DBTask = DBreference.Child("users").Child(_Token).Child("Usuarios").Child(paciente).Child("Errores").Child(ejercicio).Child("Intento " + intentos).Child("Aciertos").SetValueAsync(aciertos);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }
    //Corrutina para poder validar si el nombre de usuario ingresado es correcto y obtener el terapeuta asociado a ese usuario
    private IEnumerator SacarUsuarios(bool veri)
    {
        //Establecer las muertes del usuario actualmente conectado
        var DBTask = DBreference.Child("users").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        if (DBTask.Result.Value == null)
        {
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;
            int i = 0;
            //Se guarda en un vector la cantidad de terapeutas registrados en firebase
            numeroDocs = int.Parse(snapshot.ChildrenCount.ToString());
            //Vector donde se guardaran los token asociados a los terapeutas
            docs = new string[numeroDocs];
            //Foreach para recorrer todos lo terapeutas registrados 
            foreach (var us in snapshot.Children)
            {
                //Se guardan los token de los terapeutas en un vector 
                docs[i] = us.Key.ToString();
                //Foreach para buscar al usuario dentro de los teraputas 
                foreach (var item in us.Child("Usuarios").Children)
                {
                    //Si el nombre de usuario es igual a "item.key" (variable asociada a los usuaros por cada terapeuta)
                    //Quiere decir que el usuario si se encuentra registrado 
                    if (OnHit.nicks == item.Key)
                    {
                        //Variable para validar el inicio de sesión 
                        veri = true;
                        //Se guarda el token del terapeuta asociado al usaurio en la variable "Token"
                        Token = docs[i];
                        Debug.Log("Estas con la terapeuta: " + docs[i]);
                        break;
                    }
                }
                if (Verificacion == true)
                {
                    
                    Debug.Log("Se rompe el ciclo foreach con el true verificado ");
                    break;
                }
                i++;
            }
            //Condicion en caso de que el usuario no se encuentre registrado
            if (veri != true)
            {
                if (UserInvalido != null)
                {
                    //Se activa un mensaje por defecto que pedira ingresar un usuario correcto
                    UserInvalido.SetActive(true);
                }                
            }
            //Condicion en caso de que el usuario se encuentre registrado
            else if (veri == true)
            {
                if (UserInvalido != null)
                {
                    //Se desactiva el mensaje de usuario invalido si esta activo
                    UserInvalido.SetActive(false);
                    //Se activa un mensaje por defecto que indica qeu el usuario es correcto 
                    UserValido.SetActive(true);
                }
                //Se procese a hacer el incio de sesion y cargar los datos del usuario
                StartCoroutine(Login(emailLoginField, passwordLoginField));
                StartCoroutine(Saltos());
                otenerFecha();
            }
            yield return new WaitForSeconds(2);
        }
    }
    //Corrutina para obtener los ejerccios del usuairo
    private IEnumerator SacarEjercicio(string _Token,string paciente)
    {
        //Establecer las muertes del usuario actualmente conectado
        var DBTask = DBreference.Child("users").Child(_Token).Child("Usuarios").Child(paciente).Child("Ejercicio").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }

        if (DBTask.Result.Value == null)
        {

        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;
            int i = 0;
            //Se guarda el numero de ejercicios del usuario
            numeroE = int.Parse(snapshot.ChildrenCount.ToString());
            Eje = new string[numeroE];
            //Foreach para sacar el nombre de los ejercicios 
            foreach (var us in snapshot.Children)
            {
                Eje[i] = us.Key.ToString();
                Debug.Log("Ejercicios: " + Eje[i]);
                i++;
            }
        }
    }
    //Funcion para identificar si cumplio con la condicion de victoria para terminar el juego 
    public static void condicionVictoria()
    {
        //Se guardan las variables de los conteos de los objetos en un vector 
        Timer.conObjetos[0]  = Timer.Manzana;
        Timer.conObjetos[1]  = Timer.Banana;
        Timer.conObjetos[2]  = Timer.Leche;
        Timer.conObjetos[3]  = Timer.Cereal;
        Timer.conObjetos[4]  = Timer.Naranja;
        Timer.conObjetos[5]  = Timer.Soda;
        Timer.conObjetos[6]  = Timer.SalsaTomate;
        Timer.conObjetos[7]  = Timer.Crema;
        Timer.conObjetos[8]  = Timer.JuNaranja;
        Timer.conObjetos[9]  = Timer.Pera;
        Timer.conObjetos[10] = Timer.Pocky;
        Timer.conObjetos[11] = Timer.Rosquillas;
        Timer.conObjetos[12] = Timer.Uvas;
        Timer.conObjetos[13] = Timer.Agua;
        Timer.conObjetos[14] = Timer.SalsaRosada;
        Timer.conObjetos[15] = Timer.ManzanaVari;
        Timer.conObjetos[16] = Timer.BananaVari;
        Timer.conObjetos[17] = Timer.LecheVari;
        Timer.conObjetos[18] = Timer.CerealVari;
        Timer.conObjetos[19] = Timer.NaranjaVari;
        Timer.conObjetos[20] = Timer.SodaVari;
        Timer.conObjetos[21] = Timer.SalsaTomateVari;
        Timer.conObjetos[22] = Timer.CremaVari;
        Timer.conObjetos[23] = Timer.JuNaranjaVari;
        Timer.conObjetos[24] = Timer.PeraVari;
        Timer.conObjetos[25] = Timer.PockyVari;
        Timer.conObjetos[26] = Timer.RosquillasVari;
        Timer.conObjetos[27] = Timer.UvasVari;
        Timer.conObjetos[28] = Timer.AguaVari;
        Timer.conObjetos[29] = Timer.SalsaRosadaVari;
        //Se crea un vector del tamaño de la cantidad de retos que haya en el ejercicio 
        Timer.val = new int[mNombres.Length];
        //Se llena el vector con el conteo de los objetos correspondientes a las misiones
        for (int i = 0; i < mNombres.Length; i++)
        {
            Timer.val[i] = Timer.conObjetos[posiciones[i]];

        }

        for (int i = 0; i < mNombres.Length; i++)
        {
            //Se hace la evaluacion, si el conteo de objetos es igual al numero de objetos requerido por la mision
            if (Timer.val[i] == misiones[i])
            {
                //Se aumenta en 1 el valor de la variable conteo para poder contar las misiones que se va completando
                //Ademas de cambiar el nomrbe del objetode de la mision a "Completado" y
                //el numero de objetos a buscar a "-1" para que no sigan contado los aciertos de ese objeto
                Timer.conteo++;
                mNombres[i] = "Completado";
                misiones[i] = -1;

            }
        }
    }
    //Delay de 4 segundos con cambio de escena a "Perfil"
    IEnumerator Saltos()
    {
        yield return new WaitForSeconds(1);
        Cargador.cargarNivel("Perfil");
        //SceneManager.LoadScene("Perfil");
    }
    //Delay de 4 segundos con cambio de escena a ""Objetivos"
    IEnumerator Saltos1()
    {
        yield return new WaitForSeconds(1);
        Cargador.cargarNivel("Objetivos");
        //SceneManager.LoadScene("Objetivos");
    }
    //Delay de 3 segundos
    IEnumerator Saltos2()
    {
        yield return new WaitForSeconds(3);

    }


}
