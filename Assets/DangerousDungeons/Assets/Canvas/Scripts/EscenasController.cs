/*
    Autor: Fausto S�nchez Hoya
    Descripci�n: este c�digo se encarga de las transiciones de escenas. Sus m�todos son llamadas por los botones del canvas.
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenasController : MonoBehaviour
{
    public void VueltaALMenu()
    {
        Time.timeScale = 1; //El timeScale a 1 es para asegurar que el tiempo de ejcuci�n vuelva a ser normal despu�s de que se haya parado al o bien morir o bien pausar el juego.
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void Campo()
    {
        SceneManager.LoadScene("EscenarioCampo");
    }

    public void Cementerio()
    {
        SceneManager.LoadScene("EscenarioCementerio");
    }
}
