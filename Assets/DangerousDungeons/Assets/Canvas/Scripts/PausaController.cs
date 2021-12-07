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
    // Update is called once per frame
    void Update()
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
