/*
    Autor: Fausto Sánchez Hoya
    Descripción: este código se encarga de controlar la vida de Mike y ejecutar las acciones necesarias cuando este muere. 
            
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuerteController : MonoBehaviour
{
    private GameObject Mike;
    public GameObject escenaMuerte;
    public Text textoMuertes;
    // Start is called before the first frame update
    void Start()
    {
        Mike = GameObject.Find("Mike");
    }

    // Update is called once per frame
    void Update()
    {
        if(Mike.GetComponent<MikeController>().vida<=0) //Cuando la vida es igual o menor que 0 (ha muerto)
        {
            Time.timeScale = 0; //Se pausa la ejecución del juego para evitar que las animaciones se sigan viendo y afectando al juego.
            MikeHaMuerto(); //Se presenta la pantalla de muerte
            enabled = false; //Se desactiva este componente para evitar que se siga comprobando la vida y ejecutando continuamente MikeHaMuerto()
        }
    }

    private void MikeHaMuerto()
    {        
        textoMuertes.text = "Has matado " + Mike.GetComponent<MikeController>().zombiesMuertos + " zombies pero al final ellos han podido contigo"; 
        escenaMuerte.SetActive(true);
    }
}
