using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using TMPro;
using System.Linq;


public class FirebaseManager : MonoBehaviour
{
    
    //Variables de firebase
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth; //Variable de autenticacion    
    public FirebaseUser User; //Variable de usuario (Terapeuta)
    public DatabaseReference DBreference; //Referencia a la base de datos
    

    //Variables del login 
    [Header("Login")]
    public TMP_InputField emailLoginField; //Asignacion de campo de ingreso de texto de email
    public TMP_InputField passwordLoginField; //Asignacion de campo de ingreso de texto de correo
    public TMP_Text warningLoginText; //Asignacion texto de aviso error o falla
    public TMP_Text confirmLoginText; //Asignacion texto de aviso confirmacion o correcto
    
    //Variables de registro
    [Header("Register")]
    public TMP_InputField usernameRegisterField; //Asignacion campo de ingreso de texto de username usado en registro
    public TMP_InputField emailRegisterField; //Asignacion campo de ingreso de texto de email usado en registro
    public TMP_InputField passwordRegisterField; //Asignacion campo de ingreso de texto de contraseña usado en registro
    public TMP_InputField passwordRegisterVerifyField; //Asignacion campo de ingreso de texto de confirmacion de contraseña usado en registro
    public TMP_Text warningRegisterText; //Asignacion campo de aviso de error en registro

    //User Data variables
    [Header("UserData")]
    public TMP_InputField usernameField;

    //Campos de texto usados en las interfaces, algunos se usan no interactuables para mostrar informacion
    //otros para llenar texto y cargar
    [Header("Espacios de texto")]
    public TMP_Text userVacio;
    public TMP_Text ubicacionM; //Texto input de ubicacion del monster
    public TMP_InputField tiempoField; //Texto input de ubicacion de asignacion de tiempo
    public TMP_InputField monsterField; //Variable ubicacion del monster
    public TMP_InputField usUsuario; //Usuario del paciente
    public TMP_InputField docUsuario; //Variable documento paciente
    public TMP_InputField nomUsuario; //Variable nombre paciente
    public TMP_InputField nomUsuarioEstante; //Variable 
    public TMP_InputField paciente; //Usuario del paciente buscado
    public TMP_InputField NombreDelEjercicio; //Nombre del ejercicio
    public TMP_InputField FechaInicioEjercicio; //Fecha inicio del ejercicio
    public TMP_InputField FechaFinEjercicio; //Fecha fin del ejercicio
    public TMP_InputField NombreEjercicioActual; //Nombre del ejercicio actual

    public  TMP_InputField [] NombresEjer = new TMP_InputField [7]; //Vector de campos de texto para los espacios de ejercicio
    public  TMP_InputField [] FechasIniEjer = new TMP_InputField [7]; //Vector de campos de texto para las fechas iniciales de los ejercicios
    public  TMP_InputField [] FechasFiniEjer = new TMP_InputField [7]; //Vector de campos de texto para las fechas finales de los ejercicios
    public TMP_InputField usCompare; // Comparador de usuario

    //Controladores, estos se encargan de llamar los scripts que hacen el manejo de los espacios de los estantes
    //Y funcionalidades de la interfaz de retos, interfaz de pacientes (Generacion de botones)
    [Header("Controladores")]

    [SerializeField()] private ControlLateralAmarillo controlLateralAmarillo; //Estante lateral amarillo
    [SerializeField()] private ControlFrontal controlFrontal; //Estante frontal 
    [SerializeField()] private ControlPosterior controlPosterior; //Estante posterior
    [SerializeField()] private ControlLateralRojo controlLateralRojo; //Estante lateral rojo 
    [SerializeField()] private ControlRetos controlRetos; //Control interfaz retos
    [SerializeField()] private CreacionUsuarios creacionUsuarios; //Botones de usuarios en la interfaz de pacientes 
    [SerializeField()] private ControlScroll controlScroll; //Scroll de botones
    [SerializeField()] private EjerciciosControl ejerciciosControl;  //Ejercicios de control

    // Interfaces del modulo de configuracion para los canvas usando GameObjects 
    //Cada GameObject representa una interfaz o una seccion de la interfaz que puede ser visible o no
    //Dependiendo el caso
    [Header("Screens")]
    [SerializeField] private GameObject Bienvenida; 
    [SerializeField] private GameObject UsuarioScreen;
    [SerializeField] private GameObject seleccionar;
    [SerializeField] private GameObject crear;
    [SerializeField] private GameObject seccionBotonesEstante;
    [SerializeField] private GameObject INFO;
    [SerializeField] private GameObject Reto;
    [SerializeField] private GameObject VerReto;
    [SerializeField] private GameObject VerEjercicio;
    [SerializeField] private GameObject EjercicioInicio;
    [SerializeField] private GameObject CrearEjercicioScreen;
    [SerializeField] private GameObject Saber;
    [SerializeField] private GameObject ScreenFrontal;
    [SerializeField] private GameObject ScreenPosterior;
    [SerializeField] private GameObject ScreenLateralAma;
    [SerializeField] private GameObject ScreenLateralRoj;
    [SerializeField] private GameObject screengeneral;
    [SerializeField] private GameObject screenBotones;

    //Variables para almacenar las cuadro y cantidad de la seccion de retos en ver retos
    [Header ("SeccionRetos")]
    [SerializeField] private TMP_InputField [] cuadro = new TMP_InputField [15]; 
    [SerializeField] private TMP_InputField [] cantidad = new TMP_InputField [15];

    //Boton Ok y guardar pacientes, el boton ok es usado en la interfaz de pacientes
    [Header ("Botones")]
    public Button ok;
    public Button GuardarPaciente;

    //Variables, vectores, campos de texto usadas en la logica de programacion y vectores
    [Header ("Cosas")]
    private string sinobjeto = "Sin objeto";
    public TMP_InputField numUsuarios;
    public string valor;
    [HideInInspector] public int c;
    [SerializeField] private GameObject content;
    [HideInInspector] public static string [] NombresUsuarios;
    [HideInInspector] public static string [] DocumentosUsuarios;
    [HideInInspector] public static string [] UsUsuarios;
    private int numEjercicio;
    private int numTerap;
    private int numPac;
    private string [] Terapeutas;
    private string [] NombresPcientes;
    public Text aviso;
    public Text aviso2;
    public Text Cargando;

