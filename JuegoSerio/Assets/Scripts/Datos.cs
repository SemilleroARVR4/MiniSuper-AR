using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Datos : MonoBehaviour
{
    //Variables para el numero de identificación y el nombre completo del usuario
    public Text Nombre;
    public Text Identificacion;

    void Start()
    {
       //Se visualiza el nombre completo y la identificación en la escena "Perfil"
       Nombre.text = SeleccionDeOjeto.Userss;
       Identificacion.text = SeleccionDeOjeto.Userssd;
    }


}
