using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenasController : MonoBehaviour
{
    public void VueltaALMenu()
    {
        Time.timeScale = 1;
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
