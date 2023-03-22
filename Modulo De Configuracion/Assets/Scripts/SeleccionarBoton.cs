using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeleccionarBoton : MonoBehaviour
{

    [SerializeField] Text nombreusuario;
    [SerializeField] Text code;

    void Start(){
        
    }

    public void BotonClick(string name){

        code.text = "El usuario que presiono es: " + name;

    }

    public void clickboton(){

        BotonClick(nombreusuario.text);

    }

    

}