    //El awake se ejecuta al abrir la aplicacion, mucho antes de el start 
    void Awake()
    {
        //Revisa todas las dependencias necesarias para firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //Si es valido, inicializa firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
        
        //Variables de uso global
        valor = "";
        numEjercicio = 0;
        c = 0;
        numTerap = 0;
        

    }

    //Inicializador de firebase
    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Establece la instancia o dato a usar para autenticar con firebase
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    //funcion que limpia los campos de texto de login
    public void ClearLoginFeilds()
    {
        emailLoginField.text = "";
        passwordLoginField.text = "";
    }

    //Funcion que limpia los campos de texto de registro
    public void ClearRegisterFeilds()
    {
        usernameRegisterField.text = "";
        emailRegisterField.text = "";
        passwordRegisterField.text = "";
        passwordRegisterVerifyField.text = "";
    }

    //Funcion para el boton de login
    public void LoginButton()
    {
        //Llama la corrutina de login para verificar si el correo y la contraseña son correctas
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
        
    }

    //Funcion publica para reestablecer contraseña
    public void RestablecerButton(){
        //Llama la corrutina de restabkecer la contraseña
        RestablecerContraseña(emailLoginField.text);
    }

    //Funcion del boton de registro
    public void RegisterButton()
    {
        //Llama la corrutina de registro con los datos de los campos de texto
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }

    //Funcion del boton cerrar sesion, la cual apaga las pantallas llevando a la de inicio
    //y finaliza limpiando los pacientes, y los campos de texto
    public void SignOutButton()
    {
        auth.SignOut(); //Autentica la salida
        UIManager.instance.LoginScreen();
        LimpiarBotones();
        nomUsuario.text = "";
        usUsuario.text = "";
        docUsuario.text = "";
        Bienvenida.SetActive(true);
        UsuarioScreen.SetActive(false);
        seccionBotonesEstante.SetActive(false);
        Reto.SetActive(false);
        VerReto.SetActive(false);
        VerEjercicio.SetActive(false);
        EjercicioInicio.SetActive(false);
        CrearEjercicioScreen.SetActive(false);
        Saber.SetActive(false);
        ScreenFrontal.SetActive(false);
        ScreenPosterior.SetActive(false);
        ScreenLateralAma.SetActive(false);
        ScreenLateralRoj.SetActive(false);
        screengeneral.SetActive(false);
        screenBotones.SetActive(false);
        ClearRegisterFeilds();
        ClearLoginFeilds();

    }

    //Funcion del boton de guardar, ejecuta las corrutinas que actualizan en la base de datos toda la informacion de data
    public void SaveDataButton()
    {
        StartCoroutine(UpdateUsernameAuth(usernameField.text));
        StartCoroutine(UpdateUsernameDatabase(usernameField.text));
        
    }

    //Esta funcion extrae la informacion de los campos de texto para cargarlas en la base de datos, esta funcion
    //Se encarga de actualizar usando dos corrutinas, una updateEstante que es para los espacios del estante 
    //y updateSandIpaciente para el tiempo y la ubicacion del monster
    public void SaveDataPaciente(){

        StartCoroutine(UpdateSandIPaciente("Tiempo",int.Parse(tiempoField.text)));
        StartCoroutine(UpdateSandIPaciente("Monster",int.Parse(monsterField.text)));
        StartCoroutine(UpdateEstante(controlFrontal.entradasF[0].text, "estanteFrontal1"));
        StartCoroutine(UpdateEstante(controlFrontal.entradasF[1].text, "estanteFrontal2"));
        StartCoroutine(UpdateEstante(controlFrontal.entradasF[2].text, "estanteFrontal3"));
        StartCoroutine(UpdateEstante(controlFrontal.entradasF[3].text, "estanteFrontal4"));
        StartCoroutine(UpdateEstante(controlFrontal.entradasF[4].text, "estanteFrontal5"));
        StartCoroutine(UpdateEstante(controlFrontal.entradasF[5].text, "estanteFrontal6"));
        StartCoroutine(UpdateEstante(controlFrontal.entradasF[6].text, "estanteFrontal7"));
        StartCoroutine(UpdateEstante(controlFrontal.entradasF[7].text, "estanteFrontal8"));
        StartCoroutine(UpdateEstante(controlFrontal.entradasF[8].text, "estanteFrontal9"));
        StartCoroutine(UpdateEstante(controlFrontal.entradasF[9].text, "estanteFrontal10"));
        StartCoroutine(UpdateEstante(controlFrontal.entradasF[10].text, "estanteFrontal11"));
        StartCoroutine(UpdateEstante(controlFrontal.entradasF[11].text, "estanteFrontal12"));
        StartCoroutine(UpdateEstante(controlLateralAmarillo.entradas[0].text, "estantelateralAmarillo1"));
        StartCoroutine(UpdateEstante(controlLateralAmarillo.entradas[1].text, "estantelateralAmarillo2"));
        StartCoroutine(UpdateEstante(controlLateralAmarillo.entradas[2].text, "estantelateralAmarillo3"));
        StartCoroutine(UpdateEstante(controlLateralAmarillo.entradas[3].text, "estantelateralAmarillo4"));
        StartCoroutine(UpdateEstante(controlLateralAmarillo.entradas[4].text, "estantelateralAmarillo5"));
        StartCoroutine(UpdateEstante(controlLateralAmarillo.entradas[5].text, "estantelateralAmarillo6"));
        StartCoroutine(UpdateEstante(controlLateralAmarillo.entradas[6].text, "estantelateralAmarillo7"));
        StartCoroutine(UpdateEstante(controlLateralAmarillo.entradas[7].text, "estantelateralAmarillo8"));
        StartCoroutine(UpdateEstante(controlPosterior.entradasP[0].text, "estantePosterior1"));
        StartCoroutine(UpdateEstante(controlPosterior.entradasP[1].text, "estantePosterior2"));
        StartCoroutine(UpdateEstante(controlPosterior.entradasP[2].text, "estantePosterior3"));
        StartCoroutine(UpdateEstante(controlPosterior.entradasP[3].text, "estantePosterior4"));
        StartCoroutine(UpdateEstante(controlPosterior.entradasP[4].text, "estantePosterior5"));
        StartCoroutine(UpdateEstante(controlPosterior.entradasP[5].text, "estantePosterior6"));
        StartCoroutine(UpdateEstante(controlPosterior.entradasP[6].text, "estantePosterior7"));
        StartCoroutine(UpdateEstante(controlPosterior.entradasP[7].text, "estantePosterior8"));
        StartCoroutine(UpdateEstante(controlPosterior.entradasP[8].text, "estantePosterior9"));
        StartCoroutine(UpdateEstante(controlPosterior.entradasP[9].text, "estantePosterior10"));
        StartCoroutine(UpdateEstante(controlPosterior.entradasP[10].text, "estantePosterior11"));
        StartCoroutine(UpdateEstante(controlPosterior.entradasP[11].text, "estantePosterior12"));
        StartCoroutine(UpdateEstante(controlLateralRojo.entradasR[0].text, "estantelateralRojo1"));
        StartCoroutine(UpdateEstante(controlLateralRojo.entradasR[1].text, "estantelateralRojo2"));
        StartCoroutine(UpdateEstante(controlLateralRojo.entradasR[2].text, "estantelateralRojo3"));
        StartCoroutine(UpdateEstante(controlLateralRojo.entradasR[3].text, "estantelateralRojo4"));
        StartCoroutine(UpdateEstante(controlLateralRojo.entradasR[4].text, "estantelateralRojo5"));
        StartCoroutine(UpdateEstante(controlLateralRojo.entradasR[5].text, "estantelateralRojo6"));
        

    }

