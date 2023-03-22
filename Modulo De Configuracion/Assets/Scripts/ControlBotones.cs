using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlBotones : MonoBehaviour
{
    public TMP_InputField espacio;
    public Button BotonEquis;

    public void MostrarCaja(){
        Button btn = BotonEquis.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
    }
    public void TaskOnClick(){
        Debug.Log ("You have clicked the button!");
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
