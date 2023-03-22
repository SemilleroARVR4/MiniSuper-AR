using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BotonUsuario : MonoBehaviour
{
    //private string [] nombresUsuarios ={"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k"};
    [HideInInspector] public int usuarioCode;
    [HideInInspector] public ControlScroll controlScroll;

    [SerializeField] Text nombreusuario;

    [HideInInspector] public string[] huish;
    [HideInInspector] public string[] huish2;
    [HideInInspector] public string[] huish3;

    void Awake() {

       // usuarioCode = int.Parse(fire.valor);

    }
    private void Start(){



        huish = new string[FirebaseManager.NombresUsuarios.Length];
        huish = FirebaseManager.NombresUsuarios;
        //nombreusuario.text = "Usuario: " + (usuarioCode + 1);
        nombreusuario.text = huish[usuarioCode];

        //Debug.Log("el numero es" + controlScroll.huish[usuarioCode]);
    }
    public void clickboton()
    {
        controlScroll.refresh();
        controlScroll.BotonClick(nombreusuario.text, usuarioCode);

    }
}