    //Funcion que ejecuta la corrutina de actualizar el reto (Lo guarda)
    public void SaveDataReto(){
        StartCoroutine(UpdateReto());
    }

    //Funcion que ejecuta las corrutinas de actualizar informacion de paciente y llama al script de
    //Creacion de usuarios para actualizar en la interfaz de pacientes
    public void SaveDataUser(){
        creacionUsuarios.ExtraerDatos();
        StartCoroutine(UpdateUsers1());
        StartCoroutine(UpdateUsers2());
        creacionUsuarios.Limpiar();
        Debug.Log("Se ha creado un nuevo usuario");
    }

    //Actualiza la cantidad de usuarios
    public void CrearUsuario(){
        //controlScroll.mas();
        StartCoroutine(UpdateSandI("numUsuarios",int.Parse(numUsuarios.text)));
    }

    //Ejecuta las corrutinas para borrar los pacientes
    public void BotonBorrarUsuarios(){
        StartCoroutine(BorrarUsers());
        StartCoroutine(numAcero());
    }

    //Ejecuta la corrutina para actualizacion de datos en la interfaz
    public void establecerNum(){
        // StartCoroutine(cargarYcambiarValor()); ---> Esto se esta haciendo a cada rato en el update
        /*int c;
        int.TryParse(valor, out c);
        int b = c + 1;
        StartCoroutine(SetearNuevoUser(b));
        controlScroll.LoadBotones(b);*/

        
        c = int.Parse(numUsuarios.text) + 1;
        StartCoroutine(SetearNuevoUser(c));
        StartCoroutine(cargarYcambiarValor());
    }

    //Ejecuta la corrutina para actualizacion de datos en la interfaz
    public void establecerNum2(){

        
        c = int.Parse(numUsuarios.text);
        StartCoroutine(SetearNuevoUser(c));
        controlScroll.LoadBotones(c);
    }

    //Ejecuta la corrutina para actualizacion de datos en la interfaz
    public void establecerNum3(){

        
        c = int.Parse(numUsuarios.text) - 1;
        StartCoroutine(SetearNuevoUser(c));
        StartCoroutine(cargarYcambiarValor());
    }

    //Muestra los usuarios
    public void MostrarUsuarios(){
        //StartCoroutine(cargarYcambiarValor());
        StartCoroutine(sacarUsuarios());

    }

    //Funcion para mostrar datos seteandolos
    public void DobleMostrar(){
        
        MostrarUsuarios();
        establecerNum2();

        StartCoroutine(Esperar());  
    }

    //Guarda nuevo usuario
    public void GuardarNuevoUsuario(){


        StartCoroutine(Esperar2());
      
    }

    //Vuelve a la iterfaz
    public void VolverUsuario(){
        StartCoroutine(Esperar3());
    }

    //Cambia el valor en consola para actualizar
    public void cargar(){
        
        StartCoroutine(cargarYcambiarValor());
    }

    //Llama al script para cargar botones en la interfaz de pacientes
    public void BotonesUs(){
        
        controlScroll.LoadBotones(int.Parse(numUsuarios.text));
    }

    //Limpia los botones de pacientes en la interfaz de pacientes
    public void LimpiarBotones(){

        if(content.transform.childCount > 0){
            Debug.Log("Limpiando los botones");
            while (content.transform.childCount > 0){
                Transform child = content.transform.GetChild(0);
                //child.parent = null; // set parent -- child.setParent(null)
                child.SetParent(null);
                Destroy(child.gameObject);
            }
        }
    }

    //Ejecuta una corrutina de espera y ejecuta las funciones para actualizacion de datos
    private IEnumerator Esperar2(){


        CrearUsuario();

        establecerNum();

        cargar();
   
        MostrarUsuarios();

        SaveDataUser();

        yield return new WaitForSeconds(10);

        
        BotonesUs();

        StartCoroutine(lapso2());

    }

    //Ejecuta una corrutina de espera y ejecuta las funciones para actualizacion de datos
    private IEnumerator Esperar3(){

        CrearUsuario();

        establecerNum2();

        cargar();
   
        MostrarUsuarios();

        yield return new WaitForSeconds(10);

        BotonesUs();

        StartCoroutine(lapso2());

    }

