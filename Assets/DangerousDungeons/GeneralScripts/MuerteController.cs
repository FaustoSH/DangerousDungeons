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
        if(Mike.GetComponent<MikeController>().vida<=0)
        {
            
            MikeHaMuerto();
            Time.timeScale = 0;
        }
    }

    private void MikeHaMuerto()
    {        
        textoMuertes.text = "Has matado " + Mike.GetComponent<MikeController>().zombiesMuertos + " zombies pero al final ellos han podido contigo";
        escenaMuerte.SetActive(true);
    }
}
