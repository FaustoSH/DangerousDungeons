/*
    Autor: Fausto Sánchez Hoya
    Descripción: este código se encarga de presentar u ocultar la ventana de pausa en el juego.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausaController : MonoBehaviour
{
    private bool pausa;
    public GameObject pausaMenu;
    private void Start()
    {
        pausa = false;
    }
    void Update() //Comprueba constantemente si se pulsa la tecla ESC y en ese caso presenta u oculta dicha ventana.
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!pausa)
            {
                Time.timeScale = 0;
                pausaMenu.SetActive(true);
                pausa = true;
            }else
            {
                
                pausaMenu.SetActive(false);
                Time.timeScale = 1;
                pausa = false;
            }
        }
    }
}