    // Corrutina de Login usada en el boton de login
    private IEnumerator Login(string _email, string _password)
    {
        
        //Llama la autenticacion de firebase
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Espera hasta que las tareas se ejecuten
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //Si hay un error
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "¡Login fallido!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Falta el correo";
                    break;
                case AuthError.MissingPassword:
                    message = "Falta la contraseña";
                    break;
                case AuthError.WrongPassword:
                    message = "Contraseña erronea";
                    break;
                case AuthError.InvalidEmail:
                    message = "Correo invalido";
                    break;
                case AuthError.UserNotFound:
                    message = "Cuenta no existe";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //La informacion fue correcta
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Logeado";
            StartCoroutine(LoadUserData());
            StartCoroutine(cargarYcambiarValor());
            
            

            yield return new WaitForSeconds(3);

            usernameField.text = User.DisplayName;
            userVacio.text = User.DisplayName;
            ubicacionM.text = monsterField.text;
            UIManager.instance.UserDataScreen(); // Change to user data UI
            confirmLoginText.text = "";
            ClearLoginFeilds();
            ClearRegisterFeilds();
            
        }
    }

    //Funcion para reestablecer la contraseña que ejecuta el boton de reestablecer en la interfaz incial 
    private void RestablecerContraseña(string email){
        auth.SendPasswordResetEmailAsync(email).ContinueWith(task => { //Usando la autenticacion y la funcion de firebase
            if (task.IsCanceled){
                Debug.LogError("El restablecimiento fue cancelado");
                return;
            }
            if (task.IsFaulted){
                Debug.LogError("El restablecimiento tuvo un error");
                return;
            }

            Debug.LogError("Restablecimiento completado");

        });
    }

    //Corrutina de registro usando datos de entrada en los campos de texto
    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            //Aviso si el campo de texto esta vacio
            warningRegisterText.text = "Falta el nombre de usuario";
        }
        else if(passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //Si las contraseñas no coinciden al momento de confirmar
            warningRegisterText.text = "La contraseña no coincide";
        }
        else 
        {
            //Llama a firebase para crear y autenticar
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Espera hasta que las tareas sean completadas
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //Si hay algun error, avisa cual es el fallo
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "¡Registro fallido!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Falta el correo";
                        break;
                    case AuthError.MissingPassword:
                        message = "Falta la contraseña";
                        break;
                    case AuthError.WeakPassword:
                        message = "Contraseña debil";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "El correo actualmente esta en uso";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //Cuando los datos son correcto
                //Now get the result
                User = RegisterTask.Result;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile{DisplayName = _username};

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen
                        UIManager.instance.LoginScreen();
                        warningRegisterText.text = "";
                        ClearLoginFeilds();
                        ClearRegisterFeilds();
                    }
                }
            }
        }
    }

    //Actualiza el usuario
    private IEnumerator UpdateUsernameAuth(string _username)
    {
        //Create a user profile and set the username
        UserProfile profile = new UserProfile { DisplayName = _username };

        //Call the Firebase auth update user profile function passing the profile with the username
        var ProfileTask = User.UpdateUserProfileAsync(profile);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if (ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        }
        else
        {
            //Auth username is now updated
        }        
    }

    //Establece el usuario del terapeuta en la base de datos
    private IEnumerator UpdateUsernameDatabase(string _username)
    {
        //Set the currently logged in user username in the database
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("username").SetValueAsync(_username);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }

    //Actualiza usando un string y un int para establecer la cantidad de pacientes o cualquiera en la jerarquia
    private IEnumerator UpdateSandI(string text,int par)
    {
        //Set the currently logged in user deaths
        var DBTask = DBreference.Child("users").Child(User.UserId).Child(text).SetValueAsync(par);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            
        }
    }

    //Se usa para actualizar el tiempo, el monster y demas
    private IEnumerator UpdateSandIPaciente(string text,int par)
    {
        //Set the currently logged in user deaths
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Usuarios").Child(paciente.text).Child("Ejercicio").Child(NombreEjercicioActual.text).Child("Estante").Child(text).SetValueAsync(par);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            
        }
    }

    //Espacios estantes actualizandolos en la base de datos, el espacio del estante y el objeto
    private IEnumerator UpdateEstante(string objeto, string ubicacion)
    {
        //Set the currently logged in user deaths
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Usuarios").Child(paciente.text).Child("Ejercicio").Child(NombreEjercicioActual.text).Child("Estante").Child(ubicacion).SetValueAsync(objeto);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            
        }
    }

    //Actualiza el reto agregando el objeto y la cantidad a la base de datos en la seccion de reto
    private IEnumerator UpdateReto()
    {
        string objeto;
        string cantidad;

        objeto = controlRetos.espacioObjeto.text;
        cantidad = controlRetos.espacioCantidad.text;
        
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Usuarios").Child(paciente.text).Child("Ejercicio").Child(NombreEjercicioActual.text).Child("Reto").Child(objeto).SetValueAsync(cantidad);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
                    
        }
        
    }

    //Corrutina para una espera y cambio de interfaz
    private IEnumerator Esperar(){
        yield return new WaitForSeconds (1);

        Bienvenida.SetActive(false);
        UsuarioScreen.SetActive(true);
    }

    //Corrutina de actualizacion de campos en la interfaz de pacientes
    private IEnumerator UpdateUsers1()
    {

        var DBTask = DBreference.Child("users").Child(User.UserId).
        Child("Usuarios").Child(creacionUsuarios.textUsuario).Child("Nombre Completo").
        SetValueAsync(creacionUsuarios.textNombre);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
                    
        }
    }

    //Corrutina de actualizacion de campos en la interfaz de pacientes
    private IEnumerator UpdateUsers2()
    {

        var DBTask = DBreference.Child("users").Child(User.UserId).
        Child("Usuarios").Child(creacionUsuarios.textUsuario).Child("Documento").
        SetValueAsync(creacionUsuarios.TI);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
                    
        }

        
    }
    
    //Carga toda la informacion del terapeuta (incluye los que hay debajo de la gerarquia)
    private IEnumerator LoadUserData()
    {
        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }

        if (DBTask.Result.Value == null)
        {
            numUsuarios.text = "0";

        }
        else
        {
            
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            
            numUsuarios.text = snapshot.Child("numUsuarios").Value.ToString();

        }
    }

    //Borra los pacientes
    private IEnumerator BorrarUsers(){

        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Usuarios").RemoveValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            
        }
    }

    //Borra todos los numeros de pacientes
    private IEnumerator numAcero(){

        var DBTask = DBreference.Child("users").Child(User.UserId).Child("numUsuarios").SetValueAsync(0);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            
        }

    }

    //Establece una nueva cantidad de pacientes
    private IEnumerator SetearNuevoUser(int num){


        var DBTask = DBreference.Child("users").Child(User.UserId).Child("numUsuarios").SetValueAsync(num);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            
        }

    }

    //Corrutina de extraccion y cambio de valor
    private IEnumerator cargarYcambiarValor(){


        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            DataSnapshot sanpping = DBTask.Result;
            
            valor = sanpping.Child("numUsuarios").Value.ToString(); 
            numUsuarios.text = valor; //sanpping.Child("numUsuarios").Value.ToString();

            
            
        }

    }

    //Extrae la informacion del paciente
    private IEnumerator sacarUsuarios(){

        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Usuarios").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            DataSnapshot sanpping = DBTask.Result; // us1, us2, us3 ----> usuario - key = us1, value (nombre, docum)
            int i = 0;
            int k = int.Parse(numUsuarios.text);
            NombresUsuarios = new string [k];
            DocumentosUsuarios = new string [k];
            UsUsuarios = new string [k];
            foreach (var us in sanpping.Children)
            {
                string nameUs;
                string doc;
                string neim;

                nameUs = us.Key.ToString();
                UsUsuarios [i]= nameUs;
                
                
                neim = "";
                doc = "";
                
                foreach (var nom in us.Children)
                {
                    if(nom.Key == "Nombre Completo")
                    {
                        neim = nom.Value.ToString();
                        NombresUsuarios [i]= neim;
                        
                    }
                    if(nom.Key == "Documento"){
                        doc = nom.Value.ToString();
                        DocumentosUsuarios [i] = doc;
                        
                    }
                    
                }
                //Debug.Log("El nombre del usuario " + i + " es: " + nameUs + " su nombre completo " +neim + 
                //" con dcoumento" + doc);
                i++;
  
            }
            
        }

    }

    //Carga toda la informacion del paciente y sus ejercicos asociados, para verla en el modulo
    private IEnumerator LoadPacienteData(){

        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Usuarios").Child(paciente.text).Child("Ejercicio").Child(NombreEjercicioActual.text).Child("Estante").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }

        if (DBTask.Result.Value == null)
        {
            //No data exists yet
            tiempoField.text = "0";
            //killsField.text = "0";
            monsterField.text = "1";
            controlFrontal.entradasF[0].text = sinobjeto;
            controlFrontal.entradasF[1].text = sinobjeto;
            controlFrontal.entradasF[2].text = sinobjeto;
            controlFrontal.entradasF[3].text = sinobjeto;
            controlFrontal.entradasF[4].text = sinobjeto;
            controlFrontal.entradasF[5].text = sinobjeto;
            controlFrontal.entradasF[6].text = sinobjeto;
            controlFrontal.entradasF[7].text = sinobjeto;
            controlFrontal.entradasF[8].text = sinobjeto;
            controlFrontal.entradasF[9].text = sinobjeto;
            controlFrontal.entradasF[10].text = sinobjeto;
            controlFrontal.entradasF[11].text = sinobjeto;
            controlLateralAmarillo.entradas[0].text = sinobjeto;
            controlLateralAmarillo.entradas[1].text = sinobjeto;
            controlLateralAmarillo.entradas[2].text = sinobjeto;
            controlLateralAmarillo.entradas[3].text = sinobjeto;
            controlLateralAmarillo.entradas[4].text = sinobjeto;
            controlLateralAmarillo.entradas[5].text = sinobjeto;
            controlLateralAmarillo.entradas[6].text = sinobjeto;
            controlLateralAmarillo.entradas[7].text = sinobjeto;
            controlPosterior.entradasP[0].text = sinobjeto;
            controlPosterior.entradasP[1].text = sinobjeto;
            controlPosterior.entradasP[2].text = sinobjeto;
            controlPosterior.entradasP[3].text = sinobjeto;
            controlPosterior.entradasP[4].text = sinobjeto;
            controlPosterior.entradasP[5].text = sinobjeto;
            controlPosterior.entradasP[6].text = sinobjeto;
            controlPosterior.entradasP[7].text = sinobjeto;
            controlPosterior.entradasP[8].text = sinobjeto;
            controlPosterior.entradasP[9].text = sinobjeto;
            controlPosterior.entradasP[10].text = sinobjeto;
            controlPosterior.entradasP[11].text = sinobjeto;
            controlLateralRojo.entradasR[0].text = sinobjeto;
            controlLateralRojo.entradasR[1].text = sinobjeto;
            controlLateralRojo.entradasR[2].text = sinobjeto;
            controlLateralRojo.entradasR[3].text = sinobjeto;
            controlLateralRojo.entradasR[4].text = sinobjeto;
            controlLateralRojo.entradasR[5].text = sinobjeto;

            
            controlFrontal.CargarImagenBoton();
            controlPosterior.CargarImagenBoton();
            controlLateralAmarillo.CargarImagenBoton();
            controlLateralRojo.CargarImagenBoton();
            

            

            

        }
        else
        {
            
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            tiempoField.text = snapshot.Child("Tiempo").Value.ToString();
            //killsField.text = snapshot.Child("kills").Value.ToString();
            monsterField.text = snapshot.Child("Monster").Value.ToString();
            
            controlFrontal.entradasF[0].text = snapshot.Child("estanteFrontal1").Value.ToString();
            controlFrontal.entradasF[1].text = snapshot.Child("estanteFrontal2").Value.ToString();
            controlFrontal.entradasF[2].text = snapshot.Child("estanteFrontal3").Value.ToString();
            controlFrontal.entradasF[3].text = snapshot.Child("estanteFrontal4").Value.ToString();
            controlFrontal.entradasF[4].text = snapshot.Child("estanteFrontal5").Value.ToString();
            controlFrontal.entradasF[5].text = snapshot.Child("estanteFrontal6").Value.ToString();
            controlFrontal.entradasF[6].text = snapshot.Child("estanteFrontal7").Value.ToString();
            controlFrontal.entradasF[7].text = snapshot.Child("estanteFrontal8").Value.ToString();
            controlFrontal.entradasF[8].text = snapshot.Child("estanteFrontal9").Value.ToString();
            controlFrontal.entradasF[9].text = snapshot.Child("estanteFrontal10").Value.ToString();
            controlFrontal.entradasF[10].text = snapshot.Child("estanteFrontal11").Value.ToString();
            controlFrontal.entradasF[11].text = snapshot.Child("estanteFrontal12").Value.ToString();
            controlLateralAmarillo.entradas[0].text = snapshot.Child("estantelateralAmarillo1").Value.ToString();
            controlLateralAmarillo.entradas[1].text = snapshot.Child("estantelateralAmarillo2").Value.ToString();
            controlLateralAmarillo.entradas[2].text = snapshot.Child("estantelateralAmarillo3").Value.ToString();
            controlLateralAmarillo.entradas[3].text = snapshot.Child("estantelateralAmarillo4").Value.ToString();
            controlLateralAmarillo.entradas[4].text = snapshot.Child("estantelateralAmarillo5").Value.ToString();
            controlLateralAmarillo.entradas[5].text = snapshot.Child("estantelateralAmarillo6").Value.ToString();
            controlLateralAmarillo.entradas[6].text = snapshot.Child("estantelateralAmarillo7").Value.ToString();
            controlLateralAmarillo.entradas[7].text = snapshot.Child("estantelateralAmarillo8").Value.ToString();
            controlPosterior.entradasP[0].text = snapshot.Child("estantePosterior1").Value.ToString();
            controlPosterior.entradasP[1].text = snapshot.Child("estantePosterior2").Value.ToString();
            controlPosterior.entradasP[2].text = snapshot.Child("estantePosterior3").Value.ToString();
            controlPosterior.entradasP[3].text = snapshot.Child("estantePosterior4").Value.ToString();
            controlPosterior.entradasP[4].text = snapshot.Child("estantePosterior5").Value.ToString();
            controlPosterior.entradasP[5].text = snapshot.Child("estantePosterior6").Value.ToString();
            controlPosterior.entradasP[6].text = snapshot.Child("estantePosterior7").Value.ToString();
            controlPosterior.entradasP[7].text = snapshot.Child("estantePosterior8").Value.ToString();
            controlPosterior.entradasP[8].text = snapshot.Child("estantePosterior9").Value.ToString();
            controlPosterior.entradasP[9].text = snapshot.Child("estantePosterior10").Value.ToString();
            controlPosterior.entradasP[10].text = snapshot.Child("estantePosterior11").Value.ToString();
            controlPosterior.entradasP[11].text = snapshot.Child("estantePosterior12").Value.ToString();
            controlLateralRojo.entradasR[0].text = snapshot.Child("estantelateralRojo1").Value.ToString();
            controlLateralRojo.entradasR[1].text = snapshot.Child("estantelateralRojo2").Value.ToString();
            controlLateralRojo.entradasR[2].text = snapshot.Child("estantelateralRojo3").Value.ToString();
            controlLateralRojo.entradasR[3].text = snapshot.Child("estantelateralRojo4").Value.ToString();
            controlLateralRojo.entradasR[4].text = snapshot.Child("estantelateralRojo5").Value.ToString();
            controlLateralRojo.entradasR[5].text = snapshot.Child("estantelateralRojo6").Value.ToString();

            controlFrontal.CargarImagenBoton();
            controlPosterior.CargarImagenBoton();
            controlLateralAmarillo.CargarImagenBoton();
            controlLateralRojo.CargarImagenBoton();

            


        }

    }

    //Cambia la interfaz y las muestra
    public void IrAPacienteEstante(){

    
        
        nomUsuarioEstante.text = nomUsuario.text;
        NombreEjercicioActual.text = "";
        UsuarioScreen.SetActive(false);
        EjercicioInicio.SetActive(true);
        Saber.SetActive(true);
        

        

    }

    //Funcion que ejecuta una corrutina de espera y cambio de interfaz
    public void IrAConfig(){
        StartCoroutine(EsperarPaciente());
    }

    //Corrutina de espera y de cambio de interfaz
    private IEnumerator EsperarPaciente(){

        StartCoroutine(LoadPacienteData());

        yield return new WaitForSeconds(5);

        //UsuarioScreen.SetActive(false);
        seccionBotonesEstante.SetActive(true);
        EjercicioInicio.SetActive(false);
        Saber.SetActive(true);
        

    }

    //Hace que los campos de texto en la visualizacion del paciente sean interactuables 
    public void editarInteraccion(){
        nomUsuario.interactable = true;
        docUsuario.interactable = true;
        ok.interactable = true;


    }

    //Usa la corrutina para actualiar el nombre y el documento
    public void ActualizarUsuario(){


        StartCoroutine(ActualizarNombre());
        StartCoroutine(ActualizarDocumento());

        nomUsuario.interactable = false;
        docUsuario.interactable = false;
        ok.interactable = false;

        
        StartCoroutine(lapso());

    }

    //Funcion que ejecuta las corrutinas de eliminacion y actualizacion de pacientes
    public void EliminarUsuario(){
        
        controlScroll.refresh();
        StartCoroutine(BorrarEsteUser());
        StartCoroutine(lapso3());
        


    }

    //Corrutina de actualizacion de nombre del paciente
    private IEnumerator ActualizarNombre(){

        var DBTask = DBreference.Child("users").Child(User.UserId).
        Child("Usuarios").Child(usUsuario.text).Child("Nombre Completo").
        SetValueAsync(nomUsuario.text);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
                    
        }

    }

    //Corrutina de actualizacion de documento del paciente
    private IEnumerator ActualizarDocumento(){

        var DBTask = DBreference.Child("users").Child(User.UserId).
        Child("Usuarios").Child(usUsuario.text).Child("Documento").
        SetValueAsync(docUsuario.text);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
                    
        }

    }

    //Corrutina de espera de tiempo y ejecucion de funciones
    private IEnumerator lapso(){

        LimpiarBotones();

        MostrarUsuarios();

        nomUsuario.text = "";
        usUsuario.text = "";
        docUsuario.text = "";


        yield return new WaitForSeconds(5);

        BotonesUs();

    }

    //Corrutina de espera de tiempo y ejecucion de funciones
    private IEnumerator lapso2(){

        LimpiarBotones();

        MostrarUsuarios(); 

        nomUsuario.text = "";
        usUsuario.text = "";
        docUsuario.text = "";

        yield return new WaitForSeconds(5);

        seleccionar.SetActive(true);
        crear.SetActive(false);
        INFO.SetActive(true);

        BotonesUs();

        

    }

    //Corrutina de espera de tiempo y ejecucion de funciones
    private IEnumerator lapso3(){

        establecerNum3();

        LimpiarBotones();

        MostrarUsuarios();

        nomUsuario.text = "";
        usUsuario.text = "";
        docUsuario.text = "";

        INFO.SetActive(false);

        yield return new WaitForSeconds(5);

        INFO.SetActive(true);

        

        BotonesUs();

    }

   //Corrutina de eliminacion de paciente de la base de datos
    private IEnumerator BorrarEsteUser(){

        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Usuarios").Child(usUsuario.text).RemoveValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            
        }
    }

    //Corrutina de verificacion de la disponibilidad del usuario del paciente
    private IEnumerator verificador(){

        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Usuarios").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            DataSnapshot sanpping = DBTask.Result; // us1, us2, us3 ----> usuario - key = us1, value (nombre, docum)
            int i = 0;
            foreach (var us in sanpping.Children)
            {

                i++;
  
            }
            
        }


    }

    //Muestra la interfaz de retos y ejecuta la corrutina de carga de retos
    public void MostrarRetos(){
        StartCoroutine(MostarElReto());
        Reto.SetActive(false);
        VerReto.SetActive(true);

    }

    //Elimina los retos o objetivos de la base de datos
    public void DelateReto(){
        StartCoroutine(BorrarReto());
        LimpiarRetos();
        Reto.SetActive(true);
        VerReto.SetActive(false);
    }

    //Corrutina de carga y visualizacion de reto 
    private IEnumerator MostarElReto()
    {

        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Usuarios").Child(paciente.text)
        .Child("Ejercicio").Child(NombreEjercicioActual.text).Child("Reto").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            DataSnapshot snap = DBTask.Result; 
            int i = 0;

            foreach (var us in snap.Children)
            {
                string objeto;
                string canti;
                
                objeto = us.Key.ToString();
                canti = us.Value.ToString();

                cuadro[i].text = objeto;
                cantidad[i].text = canti;

                
                i ++;
                
                
  
            }
            
        }
        
    }

    //Limpia los retos en las cajas de visualizacion de retos
    public void LimpiarRetos(){

        for (int i = 0; i < 15; i++)
        {
            cuadro[i].text = "";
            cantidad[i].text = "";

        }
    }

    //Corrutina de eliminacion del objetivo o reto
    private IEnumerator BorrarReto(){

        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Usuarios").Child(paciente.text)
        .Child("Ejercicio").Child(NombreEjercicioActual.text).Child("Reto").RemoveValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            
        }

    }

    //Funcion de verifiacion de llenado de formulario para la creacion de ejercicio
    public void CrearEjercicioBoton(){

        if (NombreDelEjercicio.text == "")
        {
            Debug.Log("Le falta el nombre");
        }
        else if(FechaInicioEjercicio.text == "")
        {
            Debug.Log("Le falta la fecha de inicio");
        }
        else if(FechaFinEjercicio.text == ""){
            Debug.Log("Le falta la fecha final");
        }
        else{

            StartCoroutine(CrearEjercicioInicio(NombreDelEjercicio.text,FechaInicioEjercicio.text));
            StartCoroutine(CrearEjercicioFinal(NombreDelEjercicio.text, FechaFinEjercicio.text));
            StartCoroutine(esperarEjer());
            

        }
        
    }

    //Corrutina de espera para la actualizacion de los ejercicios
    private IEnumerator esperarEjer(){
        NombreEjercicioActual.text = NombreDelEjercicio.text;
        yield return new WaitForSeconds(2);
        EjercicioInicio.SetActive(true);
        CrearEjercicioScreen.SetActive(false);
        NombreDelEjercicio.text = "";
        FechaInicioEjercicio.text = "";
        FechaFinEjercicio.text = "";

    }

    //Funcion de cambio de pantalla
    public void IrACrearEjercicio(){

        EjercicioInicio.SetActive(false);
        CrearEjercicioScreen.SetActive(true);

    }

    //Corrutina de creacion de la fecha de inicio del ejercicio en la base de datos
    private IEnumerator CrearEjercicioInicio(string nomEjercicio, string FechaIni){

        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Usuarios").Child(paciente.text)
        .Child("Ejercicio").Child(nomEjercicio).Child("FechaInicio").SetValueAsync(FechaIni);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
                      
        }

    }

    //Corrutina de creacion de la fecha de fin del ejercicio en la base de datos
    private IEnumerator CrearEjercicioFinal(string nomEjercicio, string FechaFini){

        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Usuarios").Child(paciente.text)
        .Child("Ejercicio").Child(nomEjercicio).Child("FechaFin").SetValueAsync(FechaFini);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
                      
        }

    }

    //Corrutina que cuenta la cantidad de ejercicios del paciente
    private IEnumerator contadorEjercicios(){

        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Usuarios").Child(paciente.text)
        .Child("Ejercicio").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {     
              DataSnapshot snapig = DBTask.Result; 
              numEjercicio = int.Parse(snapig.ChildrenCount.ToString());

              
        }

    }

    //Funcion cuando presiona un ejercicio 
    public void OprimirSeleccionarEjer(){

        NombreEjercicioActual.text = NombresEjer[ejerciciosControl.dato].text;
        
        VerEjercicio.SetActive(false);
        EjercicioInicio.SetActive(true);

    }

    //Funcion cuando presiona el boton de editar ejercicio
    public void OprimirEditarEjer(){
        StartCoroutine(EditarEjercicioI(NombresEjer[ejerciciosControl.dato].text,FechasIniEjer[ejerciciosControl.dato].text));
        StartCoroutine(EditarEjercicioF(NombresEjer[ejerciciosControl.dato].text,FechasFiniEjer[ejerciciosControl.dato].text));
        VerEjercicio.SetActive(false);
        EjercicioInicio.SetActive(true);
    }

    //Funcion que ejecuta cuando quiere ver el ejercicio
    public void OprimirVerEjercicios(){
        StartCoroutine(MostarEjercicios());
        VerEjercicio.SetActive(true);
        EjercicioInicio.SetActive(false);
    }

    //Funciona que se ejecuta cuando oprime eliminar el ejercicio
    public void OprimirEliminarEjer(){
        
        StartCoroutine(BorrarEjercicio(NombresEjer[ejerciciosControl.dato].text));
        VerEjercicio.SetActive(false);
        EjercicioInicio.SetActive(true);
    }

    //corrutina que carga los ejercicios para mostrarlos en la interfaz de ver ejercicio
    private IEnumerator MostarEjercicios()
    {

        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Usuarios").Child(paciente.text)
        .Child("Ejercicio").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            DataSnapshot snap = DBTask.Result; 
            int i = 0;

            foreach (var us in snap.Children)
            {
                string nomi;
                
                
                nomi = us.Key.ToString();

                NombresEjer[i].text = nomi;

                foreach (var item in us.Children)
                {
                    if(item.Key == "FechaInicio"){
                        FechasIniEjer[i].text = item.Value.ToString();
                    }
                    if(item.Key == "FechaFin"){
                        FechasFiniEjer[i].text = item.Value.ToString();
                    }
                    
                }          
                i ++;
                
                /*/public TMP_InputField [] NombresEjer = new TMP_InputField [7];
                public TMP_InputField [] FechasIniEjer = new TMP_InputField [7];
                public TMP_InputField [] FechasFiniEjer = new TMP_InputField [7];/*/
  
            }
            
        }
        
    }

    //Limpia los campos de registro de ejercicio
    public void LimpiarEjercicios(){

        for (int i = 0; i < 7; i++)
        {
            NombresEjer[i].text = "";
            FechasIniEjer[i].text = "";
            FechasFiniEjer[i].text = "";

        }

        ejerciciosControl.DesactivarAuto(ejerciciosControl.dato);
    }

    //Limpia el aviso superior
    public void LimpiarSaber(){
        NombreEjercicioActual.text = "";
        nomUsuarioEstante.text = "";

    }

    //Corrutina para eliminar el ejercicio
    private IEnumerator BorrarEjercicio(string nomEjer){

        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Usuarios").Child(paciente.text)
        .Child("Ejercicio").Child(nomEjer).RemoveValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            
        }

    }

    //Corrutina para editar el ejercicio fecha inicio
    private IEnumerator EditarEjercicioI(string nomEjer, string FechaI){

        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Usuarios").Child(paciente.text)
        .Child("Ejercicio").Child(nomEjer).Child("FechaInicio").SetValueAsync(FechaI);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            
        }

    }

    //Corrutina para eliminar el ejercicio fecha fin
    private IEnumerator EditarEjercicioF(string nomEjer, string FechaF){

        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Usuarios").Child(paciente.text)
        .Child("Ejercicio").Child(nomEjer).Child("FechaFin").SetValueAsync(FechaF);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            
        }

    } 

    //Funcion que ejecuta la corrutina para contar ejercicios
    public void OprimirPrueba(){
        StartCoroutine(contadorEjercicios());
    }

    //Cambia la interfaz superior visible de nombre ejercicio y paciente
    public void ApagarSaber(){
        Saber.SetActive(false);
    }
    
    //Funcion para ejecutar la corrutina para comparar la existencia de un usuario con todos los terapeutas
    public void ComprobarExistenciaPaciente(){
        StartCoroutine(ExtractorTerap());

    }

    //Quita el boton de guardar hasta que no se verifique su existencia
    public void ApagarGuardad(){
        GuardarPaciente.interactable = false;
        Cargando.text = "";
    }

    //Aviso de cargando en la interfaz de registro de pacientes
    public void CargandoCreacionUsuario(){
        Cargando.text = "Cargando, por favor espere un momento.";
        aviso.text = "";
        aviso2.text = "";
    }

    //corrutina para comparar la existencia de un usuario con todos los terapeutas
    private IEnumerator ExtractorTerap(){

        var DBTask = DBreference.Child("users").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            DataSnapshot snapsh = DBTask.Result; 
            int i = 0;
            aviso.text = "";
            aviso2.text = "Este usuario no existe";
            GuardarPaciente.interactable = true;
            numTerap = int.Parse(snapsh.ChildrenCount.ToString());
            Terapeutas = new string [numTerap];
            foreach (var us in snapsh.Children)
            {
                string TerapKey;
                TerapKey = us.Key.ToString();

                Terapeutas[i] = TerapKey;
                foreach (var item in us.Child("Usuarios").Children)
                {
                    if(usCompare.text == item.Key){
                        aviso2.text = "";
                        aviso.text = "Este usuario ya existe";
                        GuardarPaciente.interactable = false;
                    }
                    Debug.Log(item.Key);
                }
                Debug.Log(Terapeutas[i]);
                i ++;
                
            }
            
            
            
        }

    }





}
